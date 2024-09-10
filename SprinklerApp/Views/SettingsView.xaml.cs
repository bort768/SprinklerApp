using SprinklerApp.ViewModels;

namespace SprinklerApp.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView()
	{
		InitializeComponent();
		BindingContext = new SettingsViewModel();
	}
}