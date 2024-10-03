using Model.Dto;

namespace Model.Mapper
{
    public static class IrrigationScheduleMapper
    {
        public static IrrigationScheduleDto ToDto(IrrigationSchedule irrigationSchedule)
        {
            return new IrrigationScheduleDto
            {
                Id = irrigationSchedule.Id,
                Day = irrigationSchedule.Day,
                StartTime = irrigationSchedule.StartTime,
                EndTime = irrigationSchedule.EndTime,
                Duration = irrigationSchedule.Duration,
                IsActive = irrigationSchedule.IsActive,
                MinimumTankLevel = irrigationSchedule.MinimumTankLevel,
                TankId = irrigationSchedule.TankId,
                Tank = irrigationSchedule.Tank,
                SprinklerId = irrigationSchedule.SprinklerId,
                Sprinkler = irrigationSchedule.Sprinkler

            };
        }

        public static IrrigationSchedule ToModel(IrrigationScheduleDto irrigationScheduleDto)
        {
            var irrigationSchedule = new IrrigationSchedule(irrigationScheduleDto.Id)
            {
                Day = irrigationScheduleDto.Day,
                StartTime = irrigationScheduleDto.StartTime,
                EndTime = irrigationScheduleDto.EndTime,
                Duration = irrigationScheduleDto.Duration,
                IsActive = irrigationScheduleDto.IsActive,              
                TankId = irrigationScheduleDto.TankId,
                Tank = irrigationScheduleDto.Tank,
                SprinklerId = irrigationScheduleDto.SprinklerId,
                Sprinkler = irrigationScheduleDto.Sprinkler

            };
            irrigationSchedule.SetMinimumTankLevel(irrigationScheduleDto.MinimumTankLevel);
            return irrigationSchedule;

        }
    }
}
