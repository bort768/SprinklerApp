
namespace Model
{
    public class HistoryOfTankWaterLevel
    {
        public long Id { get; private set; }
        public long TankId { get; private set; }
        public Tank Tank { get; private set; }
        public DateTime Date { get; private set; }
        public double WaterLevel { get; private set; }

        public HistoryOfTankWaterLevel()
        {
            
        }

        public HistoryOfTankWaterLevel(long id, Tank tank, long tankId, DateTime date, double waterLevel)
        {
            Id = id;
            Tank = tank;
            TankId = tankId;
            Date = date;
            WaterLevel = waterLevel;
        }
    }
}
