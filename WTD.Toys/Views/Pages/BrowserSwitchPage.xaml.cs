using Wpf.Ui.Common.Interfaces;
using WTD.Toys.ViewModels;

namespace WTD.Toys.Views.Pages;

/// <summary>
/// Interaction logic for BrowserSwitchPage.xaml
/// </summary>
public partial class BrowserSwitchPage : INavigableView<BrowserSwitchViewModel>
{
    public BrowserSwitchViewModel ViewModel { get; }

    public BrowserSwitchPage(BrowserSwitchViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}