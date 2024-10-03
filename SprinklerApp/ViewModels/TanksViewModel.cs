using CommunityToolkit.Mvvm.Input;
using Model.Dto;
using Model.Mapper;
using Newtonsoft.Json;
using SprinklerApp.Helpers;
using SprinklerApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Model.Helpers;

namespace SprinklerApp.ViewModels
{
    public partial class TanksViewModel : BaseViewModel
    {

        [ObservableProperty]
        public List<TankDisplayModel> tankDisplays;

        [ObservableProperty]
        string searchText;

        partial void OnSearchTextChanged(string value)
        {
             LoadItems();
        }

        [ObservableProperty]
        bool searchByName;

        [ObservableProperty]
        bool sortByCapacity;

        public TanksViewModel() { 
            LoadItems().ConfigureAwait(false);
        }

        [RelayCommand]
        public async Task LoadItems()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage? response = new();
                try
                {
                        var searchRoute = string.IsNullOrEmpty(SearchText)
                            ? GetApiAddress.GetAddress(GetApiAddress.ApiType.Tank)
                            : GetApiAddress.GetAddress(GetApiAddress.ApiType.Tank) +
                                $"/{RouteDictionary.Tank.SearchByNameRoute + SearchText}";
                        response = await client.GetAsync(searchRoute);
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

                    var tanksDto = JsonConvert.DeserializeObject<IEnumerable<TankDto>>(json);
                    if (tanksDto is null)
                        return;

                    var tanks = tanksDto.Select(t => TankMapper.ToModel(t));

                    TankDisplays = tanks.Select(t => new TankDisplayModel(t)).ToList();               

                }
                else
                {
                    await ToastSaveFail($"Something went wrong: {response}");
                }
            }
        }

        [RelayCommand]
        public async Task TankSelected(TankDisplayModel tank)
        {
            var navigationParameter = new ShellNavigationQueryParameters
                {
                    { ParamDictionary.TankId, tank.Id }
                };
            await Shell.Current.GoToAsync($"{nameof(TanksView)}/{nameof(TankView)}", navigationParameter);
        }

        [RelayCommand]
        public async Task AddTank()
        {
            await Shell.Current.GoToAsync($"{nameof(TanksView)}/{nameof(TankView)}");
        }

    }

}
