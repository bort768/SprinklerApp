
namespace Model
{
    public class HistoryOfIrrigation
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int TankId { get; set; }
        public Tank Tank { get; set; }
        public int SprinklerId { get; set; }
        public Sprinkler Sprinkler { get; set; }
        public double AmountOfWaterUsed { get; set; }
    }
}
