using WinToys.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace WinToys.Views.Pages;

/// <summary>
/// Interaction logic for DataView.xaml
/// </summary>
public partial class DataPage : INavigableView<DataViewModel>
{
    public DataViewModel ViewModel
    {
        get;
    }

    public DataPage(DataViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}