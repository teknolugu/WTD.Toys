using System.Collections.Generic;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinToys.DataSource.Entities.Realm;
using WinToys.DataSource.Repository;
using WinToys.Models.Enums;
using WinToys.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace WinToys.ViewModels;

public partial class ManageBrowserMapViewModel : ObservableObject, INavigationAware
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string _selectedBrowser;

    [ObservableProperty]
    private BrowserMap _browserMap;

    [ObservableProperty]
    private IEnumerable<BrowserMap> _browserMaps;

    public ManageBrowserMapViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void OnNavigatedTo()
    {
        SelectedBrowser = BrowserSwitchRepository.GetInProgressBrowser()?.Path;
        BrowserMaps = BrowserSwitchRepository.GetActiveBrowserMap();
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private void SaveRowEdit(object obj)
    {
        if (obj is DataGridCellEditEndingEventArgs editEndingEventArgs)
            BrowserSwitchRepository.SaveUrl(new BrowserMap()
            {
                Path = SelectedBrowser,
                Url = (editEndingEventArgs.EditingElement as TextBox)!.Text,
                Status = (int)EventStatus.Completed
            });

        if (obj is DataGridRowEditEndingEventArgs rowEditEndingEventArgs)
        {
        }
    }

    [RelayCommand]
    private void Back()
    {
        _navigationService.Navigate(typeof(BrowserSwitchPage));
    }
}