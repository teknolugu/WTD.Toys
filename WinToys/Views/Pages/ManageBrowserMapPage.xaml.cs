using WinToys.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace WinToys.Views.Pages;

/// <summary>
/// Interaction logic for ManageBrowserMapPage.xaml
/// </summary>
public partial class ManageBrowserMapPage : INavigableView<ManageBrowserMapViewModel>
{
    public ManageBrowserMapViewModel ViewModel { get; }

    public ManageBrowserMapPage(ManageBrowserMapViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}