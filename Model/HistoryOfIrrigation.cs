
namespace Model
{
    public class HistoryOfIrrigation
    {
        public long Id { get; private set; }
        public DateTime Date { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public int TankId { get; private set; }
        public Tank Tank { get; private set; }
        public int SprinklerId { get; private set; }
        public Sprinkler Sprinkler { get; private set; }
        public double AmountOfWaterUsed { get; private set; }
        public double WaterLevelBefore { get; private set; }

        public HistoryOfIrrigation()
        {
            
        }

        public HistoryOfIrrigation(long id, DateTime date, TimeSpan startTime, TimeSpan endTime, TimeSpan duration, int tankId, Tank tank, int sprinklerId, Sprinkler sprinkler, double amountOfWaterUsed, double waterLevelBefore)
        {
            Id = id;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            TankId = tankId;
            Tank = tank;
            SprinklerId = sprinklerId;
            Sprinkler = sprinkler;
            AmountOfWaterUsed = amountOfWaterUsed;
            WaterLevelBefore = waterLevelBefore;
        }
    }
}
