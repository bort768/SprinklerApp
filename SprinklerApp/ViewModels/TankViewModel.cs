using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Helpers;
using Model.Mapper;
using Newtonsoft.Json;
using SprinklerApp.Helpers;
using System.Text;

namespace SprinklerApp.ViewModels
{
    public partial class TankViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string length;

        [ObservableProperty]
        private string width;

        [ObservableProperty]
        private string heigth;

        [ObservableProperty]
        private double fillLevel;

        [ObservableProperty]
        private double volumeFillLevel;

        [ObservableProperty]
        public double tankVolume;

        private Tank tank = new Tank();

        public TankViewModel()
        {
            //LoadTankInfo().ConfigureAwait(false);
        }

        public async Task OnNavigatedToAsync()
        {
            await LoadTankInfo();
        }

        private async Task LoadTankInfo()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage? response = new();
                try
                {
                    response = await client.GetAsync($"{ApiSettings.Instance.ApiAddress}/Tank");
                }
                catch (Exception e)
                {
                    await ToastSaveFail($"Something went wrong: {e.Message}");
                }
                

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(json))
                        return;

                    //TODO: It will get list of tanks, need to change it to get only one tank
                    var tanks = JsonConvert.DeserializeObject<IEnumerable<Tank>>(json);
                    tank = tanks.FirstOrDefault();

                    if (tank is null)
                        return;

                    Length = tank.Length.ToString();
                    Width = tank.Width.ToString();
                    Heigth = tank.Height.ToString();
                    FillLevel = tank.FillLevel;
                    TankVolume = tank.ConvertVolumeToCubicMeters();
                    VolumeFillLevel = tank.VolumeFillLevelInCubicMeters;

                }
            }
        }

        [RelayCommand]
        public async Task SaveData()
        {
            if(tank is null)
                tank = new Tank();

            var result = new Result<int>();
            result = result.Combine(tank.SetHeight(Heigth), tank.SetLength(Length), tank.SetWidth(Width));
            if (result.IsFailure)
            {
                await ToastSaveFail(result.Message);
                return;
            }
            TankVolume = tank.ConvertVolumeToCubicMeters();

            await SaveDataToDb();
        }

        private async Task SaveDataToDb()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage? response = new();
                var tankDto = TankMapper.ToDto(tank);
                var json = JsonConvert.SerializeObject(tankDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                if(tank.Id == 0)
                    response = await client.PostAsync($"{ApiSettings.Instance.ApiAddress}/Tank", content);
                else
                {
                    response = await client.PostAsync($"{ApiSettings.Instance.ApiAddress}/Tank/{tank.Id}", content);
                }


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