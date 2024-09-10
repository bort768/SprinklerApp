
namespace Model.Dto
{
    public class TankDto
    {
        public long Id { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double FillLevel { get; set; }
        public double Volume { get; set; }
        public double TankCapacity { get; set; }
        public double VolumeFillLevelInCubicMeters { get; set; }
    }
}
