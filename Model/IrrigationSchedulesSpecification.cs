
using Model.Helpers;
using static Model.Helpers.RouteDictionary;

namespace Model
{
    public class IrrigationSchedulesSpecificationSearchByDay_TankId_activeScheduleId : BaseSpecification<IrrigationSchedule>
    {
        public IrrigationSchedulesSpecificationSearchByDay_TankId_activeScheduleId(DayOfWeek day, long tankId, long activeScheduleId) 
            : base(s => s.Day == day && s.TankId == tankId && s.Id != activeScheduleId)
        {
            
        }
    }

    public class IrrigationSchedulesSpecificationByDay_TankId_Mode : BaseSpecification<IrrigationSchedule>
    {
        public IrrigationSchedulesSpecificationByDay_TankId_Mode(DayOfWeek day, long tankId, IrrigationMode irrigationMode) 
            : base(s => s.Day == day && s.TankId == tankId && s.Mode == irrigationMode)
        {

        }
    }
}
