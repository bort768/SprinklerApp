using SprinklerApp.ViewModels;

namespace SprinklerApp.Views;

public partial class TankView : ContentPage
{
	public TankView()
	{
		InitializeComponent();
		BindingContext = new TankViewModel();
	}
}