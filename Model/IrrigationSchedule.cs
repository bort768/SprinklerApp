
namespace Model
{
    public class IrrigationSchedule
    {
        public long Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsActive { get; set; }
        public double MinimumTankLevel { get; set; }
        public int TankId { get; set; }
        public Tank Tank { get; set; }
        public int SprinklerId { get; set; }
        public Sprinkler Sprinkler { get; set; }
    }
}
