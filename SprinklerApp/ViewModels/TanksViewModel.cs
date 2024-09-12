using CommunityToolkit.Mvvm.Input;
using Model.Dto;
using Model.Mapper;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using SprinklerApp.Helpers;
using SprinklerApp.Views;

namespace SprinklerApp.ViewModels
{
    public partial class TanksViewModel : BaseViewModel
    {
        public List<TankViewModel> Tanks { get; set; }
        public ObservableCollection<TankDisplayModel> TankDisplays { get; set; }


        public TanksViewModel() { }

        [RelayCommand]
        public async Task LoadItems()
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

                    var tanksDto = JsonConvert.DeserializeObject<IEnumerable<TankDto>>(json);
                    if (tanksDto is null)
                        return;

                    var tanks = tanksDto.Select(t => TankMapper.ToModel(t));

                    TankDisplays = (ObservableCollection<TankDisplayModel>)tanks.Select(t => new TankDisplayModel(t));

                }
            }
        }

        [RelayCommand]
        public async Task TankSelected(TankDisplayModel tank)
        {
            var navigationParameter = new ShellNavigationQueryParameters
                {
                    { "TankId", tank.Id }
                };
            await Shell.Current.GoToAsync($"{nameof(TanksView)}/{nameof(TankView)}", navigationParameter);
        }
    }

}
