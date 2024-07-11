using SprinklerApp.ViewModels;

namespace SprinklerApp.Views;

public partial class WeatherView : ContentPage
{
	public WeatherView()
	{
		InitializeComponent();
		BindingContext = new WeatherViewModel();
	}
}