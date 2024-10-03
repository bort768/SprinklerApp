using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.Dto;
using Model.Helpers;
using Model.Mapper;
using Newtonsoft.Json;
using SprinklerApp.Helpers;
using SprinklerApp.Views;

namespace SprinklerApp.ViewModels
{
    public partial class SprinklersViewModel : BaseViewModel
    {
        [ObservableProperty]
        public List<SprinklerDisplayModel> sprinklerDisplayModels;

        [ObservableProperty]
        string searchText;

        partial void OnSearchTextChanged(string value)
        {
            LoadItems();
        }

        [ObservableProperty]
        bool searchByName;

        public SprinklersViewModel()
        {
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
                    //Todo: dodać szukanie po nazwie
                    var searchRoute = string.IsNullOrEmpty(SearchText)
                        ? $"{ApiSettings.Instance.ApiAddress}/Sprinkler"
                        : $"{ApiSettings.Instance.ApiAddress}/Sprinkler/" +
                            $"{RouteDictionary.Tank.SearchByNameRoute + SearchText}";
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

                    var sprinklerDto = JsonConvert.DeserializeObject<IEnumerable<SprinklerDto>>(json);
                    if (sprinklerDto is null)
                        return;

                    var tanks = sprinklerDto.Select(s => SprinklerMapper.ToModel(s));

                    SprinklerDisplayModels = tanks.Select(t => new SprinklerDisplayModel(t)).ToList();

                }
                else
                {
                    await ToastSaveFail($"Something went wrong: {response}");
                }
            }
        }

        [RelayCommand]
        public async Task SprinklerSelected(SprinklerDisplayModel sprinklerDisplay)
        {
            var navigationParameter = new ShellNavigationQueryParameters
                {
                    { ParamDictionary.SprinklerId, sprinklerDisplay.Id }
                };
            await Shell.Current.GoToAsync($"{nameof(SprinklersView)}/{nameof(SprinklerView)}", navigationParameter);
        }

        [RelayCommand]
        public async Task AddSprinkler()
        {
            await Shell.Current.GoToAsync($"{nameof(SprinklersView)}/{nameof(SprinklerView)}");
        }

    }
}
