using SprinklerApp.ViewModels;

namespace SprinklerApp.Views;

public partial class IrrigationControlView : ContentPage
{
	public IrrigationControlView()
	{
		InitializeComponent();
		BindingContext = new IrrigationControlViewModel();
    }
}