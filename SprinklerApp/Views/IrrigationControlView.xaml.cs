using SprinklerApp.ViewModels;

namespace SprinklerApp.Views;

public partial class IrrigationControlView : ContentPage
{
    private IrrigationControlViewModel viewModel = new IrrigationControlViewModel();

    public IrrigationControlView()
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (viewModel != null)
        {
            await viewModel.OnNavigatedToAsync();
        }

    }
}