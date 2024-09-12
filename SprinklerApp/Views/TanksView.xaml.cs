using SprinklerApp.ViewModels;

namespace SprinklerApp.Views;

public partial class TanksView : ContentPage
{
	public TanksView()
	{
		InitializeComponent();
		BindingContext = new TanksViewModel();
	}
}