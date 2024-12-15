
namespace Model.Dto
{
    public class IrrigationScheduleDto
    {
        public long Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsActive { get; set; }
        public double MinimumTankLevel { get; set; }
        public long TankId { get; set; }
        public Tank Tank { get; set; }
        public IEnumerable<Sprinkler> Sprinklers { get; set; }
        public IrrigationMode Mode { get; set; }
    }
}
