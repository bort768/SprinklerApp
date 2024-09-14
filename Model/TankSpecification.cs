using Model.Helpers;
using System.Linq.Expressions;

namespace Model
{
    public class TankByVolumeSpecification : BaseSpecifcation<Tank>
    {
        public TankByVolumeSpecification()
        {
            AddOrderByDescending(x => x.Volume);
        }
    }

    public class TankByNameSpecification : BaseSpecifcation<Tank>
    {
        public TankByNameSpecification(string name) : base(x => x.Name.Contains(name))
        {
        }

    }
}
