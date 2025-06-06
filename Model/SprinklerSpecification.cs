using Model.Helpers;

namespace Model
{

    public class SprinklerByNameSpecification : BaseSpecification<Sprinkler>
    {
        public SprinklerByNameSpecification(string name)
            : base(x => x.Name.Contains(name))
        {
        }
    }
}