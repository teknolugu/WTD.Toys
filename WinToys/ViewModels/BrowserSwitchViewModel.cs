using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinToys.DataSource.Repository;
using WinToys.Utils;
using WinToys.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace WinToys.ViewModels;

public partial class BrowserSwitchViewModel : ObservableObject, INavigationAware
{
    private const string AppName = "WinToys Browser Switcher";
    private const string AppId = "WinToys.BrowserSwitcher";
    private readonly string _exeName = Process.GetCurrentProcess().MainModule!.FileName;
    private readonly INavigationService _navigationService;
    private readonly BrowserMapRepository _browserMapRepository;

    [ObservableProperty]
    private string _selectedBrowser = string.Empty;

    [ObservableProperty]
    private bool _isRegistered;

    [ObservableProperty]
    private IEnumerable<string> _webBrowsers = new List<string>();

    public BrowserSwitchViewModel(INavigationService navigationService, BrowserMapRepository browserMapRepository)
    {
        _navigationService = navigationService;
        _browserMapRepository = browserMapRepository;
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
    private async Task RefreshBrowser()
    {
        await _browserMapRepository.FeedBrowserMap();

        WebBrowsers = await _browserMapRepository.GetBrowsers();

        IsRegistered = WebBrowsers.Any(x => x.Contains(_exeName));
    }

    [RelayCommand]
    private void OpenEditPage(string path)
    {
        _browserMapRepository.PrepareEditBrowserMap(path);
        _navigationService.Navigate(typeof(ManageBrowserMapPage));
    }
}