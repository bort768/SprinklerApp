using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Helpers;
using Newtonsoft.Json;
using SprinklerApp.Helpers;
using System.Text;

namespace SprinklerApp.ViewModels
{
    public partial class TankViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string lenght;

        [ObservableProperty]
        private string widht;

        [ObservableProperty]
        private string height;

        [ObservableProperty]
        private double fillLevel;

        [ObservableProperty]
        private double volumeFillLevel;

        [ObservableProperty]
        public double tankVolume;

        private Tank tank = new Tank();

        public TankViewModel()
        {

            LoadTankInfo().ConfigureAwait(false);
            FillLevel = 70;
            volumeFillLevel = 70;
        }

        private async Task LoadTankInfo()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.Instance.ApiAddress}TankController");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(json))
                        return;

                    tank = JsonConvert.DeserializeObject<Tank>(json);
                    if (tank is null)
                        return;
                    Lenght = tank.Lenght.ToString();
                    Widht = tank.Widht.ToString();
                    Height = tank.Height.ToString();
                    FillLevel = tank.FillLevel;

                }
            }
        }

        [RelayCommand]
        public async Task SaveData()
        {
            tank = new Tank();
            var result = new Result<int>();
            result = result.Combine(tank.SetHeight(Height), tank.SetLenght(Lenght), tank.SetWidht(Widht));
            if (result.IsFailure)
            {
                await ToastSaveFail(result.Message);
                return;
            }
            TankVolume = tank.ConvertVolumeToCubicMeters();

            //await SaveDataToDb();
        }

        private async Task SaveDataToDb()
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(tank);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{ApiSettings.Instance.ApiAddress}TankController", content);

                if (response.IsSuccessStatusCode)
                {
                    await ToastSaveSuccess("Data saved successfully.");
                }
                else
                {
                    await ToastSaveFail("Failed to save data to the database.");
                }
            }
        }
    }
}