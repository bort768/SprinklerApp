using Model.Helpers;

namespace Model
{
    public class TankByVolumeSpecification : BaseSpecifcation<Tank>
    {
        public TankByVolumeSpecification() 
        {
            AddOrderByDescending(x => x.Volume);
        }
    }
}
