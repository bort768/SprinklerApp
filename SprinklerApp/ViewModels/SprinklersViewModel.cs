using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Dto;
using Model.Helpers;
using Model.Mapper;
using Newtonsoft.Json;
using SprinklerApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    //TODO: 23/09/2024: Add a mapper for sprinkler and DTO
                    var sprinklerDto = JsonConvert.DeserializeObject<IEnumerable<SprinklerDto>>(json);
                    if (sprinklerDto is null)
                        return;

                    var tanks = sprinklerDto.Select(s => SprinklerMapper.ToModel(s));

                    sprinklerDisplayModels = tanks.Select(t => new SprinklerDisplayModel(t)).ToList();

                }
                else
                {
                    await ToastSaveFail($"Something went wrong: {response}");
                }
            }
        }

        [RelayCommand]
        public async Task SprinklerSelected(TankDisplayModel tank)
        {
            var navigationParameter = new ShellNavigationQueryParameters
                {
                    { "TankId", tank.Id }
                };
            //await Shell.Current.GoToAsync($"{nameof(TanksView)}/{nameof(TankView)}", navigationParameter);
        }

        [RelayCommand]
        public async Task AddSprinkler()
        {
            //await Shell.Current.GoToAsync($"{nameof(TanksView)}/{nameof(TankView)}");
        }

    }
}
