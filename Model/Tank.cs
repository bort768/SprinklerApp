using Model.Helpers;

namespace Model
{
    public class Tank
    {
        public long Id { get; private set; } 
        public int Length { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public double FillLevel { get; private set; }

        public double Volume => Height * Width * Length;
        public double TankCapacity => Volume * FillLevel / 100;
        public double VolumeFillLevelInCubicMeters => Math.Round( ConvertVolumeToCubicMeters() * (FillLevel / 100), 2);

        public double ConvertVolumeToCubicMeters() => Math.Round(Volume / 1000000, 2);

        public Tank(long id)
        {
            Id = id;
        }

        public Result<int> SetLength(string length)
        {
            var result = int.TryParse(length, out int lengthInt);
            if (!result)
            {
                return Result<int>.Failure(lengthInt, "Length must be a number");
            }
            if (lengthInt < 0)
            {
                return Result<int>.Failure(lengthInt, "Length must be greater than 0");
            }
            Length = lengthInt;
            return Result<int>.Success(lengthInt);
        }

        public Result<int> SetWidth(string width)
        {
            var result = int.TryParse(width, out int widhtInt);
            if (!result)
            {
                return Result<int>.Failure(widhtInt, "Width must be a number");
            }
            if (widhtInt < 0)
            {
                return Result<int>.Failure(widhtInt, "Width must be greater than 0");
            }
            Width = widhtInt;
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

        //method to help map dto to model
        public void SetLength(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Length must be greater than 0");
            }
            Length = length;
        }

        public void SetWidth(int width)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Width must be greater than 0");
            }
            Width = width;
        }

        public void SetHeight(int height)
        {
            if (height <= 0)
            {
                throw new ArgumentException("Height must be greater than 0");
            }
            Height = height;
        }

    }
}
