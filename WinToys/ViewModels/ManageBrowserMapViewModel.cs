using System.Collections.Generic;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using WinToys.DataSource.Entities.EF;
using WinToys.DataSource.Repository;
using WinToys.Models.Enums;
using WinToys.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace WinToys.ViewModels;

public partial class ManageBrowserMapViewModel : ObservableObject, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly BrowserMapRepository _browserMapRepository;

    [ObservableProperty]
    private string _selectedBrowser;

    [ObservableProperty]
    private BrowserMapEntity _browserMap;

    [ObservableProperty]
    private IEnumerable<BrowserMapEntity> _browserMaps;

    public ManageBrowserMapViewModel(INavigationService navigationService, BrowserMapRepository browserMapRepository)
    {
        _navigationService = navigationService;
        _browserMapRepository = browserMapRepository;
    }

    public void OnNavigatedTo()
    {
        SelectedBrowser = _browserMapRepository.GetInProgressBrowser();
        BrowserMaps = _browserMapRepository.GetActiveBrowserMap();
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private void SaveRowEdit(object obj)
    {
        if (obj is DataGridCellEditEndingEventArgs editEndingEventArgs)
            _browserMapRepository.SaveUrl(new BrowserMapEntity()
            {
                Path = SelectedBrowser,
                Url = (editEndingEventArgs.EditingElement as TextBox)!.Text,
                Status = EventStatus.Completed
            });

        if (obj is DataGridRowEditEndingEventArgs rowEditEndingEventArgs)
        {
        }
    }

    [RelayCommand]
    private void DeleteMap(string id)
    {
        _browserMapRepository.DeleteMapById(id);
        OnNavigatedTo();
    }

    [RelayCommand]
    private void Back()
    {
        _navigationService.Navigate(typeof(BrowserSwitchPage));
    }
}