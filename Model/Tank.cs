using Model.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Tank
    {
        public long Id { get; private set; }
        [StringLength(MaxNameLenght)]
        public string Name { get; private set; }
        public int Length { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public double FillLevel { get; private set; }

        public double Volume => Height * Width * Length;
        public double TankCapacity => Volume * FillLevel / 100;
        public double VolumeFillLevelInCubicMeters => Math.Round( ConvertVolumeToCubicMeters() * (FillLevel / 100), 2);

        public double ConvertVolumeToCubicMeters() => Math.Round(Volume / 1000000, 2);

        private const int MaxNameLenght = 250; 

        public Tank()
        {
            
        }

        public Tank(long id)
        {
            Id = id;
        }

        public Result SetLength(string length)
        {
            var result = int.TryParse(length, out int lengthInt);
            if (!result)
            {
                return Result.Failure(lengthInt, "Length must be a number");
            }
            if (lengthInt < 0)
            {
                return Result.Failure(lengthInt, "Length must be greater than 0");
            }
            Length = lengthInt;
            return Result.Success(lengthInt);
        }

        public Result SetWidth(string width)
        {
            var result = int.TryParse(width, out int widhtInt);
            if (!result)
            {
                return Result.Failure(widhtInt, "Width must be a number");
            }
            if (widhtInt < 0)
            {
                return Result.Failure(widhtInt, "Width must be greater than 0");
            }
            Width = widhtInt;
            return Result.Success(widhtInt);
        }

        public Result SetHeight(string height)
        {
            var result = int.TryParse(height, out int heightInt);
            if (!result)
            {
                return Result.Failure(heightInt, "Height must be a number");
            }
            if (heightInt < 0)
            {
                return Result.Failure(heightInt, "Height must be greater than 0");
            }
            Height = heightInt;
            return Result.Success(heightInt);
        }

        public Result SetFillLevel(double fillLevel)
        {
            if (fillLevel < 0 || fillLevel > 100)
            {
                return Result.Failure(fillLevel, "Fill level must be between 0 and 100");
            }
            FillLevel = fillLevel;
            return Result.Success(fillLevel);
        }

        public Result SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure(name, "Name cannot be empty");
            }
            if(name.Length > MaxNameLenght)
            {
                return Result.Failure(name, $"Name cannot be longer than {MaxNameLenght} characters");
            }
            Name = name;
            return Result.Success(name);
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
