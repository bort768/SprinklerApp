using Model.Dto;


namespace Model.Mapper
{
    public class TankMapper
    {
        public static TankDto ToDto(Tank tank)
        {
            return new TankDto
            {
                Id = tank.Id,
                Name = tank.Name,
                Length = tank.Length,
                Width = tank.Width,
                Height = tank.Height,
                FillLevel = tank.FillLevel,
                Volume = tank.Volume,
                TankCapacity = tank.TankCapacity,
                VolumeFillLevelInCubicMeters = tank.VolumeFillLevelInCubicMeters
            };
        }

        public static Tank ToModel(TankDto tankDto)
        {
            var tank = new Tank(tankDto.Id);
            tank.SetLength(tankDto.Length);
            tank.SetWidth(tankDto.Width);
            tank.SetHeight(tankDto.Height);
            tank.SetFillLevel(tankDto.FillLevel);
            tank.SetName(tankDto.Name);
            return tank;
        }
    }
}
