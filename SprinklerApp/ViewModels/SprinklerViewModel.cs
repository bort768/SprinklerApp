using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Dto;
using Model.Mapper;
using Model.Helpers;
using Newtonsoft.Json;
using SprinklerApp.Helpers;
using System.Text;

namespace SprinklerApp.ViewModels
{
    public partial class SprinklerViewModel : BaseViewModel, IQueryAttributable
    {
        private Sprinkler sprinkler;
        private long? sprinklerId;

        public SprinklerViewModel()
        {
            sprinkler = new Sprinkler();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ParamDictionary.SprinklerId, out var sprinklerIdValue) && sprinklerIdValue is long id)
            {
                sprinklerId = id;
            }
        }

        public async Task OnNavigatedToAsync()
        {
            await LoadSprinklerInfo();
        }

        private async Task LoadSprinklerInfo()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage? response = new();
                try
                {
                    if (sprinklerId is null)
                    {
                        IsDeleteButtonVisible = false;
                        return;
                    }
                        
                    response = await client.GetAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.Sprinkler, sprinklerId));
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
                
                    var sprinklerDto = JsonConvert.DeserializeObject<SprinklerDto>(json);
                    sprinkler = SprinklerMapper.ToModel(sprinklerDto);

                    if (sprinkler is null)
                        return;

                    Id = sprinkler.Id.ToString();
                    Name = sprinkler.Name;
                    IsActive = sprinkler.IsActive;
                }
            }
        }

        [RelayCommand]
        public async Task SaveData()
        {
            if (sprinkler is null)
                sprinkler = new();

            var result = new Result();
            result = result.Combine(sprinkler.SetName(Name));
            if (result.IsFailure)
            {
                await ToastSaveFail(result.Message);
                return;
            }

            await SaveDataToDb();
        }

        private async Task SaveDataToDb()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage? response = new();
                    var sprinklerDto = SprinklerMapper.ToDto(sprinkler);
                    var json = JsonConvert.SerializeObject(sprinklerDto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var apiAddress = ApiSettings.Instance.ApiAddress;
                    if (string.IsNullOrEmpty(apiAddress))
                        await ToastSaveFail("API address is missing.");

                    if (sprinklerId is null)
                        response = await client.PostAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.Sprinkler), content);
                    else
                        response = await client.PostAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.Sprinkler, sprinklerId), content);

                    if (response.IsSuccessStatusCode)
                        await ToastSaveSuccess("Data saved successfully.");
                    else
                        await ToastSaveFail("Failed to save data to the database.");
                }

                catch (Exception e)
                {
                    await ToastSaveFail($"Something went wrong: {e.Message}");
                    return;
                }
            }
        }

        [RelayCommand]
        public async Task DeleteSprinkler()
        {
            using (var client = new HttpClient())
            {
                //bool answer = await DisplayAlert("Potwierdzenie", "Czy na pewno chcesz usunąć dany zbiornik?", "Tak", "Nie");
                //if (answer is false)
                //    return;

                HttpResponseMessage? response = new();
                try
                {
                    if (sprinklerId is null)
                        return;

                    response = await client.DeleteAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.Sprinkler, sprinklerId));
                }
                catch (Exception e)
                {
                    await ToastSaveFail($"Something went wrong: {e.Message}");
                }

                if (response.IsSuccessStatusCode)
                {
                    await ToastSaveSuccess("Sprinkler deleted successfully.");
                }
                else
                {
                    await ToastSaveFail("Failed to delete Sprinkler.");
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
        private string name;

        partial void OnNameChanged(string value)
        {
           var result = sprinkler.SetName(value);
            NameIsValid = result.IsFailure;
            NameErrorMessage = result.Message;
        }

        [ObservableProperty]
        private bool nameIsValid;

        [ObservableProperty]
        private string nameErrorMessage;

        [ObservableProperty]
        private bool isActive;

        [ObservableProperty]
        private string? id;

        [ObservableProperty]
        private bool isDeleteButtonVisible;


    }
}

