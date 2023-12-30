using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WinToys.DataSource.Repository;
using WinToys.Extensions;
using WinToys.Models;
using WinToys.Services;
using WinToys.ViewModels;
using WinToys.Views.Pages;
using WinToys.Views.Windows;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace WinToys;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static IHost _host;

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetService<T>()
        where T : class
    {
        return _host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private async void OnStartup(object sender, StartupEventArgs e)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.File(
                Path.Combine(EnvVar.AppDataDir, "Logs/Log-.log"),
                flushToDiskInterval: TimeSpan.FromSeconds(2),
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Verbose,
                shared: true
            )
            .CreateLogger();

        var applicationBuilder = Host.CreateApplicationBuilder(e.Args);

        applicationBuilder.Environment.EnvironmentName = "Development";

        applicationBuilder.Configuration.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location));
        applicationBuilder.Logging.SetMinimumLevel(LogLevel.Trace);

        applicationBuilder.Logging.AddSentry(options =>
        {
            options.Dsn = "https://6d80e3bff4104600a623df0426966e5c@o192382.ingest.sentry.io/4505559666655232";
        });

        var services = applicationBuilder.Services;

        // App Host
        services.AddHostedService<ApplicationHostService>();

        // Page resolver service
        services.AddSingleton<IPageService, PageService>();

        // Theme manipulation
        services.AddSingleton<IThemeService, ThemeService>();

        // TaskBar manipulation
        services.AddSingleton<ITaskBarService, TaskBarService>();

        // Service containing navigation, same as INavigationWindow... but without window
        services.AddSingleton<INavigationService, NavigationService>();

        // Main window with navigation
        services.AddScoped<INavigationWindow, MainWindow>();
        services.AddScoped<MainWindowViewModel>();

        // Views and ViewModels
        services.AddScoped<DashboardPage>();
        services.AddScoped<DashboardViewModel>();

        services.AddScoped<DataPage>();
        services.AddScoped<DataViewModel>();

        services.AddScoped<SettingsPage>();
        services.AddScoped<SettingsViewModel>();

        services.AddScoped<BrowserSwitchPage>();
        services.AddScoped<BrowserSwitchViewModel>();

        services.AddScoped<ManageBrowserMapPage>();
        services.AddScoped<ManageBrowserMapViewModel>();

        services.AddScoped<BrowserMapRepository>();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(App).Assembly));
        services.AddFileContext();

        services.AddLogging(builder => builder
            .AddSerilog(dispose: true)
            .SetMinimumLevel(LogLevel.Trace));

        // Configuration
        services.Configure<AppConfig>(applicationBuilder.Configuration.GetSection(nameof(AppConfig)));

        _host = applicationBuilder.Build();

        await _host.StartAsync();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();

        Log.Information("Application exit..");
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        Log.Error(e.Exception, "Unhandled Error: {e}", sender);
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}