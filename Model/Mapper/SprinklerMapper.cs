using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            };
        }

        public static Sprinkler ToModel(SprinklerDto sprinklerDto)
        {
            var sprinkler = new Sprinkler(sprinklerDto.Id);
            sprinkler.SetName(sprinklerDto.Name);
            return sprinkler;
        }
    }
}
