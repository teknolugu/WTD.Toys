using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinToys.Utils;
using Wpf.Ui.Common.Interfaces;

namespace WinToys.ViewModels;

public partial class BrowserSwitchViewModel : ObservableObject, INavigationAware
{
    private const string AppName = "WinToys Browser Switcher";
    private const string AppId = "WinToys.BrowserSwitcher";
    private readonly string _exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule!.FileName;

    [ObservableProperty]
    private string _selectedBrowser = string.Empty;

    [ObservableProperty]
    private bool _isRegistered;

    [ObservableProperty]
    private IEnumerable<string> _webBrowsers = new List<string>();

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
        if (IsRegistered)
        {
            RegistryUtil.SetDefaultBrowser(AppName, _exeName, AppId, "Let Toys help select right browser for you.");
        }
        else
        {
            RegistryUtil.UnRegisterBrowser(AppName, AppId);
        }
    }

    [RelayCommand]
    private void RefreshBrowser()
    {
        var allBrowsers = PathUtil.FindInstalledBrowser().Select(x => x.ExecutablePath).ToList();

        WebBrowsers = allBrowsers.Where(x => !x.Contains(_exeName));

        IsRegistered = allBrowsers.Any(x => x.Contains(_exeName));
    }
}