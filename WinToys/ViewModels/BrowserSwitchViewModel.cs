using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinToys.DataSource.Entities.Realm;
using WinToys.DataSource.Repository;
using WinToys.Models.Enums;
using WinToys.Utils;
using WinToys.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace WinToys.ViewModels;

public partial class BrowserSwitchViewModel : ObservableObject, INavigationAware
{
    private const string AppName = "WinToys Browser Switcher";
    private const string AppId = "WinToys.BrowserSwitcher";
    private readonly string _exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule!.FileName;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string _selectedBrowser = string.Empty;

    [ObservableProperty]
    private bool _isRegistered;

    [ObservableProperty]
    private IEnumerable<string> _webBrowsers = new List<string>();

    public BrowserSwitchViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void OnNavigatedTo()
    {
        RefreshBrowser();
    }

    public void OnNavigatedFrom()
    {
    }

    private void RegisterProtocol()
    {
    }

    [RelayCommand]
    private void OnRegisterBrowser()
    {
        RegistryUtil.IntegrateBrowserSwitcher(IsRegistered);
    }

    [RelayCommand]
    private void RefreshBrowser()
    {
        var allBrowsers = PathUtil.FindInstalledBrowser().Select(x => x.ExecutablePath).ToList();

        WebBrowsers = allBrowsers.Where(x => !x.Contains(_exeName));

        BrowserSwitchRepository.SaveBrowserList(WebBrowsers.Select(x => new BrowserPath()
        {
            Path = x,
            Status = (int)EventStatus.Completed
        }));

        IsRegistered = allBrowsers.Any(x => x.Contains(_exeName));
    }

    [RelayCommand]
    private void OpenEditPage(string path)
    {
        BrowserSwitchRepository.SaveBrowserMap(new BrowserPath()
        {
            Path = path,
            Status = (int)EventStatus.InProgress
        });

        _navigationService.Navigate(typeof(ManageBrowserMapPage));
    }
}