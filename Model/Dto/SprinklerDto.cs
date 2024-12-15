
using Model.Mapper;

namespace Model.Dto
{
    public class SprinklerDto : IDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public IModel ToModel()
        {
            return SprinklerMapper.ToModel(this);
        }
    }
}
