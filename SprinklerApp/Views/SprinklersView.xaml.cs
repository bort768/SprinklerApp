using SprinklerApp.ViewModels;

namespace SprinklerApp.Views;

public partial class SprinklersView : ContentPage
{
	public SprinklersView()
	{
		BindingContext = new SprinklersViewModel();
		InitializeComponent();
	}
}