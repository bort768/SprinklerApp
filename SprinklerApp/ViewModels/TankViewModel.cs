using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Dto;
using Model.Helpers;
using Model.Mapper;
using Newtonsoft.Json;
using SprinklerApp.Helpers;
using System.Text;

namespace SprinklerApp.ViewModels
{
    //TODO: 22/09/2024: Add Tank fill history with microcharts
    public partial class TankViewModel : BaseViewModel, IQueryAttributable
    {
        private Tank tank = new Tank();

        private long? tankId;

        public TankViewModel()
        {
        }

        //TODO 14/09/2024: Check if this works. It should be called when the user navigates to the page
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ParamDictionary.TankId, out var tankIdValue) && tankIdValue is long id)
            {
                tankId = id;
            }
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
                    if (tankId is null)
                    {
                        IsDeleteButtonVisible = false;
                        return;
                    }

                    
                    response = await client.GetAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.Tank, tankId));
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

                    var tankDto = JsonConvert.DeserializeObject<TankDto>(json);
                    if (tankDto == null) return;

                    tank = TankMapper.ToModel(tankDto);

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

            var result = new Result();
            result = result.Combine(tank.SetHeight(Heigth), tank.SetLength(Length),
                tank.SetWidth(Width), tank.SetName(Name));
            if (result.IsFailure)
            {
                OnHeigthChanged(Heigth);
                OnLengthChanged(Length);
                OnWidthChanged(Width);
                OnNameChanged(Name);

                await ToastSaveFail(result.Message);
                return;
            }
            TankVolume = tank.ConvertVolumeToCubicMeters();

            await SaveDataToDb();
        }

        //TODO: When there is no api address crash to desktop // probably fixed
        private async Task SaveDataToDb()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage? response = new();
                var tankDto = TankMapper.ToDto(tank);
                var json = JsonConvert.SerializeObject(tankDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var apiAddress = ApiSettings.Instance.ApiAddress;
                    if (string.IsNullOrEmpty(apiAddress))
                    {
                        await ToastSaveFail("API address is missing.");
                    }
                    if (tankId is null)
                    {
                        response = await client.PostAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.Tank), content);
                    }
                    else
                    {
                        response = await client.PostAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.Tank, tankId), content);
                    }
                }
                catch (Exception e)
                {
                    await ToastSaveFail($"Something went wrong: {e.Message}");
                    return;
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

        [RelayCommand]
        public async Task DeleteTank()
        {
            using (var client = new HttpClient())
            {
                //bool answer = await DisplayAlert("Potwierdzenie", "Czy na pewno chcesz usunąć dany zbiornik?", "Tak", "Nie");
                //if (answer is false)
                //    return;
            
            HttpResponseMessage? response = new();
                try
                {
                    if (tankId is null)
                        return;

                    response = await client.DeleteAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.Tank, tankId));
                }
                catch (Exception e)
                {
                    await ToastSaveFail($"Something went wrong: {e.Message}");
                }

                if (response.IsSuccessStatusCode)
                {
                    await ToastSaveSuccess("Tank deleted successfully.");
                }
                else
                {
                    await ToastSaveFail("Failed to delete tank.");
                }
            }
            await GoBack();
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        } 

        [ObservableProperty]
        private string length;

        partial void OnLengthChanged(string value)
        {
            var result = tank.SetLength(value);
            LengthIsValid = result.IsFailure;
            LengthErrorMessage = result.Message;
        }

        [ObservableProperty]
        private bool lengthIsValid;

        [ObservableProperty]
        private string lengthErrorMessage;

        [ObservableProperty]
        private string width;

        partial void OnWidthChanged(string value)
        {
            var result  = tank.SetWidth(value);
            WidthIsValid = result.IsFailure;
            WidthErrorMessage = result.Message;

        }

        [ObservableProperty]
        public bool widthIsValid;

        [ObservableProperty]
        public string widthErrorMessage;

        [ObservableProperty]
        private string heigth;

        partial void OnHeigthChanged(string value)
        {
            var result = tank.SetHeight(value);
            HeigthIsValid = result.IsFailure;
            HeigthErrorMessage = result.Message;
        }

        [ObservableProperty]
        private bool heigthIsValid;

        [ObservableProperty]
        private string heigthErrorMessage;

        [ObservableProperty]
        private string name;

        partial void OnNameChanged(string value)
        {
            var result = tank.SetName(value);
            NameIsValid = result.IsFailure;
            NameErrorMessage = result.Message;
        }

        [ObservableProperty]
        private bool nameIsValid;

        [ObservableProperty]
        private string nameErrorMessage;

        [ObservableProperty]
        private double fillLevel;

        [ObservableProperty]
        private double volumeFillLevel;

        [ObservableProperty]
        private double tankVolume;

        [ObservableProperty]
        private bool isDeleteButtonVisible;
    }

}