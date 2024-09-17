using Model;

namespace SprinklerApp.ViewModels
{
    public class SprinklerDisplayModel
    {
        private readonly Sprinkler _sprinkler;

        public Sprinkler GetSprinkler()
        {
            return _sprinkler;
        }

        public SprinklerDisplayModel(Sprinkler sprinkler)
        {
            _sprinkler = sprinkler;
            Name = _sprinkler.Name;
            Id = _sprinkler.Id;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
