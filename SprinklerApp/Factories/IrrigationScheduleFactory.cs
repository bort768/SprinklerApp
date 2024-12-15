using Model.Dto;
using Model.Mapper;
using Model;
using SprinklerApp.ViewModels;

namespace SprinklerApp.Factories
{
    public static class IrrigationScheduleFactory
    {
        public static IrrigationSchedule CreateIrrigationSchedule(Tank selectedTank, IEnumerable<SprinklerDisplayModel> selectedSprinklers, IrrigationMode mode, bool isActive)
        {
            return new IrrigationSchedule
            {
                TankId = selectedTank.Id,
                Tank = selectedTank,
                IsActive = isActive,
                Sprinklers = selectedSprinklers.Select(s => s.GetSprinkler()),
                Mode = mode
            };
        }

        public static IrrigationSchedule CreateIrrigationSchedule(Tank selectedTank, IEnumerable<SprinklerDisplayModel> selectedSprinklers, IrrigationMode mode, bool isActive, TimeSpan startTime, TimeSpan endTime, TimeSpan duration, string minimumTankLevelLabel)
        {
            var irrigationSchedule = new IrrigationSchedule
            {
                TankId = selectedTank.Id,
                Tank = selectedTank,
                IsActive = isActive,
                Sprinklers = selectedSprinklers.Select(s => s.GetSprinkler()),
                Mode = mode,
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration
            };
            irrigationSchedule.SetMinimumTankLevel(minimumTankLevelLabel);
            return irrigationSchedule;
        }

        //public static IrrigationScheduleDto CreateIrrigationScheduleDto(IrrigationSchedule irrigationSchedule)
        //{
        //    return IrrigationScheduleMapper.ToDto(irrigationSchedule);
        //}
    }
}

