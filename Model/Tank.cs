using Model.Helpers;

namespace Model
{
    public class Tank
    {
        public long Id { get; set; }
        public int Lenght { get; private set; }
        public int Widht { get; private set; }
        public int Height { get; private set; }

        public double FillLevel { get; private set; }

        public double Volume => Height * Widht * Lenght;
        public double TankCapacity => Volume * FillLevel / 100;

        public double ConvertVolumeToCubicMeters() => Math.Round( Volume / 1000000, 2);

        public Result<int> SetLenght(string lenght)
        {
            var result = int.TryParse(lenght, out int lenghtInt);
            if (!result)
            {
                return Result<int>.Failure(lenghtInt, "Lenght must be a number");
            }
            if (lenghtInt < 0)
            {
                return Result<int>.Failure(lenghtInt, "Lenght must be greater than 0");
            }
            Lenght = lenghtInt;
            return Result<int>.Success(lenghtInt);
        }

        public Result<int> SetWidht(string widht)
        {
            var result = int.TryParse(widht, out int widhtInt);
            if (!result)
            {
                return Result<int>.Failure(widhtInt, "Widht must be a number");
            }
            if (widhtInt < 0)
            {
                return Result<int>.Failure(widhtInt, "Widht must be greater than 0");
            }
            Widht = widhtInt;
            return Result<int>.Success(widhtInt);
        }

        public Result<int> SetHeight(string height)
        {
            var result = int.TryParse(height, out int heightInt);
            if (!result)
            {
                return Result<int>.Failure(heightInt, "Height must be a number");
            }
            if (heightInt < 0)
            {
                return Result<int>.Failure(heightInt, "Height must be greater than 0");
            }
            Height = heightInt;
            return Result<int>.Success(heightInt);
        }

        public Result<double> SetFillLevel(double fillLevel)
        {
            if (fillLevel < 0 || fillLevel > 100)
            {
                return Result<double>.Failure(fillLevel, "Fill level must be between 0 and 100");
            }
            FillLevel = fillLevel;
            return Result<double>.Success(fillLevel);
        }
    }
}
