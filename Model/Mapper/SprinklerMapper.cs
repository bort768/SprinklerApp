using Model.Dto;

namespace Model.Mapper
{
    public class SprinklerMapper
    {
        public static SprinklerDto ToDto(Sprinkler sprinkler)
        {
            return new SprinklerDto
            {
                Id = sprinkler.Id,
                Name = sprinkler.Name,
                IsActive = sprinkler.IsActive
            };
        }

        public static Sprinkler ToModel(SprinklerDto sprinklerDto)
        {
            var sprinkler = new Sprinkler(sprinklerDto.Id);
            sprinkler.SetName(sprinklerDto.Name);
            sprinkler.SetIsActive(sprinklerDto.IsActive);
            return sprinkler;
        }
    }
}
