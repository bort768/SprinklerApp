using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Mapper;
using Newtonsoft.Json;
using SprinklerApp.Helpers;
using System.Diagnostics;
using System.Text;

namespace SprinklerApp.ViewModels
{
    public partial class IrrigationControlViewModel : BaseViewModel
    {
        
        private double minimumTankLevel;

        [ObservableProperty]
        private string minimumTankLevelLabel;
       
        //Sussy baka
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
        private Tank selectedTank;

        [ObservableProperty]
        private List<Tank> listOfTanks;

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

        [ObservableProperty]
        private TimeSpan startTime;

        [ObservableProperty]
        private TimeSpan endTime;

        [ObservableProperty]
        private int duration;


        public IrrigationControlViewModel()
        {
            irrigationSchedules = new List<IrrigationSchedule>();
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

        //TODO : Implement the logic to control irrigation based on the provided data to raspberry pi

        //private bool ShouldIrrigate()
        //{
        //    if (selectedTank.FillLevel < minimumTankLevel)
        //        return false;

        //    TimeSpan currentTime = DateTime.Now.TimeOfDay;
        //    if (currentTime >= startTime && currentTime <= endTime)
        //        return true;

        //    return false;
        //}

        [RelayCommand]
        public async Task StartIrrigationManual()
        {
            //TODO : Implement the logic to start irrigation
            if (SelectedTank.FillLevel < minimumTankLevel)
            {
                await ToastSaveFail("Tank level is below the minimum level.");
                return;
            }
            //TODO 03/10/2024: add start irigation logic with selected sprinklers add in irrigationcontrol api address to start irrigation
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage? response = new();
                    
                    var selectedSprinklers = ListOfSprinklers.Where(s => s.IsSelected);

                    var sprinklerDto = selectedSprinklers.Select(s => SprinklerMapper.ToDto(s.GetSprinkler()));
                    var json = JsonConvert.SerializeObject(sprinklerDto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var apiAddress = ApiSettings.Instance.ApiAddress;
                    if (string.IsNullOrEmpty(apiAddress))
                        await ToastSaveFail("API address is missing.");

                    response = await client.PostAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.IrrigationControl), content);

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
        public void StopIrrigationManual()
        {
            //TODO : Implement the logic to stop irrigation
        }

        [RelayCommand]
        public async Task SaveIrrigationSchedule()
        {
            //debug
            foreach(var testActive in IrrigationSchedules)
            {
                Debug.WriteLine(testActive.Day + testActive.IsActive.ToString());
            }
            //TODO : Implement the logic to save the irrigation schedule
            //using (var client = new HttpClient())
            //{
            //    try
            //    {
            //        HttpResponseMessage? response = new();
            //        var sprinklerDto = SprinklerMapper.ToDto(sprinkler);
            //        var json = JsonConvert.SerializeObject(sprinklerDto);
            //        var content = new StringContent(json, Encoding.UTF8, "application/json");

            //        var apiAddress = ApiSettings.Instance.ApiAddress;
            //        if (string.IsNullOrEmpty(apiAddress))
            //            await ToastSaveFail("API address is missing.");

            //        if (sprinklerId is null)
            //            response = await client.PostAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.IrrigationControl), content);
            //        else
            //            response = await client.PostAsync(GetApiAddress.GetAddress(GetApiAddress.ApiType.IrrigationControl, ), content);

            //        if (response.IsSuccessStatusCode)
            //            await ToastSaveSuccess("Data saved successfully.");
            //        else
            //            await ToastSaveFail("Failed to save data to the database.");
            //    }

            //    catch (Exception e)
            //    {
            //        await ToastSaveFail($"Something went wrong: {e.Message}");
            //        return;
            //    }
            //}
        }

        //CollectionView
        [RelayCommand]
        public void CVSprinklerSelectionChanged(SprinklerDisplayModel sprinklerDisplayModel)
        {
            sprinklerDisplayModel.IsSelected = !sprinklerDisplayModel.IsSelected;
        }

    }
}
