using SprinklerApp.ViewModels;

namespace SprinklerApp.Views;

public partial class SprinklerView : ContentPage
{
    private SprinklerViewModel viewModel = new SprinklerViewModel();

    public SprinklerView()
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