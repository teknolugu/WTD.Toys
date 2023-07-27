using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WinToys.Views.Pages;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace WinToys.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private string _applicationTitle = string.Empty;

    [ObservableProperty]
    private ObservableCollection<INavigationControl> _navigationItems = new();

    [ObservableProperty]
    private ObservableCollection<INavigationControl> _navigationFooter = new();

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = new();

    public MainWindowViewModel(INavigationService navigationService)
    {
        if (!_isInitialized)
            InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        ApplicationTitle = "WPF UI - WinToys";

        NavigationItems = new ObservableCollection<INavigationControl>
        {
            new NavigationItem()
            {
                Content = "Home",
                PageTag = "dashboard",
                Icon = SymbolRegular.Home24,
                PageType = typeof(DashboardPage)
            },
            new NavigationItem()
            {
                Content = "BrowserMap",
                PageTag = "browser-map",
                Icon = SymbolRegular.Directions24,
                PageType = typeof(BrowserSwitchPage)
            },
            new NavigationItem()
            {
                Content = "Data",
                PageTag = "data",
                Icon = SymbolRegular.DataHistogram24,
                PageType = typeof(DataPage)
            }
        };

        NavigationFooter = new ObservableCollection<INavigationControl>
        {
            new NavigationItem()
            {
                Content = "Settings",
                PageTag = "settings",
                Icon = SymbolRegular.Settings24,
                PageType = typeof(SettingsPage)
            }
        };

        TrayMenuItems = new ObservableCollection<MenuItem>
        {
            new()
            {
                Header = "Home",
                Tag = "tray_home"
            }
        };

        _isInitialized = true;
    }
}