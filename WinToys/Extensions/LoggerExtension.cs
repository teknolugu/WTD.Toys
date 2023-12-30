using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using WinToys.Models;

namespace WinToys.Extensions;

internal static class LoggerExtension
{
    public static ILoggingBuilder AddLogging(this ILoggingBuilder loggingBuilder)
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

        loggingBuilder.SetMinimumLevel(LogLevel.Trace);

        loggingBuilder.AddSentry(options =>
        {
            options.Dsn = EnvConst.SentryDsn;
        });

        return loggingBuilder;
    }

    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        services.AddLogging(builder => builder
            .AddSerilog(dispose: true)
            .SetMinimumLevel(LogLevel.Trace));

        return services;
    }
}