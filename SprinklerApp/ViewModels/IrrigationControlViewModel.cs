using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;

namespace SprinklerApp.ViewModels
{
    public partial class IrrigationControlViewModel : BaseViewModel
    {
        [ObservableProperty]
        private double minimumTankLevel;

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
        public async void StartIrrigationManual()
        {
            //TODO : Implement the logic to start irrigation
            if (SelectedTank.FillLevel < MinimumTankLevel)
            {
                await ToastSaveFail("Tank level is below the minimum level.");
                return;
            }

        }

        [RelayCommand]
        public void StopIrrigationManual()
        {
            //TODO : Implement the logic to stop irrigation
        }

        [RelayCommand]
        public void SaveIrrigationSchedule()
        {
            //TODO : Implement the logic to save the irrigation schedule
        }

        public void SelectionChanged(SprinklerDisplayModel sprinklerDisplayModel)
        {
            sprinklerDisplayModel.IsSelected = !sprinklerDisplayModel.IsSelected;
        }

    }
}
