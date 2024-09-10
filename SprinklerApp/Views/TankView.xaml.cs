using SprinklerApp.ViewModels;

namespace SprinklerApp.Views;

public partial class TankView : ContentPage
{
	private TankViewModel viewModel = new TankViewModel();

	public TankView()
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