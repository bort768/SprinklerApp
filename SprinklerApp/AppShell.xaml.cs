using SprinklerApp.ViewModels;
using SprinklerApp.Views;

namespace SprinklerApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute($"{nameof(WeatherView)}", typeof(WeatherView));
            Routing.RegisterRoute($"{nameof(SettingsView)}", typeof(SettingsView));
            Routing.RegisterRoute($"{nameof(TanksView)}", typeof(TanksView));
            Routing.RegisterRoute($"{nameof(TanksView)}/{nameof(TankView)}", typeof(TankView));
            Routing.RegisterRoute($"{nameof(IrrigationControlView)}", typeof(IrrigationControlView));
        }
    }
}
