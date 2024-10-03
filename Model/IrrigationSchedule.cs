
using Model.Helpers;

namespace Model
{
    public class IrrigationSchedule
    {
        public long Id { get; private set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsActive { get; set; }
        public double MinimumTankLevel { get; private set; }
        public int TankId { get; set; }
        public Tank Tank { get; set; }
        public int SprinklerId { get; set; }
        public Sprinkler Sprinkler { get; set; }

        public IrrigationSchedule()
        {

        }

        public IrrigationSchedule(long id)
        {
            Id = id;      
        }

        public Result SetMinimumTankLevel(string minimumTankLevelStr)
        {
            if (string.IsNullOrWhiteSpace(minimumTankLevelStr))
                return Result.Failure(minimumTankLevelStr, "Minimum tank level cannot be empty");

            var result = double.TryParse(minimumTankLevelStr, out double minimumTankLevel);
            if (result == false)
            {
                return Result.Failure(minimumTankLevelStr, "Minimum tank level must be a number");
            }
            if (minimumTankLevel < 0)
            {
                return Result.Failure(minimumTankLevel, "Minimum tank level must be greater than 0");
            }

            MinimumTankLevel = minimumTankLevel;
            return Result.Success(minimumTankLevel);

        }

        public Result SetMinimumTankLevel(double minimumTankLevel)
        {
            if (minimumTankLevel < 0)
            {
                return Result.Failure(minimumTankLevel, "Minimum tank level must be greater than 0");
            }

            MinimumTankLevel = minimumTankLevel;
            return Result.Success(minimumTankLevel);
        }

    }
}
