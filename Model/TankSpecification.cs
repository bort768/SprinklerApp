using Model.Helpers;
using System.Linq.Expressions;

namespace Model
{
    public class TankByVolumeSpecification : BaseSpecification<Tank>
    {
        public TankByVolumeSpecification()
        {
            AddOrderByDescending(x => x.Volume);
        }
    }

    public class TankByNameSpecification : BaseSpecification<Tank>
    {
        public TankByNameSpecification(string name) : base(x => x.Name.Contains(name))
        {
        }

    }
}
