using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using SprinklerApp.Factories;
using SprinklerApp.Helpers;
using SprinklerApp.Helpers.Interfaces;
using SprinklerApp.Services;

namespace SprinklerApp.ViewModels
{
    public partial class IrrigationControlViewModel : BaseViewModel
    {
        private double minimumTankLevel;

        [ObservableProperty]
        private string minimumTankLevelLabel;

        partial void OnMinimumTankLevelLabelChanged(string value)
        {
            var result = IrrigationSchedules[0].SetMinimumTankLevel(value);
            MinimumTankLevelErrorMessage = result.Message;
            MinimumTankLevelIsValid = result.IsFailure;
        }

        [ObservableProperty]
        private string minimumTankLevelErrorMessage;

        [ObservableProperty]
        private bool minimumTankLevelIsValid;

        [ObservableProperty]
        private TankDisplayModel selectedTank;

        [ObservableProperty]
        private List<TankDisplayModel> listOfTanks = [];

        [ObservableProperty]
        private List<SprinklerDisplayModel> listOfSprinklers;

        [ObservableProperty]
        private List<IrrigationSchedule> irrigationSchedules;

        [ObservableProperty]
        private bool isIrrigationByDayEnabled;

        [ObservableProperty]
        private bool isIrrigationControlMaunal;

        [ObservableProperty]
        private bool isIrrigationControlAutomatic;

        private IrrigationMode irrigationMode;

        partial void OnIsIrrigationByDayEnabledChanged(bool value)
        {
            if (value)
                irrigationMode = IrrigationMode.Planned;
        }

        partial void OnIsIrrigationControlMaunalChanged(bool value)
        {
            if (value)
                irrigationMode = IrrigationMode.Manual;
        }

        partial void OnIsIrrigationControlAutomaticChanged(bool value)
        {
            if (value)
                irrigationMode = IrrigationMode.Automatic;
        }

        [ObservableProperty]
        private TimeSpan startTime;

        [ObservableProperty]
        private TimeSpan endTime;

        [ObservableProperty]
        private TimeSpan duration;

        public IrrigationControlViewModel()
        {
            irrigationSchedules = [];
            AddIrrigationSchedule(DayOfWeek.Monday, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, false);
            AddIrrigationSchedule(DayOfWeek.Tuesday, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, false);
            AddIrrigationSchedule(DayOfWeek.Wednesday, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, false);
            AddIrrigationSchedule(DayOfWeek.Thursday, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, false);
            AddIrrigationSchedule(DayOfWeek.Friday, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, false);
            AddIrrigationSchedule(DayOfWeek.Saturday, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, false);
            AddIrrigationSchedule(DayOfWeek.Sunday, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, false);
        }

        public void AddIrrigationSchedule(DayOfWeek day, TimeSpan startTime, TimeSpan endTime, TimeSpan duration, bool isActive)
        {
            IrrigationSchedules.Add(new IrrigationSchedule
            {
                Day = day,
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration,
                IsActive = isActive
            });
        }

        [RelayCommand]
        public async Task SaveIrrigationSchedule()
        {
            switch (irrigationMode)
            {
                case IrrigationMode.Manual:
                    await StartIrrigationManual();
                    break;
                case IrrigationMode.Automatic:
                    await SaveIrrigationAutmatic();
                    break;
                case IrrigationMode.Planned:
                    await SaveIrrigationPlanned();
                    break;
            }
        }

        public override async Task OnNavigatedToAsync()
        {
            await LoadTankInfo();
            await LoadSprinklers();
        }

        private async Task LoadTankInfo()
        {
            using var client = new HttpClient();
            try
            {
                var tankApiService = new TankApiService(client, GetApiAddress.GetAddress(GetApiAddress.ApiType.Tank));
                var result = await tankApiService.GetDataAsync();
                if (result.IsFailure)
                {
                    await ToastSaveFail(result.Message);
                    return;
                }
                ListOfTanks = (List<TankDisplayModel>)result.Value;
            }
            catch (Exception e)
            {
                await ToastSaveFail($"Something went wrong: {e.Message}");
            }         
        }

        private async Task LoadSprinklers()
        {
            using var client = new HttpClient();
            try
            {
                var sprinklerApiService = new SprinklerApiService(client, GetApiAddress.GetAddress(GetApiAddress.ApiType.Sprinkler));
                var result = await sprinklerApiService.GetDataAsync();
                if (result.IsFailure)
                {
                    await ToastSaveFail(result.Message);
                    return;
                }
                ListOfSprinklers = (List<SprinklerDisplayModel>)result.Value;
            }
            catch (Exception e)
            {
                await ToastSaveFail($"Something went wrong: {e.Message}");
            }
        }

        [RelayCommand]
        public async Task StartIrrigationManual()
        {
            if (SelectedTank is null)
            {
                await ToastSaveFail("Please select a tank.");
                return;
            }
            if (SelectedTank.FillLevel < minimumTankLevel)
            {
                await ToastSaveFail("Tank level is below the minimum level.");
                return;
            }

            using var client = new HttpClient();
            try
            {
                var selectedSprinklers = ListOfSprinklers.Where(s => s.IsSelected);
                var irrigationSchedule = IrrigationScheduleFactory.CreateIrrigationSchedule(SelectedTank.GetTank(), selectedSprinklers, IrrigationMode.Manual, true);

                var irrigationApiService = new ApiService<IrrigationSchedule>(client, ApiSettings.Instance.ApiAddress);
                var response = await irrigationApiService.SendDataAsync(irrigationSchedule);

                if (response.IsSuccessful)
                    await ToastSaveSuccess("Data saved successfully.");
                else
                    await ToastSaveFail(response.Message);
            }
            catch (Exception e)
            {
                await ToastSaveFail($"Something went wrong: {e.Message}");
                return;
            }
        }

        [RelayCommand]
        public async Task StopIrrigationManual()
        {
            if (SelectedTank is null)
            {
                await ToastSaveFail("Please select a tank.");
                return;
            }

            using var client = new HttpClient();
            try
            {
                var selectedSprinklers = ListOfSprinklers.Where(s => s.IsSelected);
                var irrigationSchedule = IrrigationScheduleFactory.CreateIrrigationSchedule(SelectedTank.GetTank(), selectedSprinklers, IrrigationMode.Manual, false);

                var irrigationApiService = new ApiService<IrrigationSchedule>(client, ApiSettings.Instance.ApiAddress);
                var response = await irrigationApiService.SendDataAsync(irrigationSchedule);

                if (response.IsSuccessful)
                    await ToastSaveSuccess("Irrigation stopped successfully.");
                else
                    await ToastSaveFail(response.Message);
            }
            catch (Exception e)
            {
                await ToastSaveFail($"Something went wrong: {e.Message}");
                return;
            }
        }

        public async Task SaveIrrigationPlanned()
        {
            using var client = new HttpClient();
            try
            {
                List<IrrigationSchedule> irrigationSchedules = [];
                foreach (var irrigationSchedule in IrrigationSchedules)
                {
                    var newIrrigationSchedule = IrrigationScheduleFactory.CreateIrrigationSchedule(SelectedTank.GetTank(),
                        ListOfSprinklers.Where(s => s.IsSelected), IrrigationMode.Planned, true, StartTime, EndTime, Duration, MinimumTankLevelLabel);

                    irrigationSchedules.Add(irrigationSchedule);
                }

                var irrigationApiService = new ApiService<IrrigationSchedule>(client, ApiSettings.Instance.ApiAddress);
                var response = await irrigationApiService.SendDataBatchAsync(irrigationSchedules);

                if (response.IsSuccessful)
                    await ToastSaveSuccess("Data saved successfully.");
                else
                    await ToastSaveFail(response.Message);
            }
            catch (Exception e)
            {
                await ToastSaveFail($"Something went wrong: {e.Message}");
                return;
            }
        }

        public async Task SaveIrrigationAutmatic()
        {
            using var client = new HttpClient();
            try
            {
                List<IrrigationSchedule> irrigationSchedules = [];
                foreach (var irrigationSchedule in IrrigationSchedules)
                {
                    var newIrrigationSchedule = IrrigationScheduleFactory.CreateIrrigationSchedule
                        (SelectedTank.GetTank(), ListOfSprinklers.Where(s => s.IsSelected), IrrigationMode.Planned, true, StartTime, EndTime, Duration, MinimumTankLevelLabel);

                    irrigationSchedules.Add(irrigationSchedule);
                }

                var irrigationApiService = new ApiService<IrrigationSchedule>(client, ApiSettings.Instance.ApiAddress);
                var response = await irrigationApiService.SendDataBatchAsync(irrigationSchedules);

                if (response.IsSuccessful)
                    await ToastSaveSuccess("Data saved successfully.");
                else
                    await ToastSaveFail(response.Message);
            }
            catch (Exception e)
            {
                await ToastSaveFail($"Something went wrong: {e.Message}");
                return;
            }
        }

        ////TODO: Seperetate this method into two methods for Sprinklers and Tanks to notify change in list
        //[RelayCommand]
        //public void CollectionViewSelectionChanged(ISelectableCollection selectableCollection)
        //{
        //    selectableCollection.IsSelected = !selectableCollection.IsSelected;
        //    //if (selectableCollection is List<TankDisplayModel>)
        //    //    ListOfTanks.Where(t => t.Id == selectableCollection.Id) = 
        //}
 
        [RelayCommand]
        public void CollectionViewTankSelectionChanged(TankDisplayModel selectedTank)
        {
            var tank = ListOfTanks.Find(t => t.Id == selectedTank.Id);
            if (tank != null)
            {
                tank.IsSelected = !selectedTank.IsSelected;
            }
        }

        [RelayCommand]
        public void CollectionViewSprinklerSelectionChanged(SprinklerDisplayModel selectedSprinkler)
        {
            var sprinkler = ListOfSprinklers.Find(s => s.Id == selectedSprinkler.Id);
            if (sprinkler != null)
            {
                sprinkler.IsSelected = !selectedSprinkler.IsSelected;
            }
        }
    }
}
