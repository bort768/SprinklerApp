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
            Routing.RegisterRoute($"{nameof(TankView)}", typeof(TankView));
        }
    }
}
