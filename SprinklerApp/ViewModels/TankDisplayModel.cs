using Model;

namespace SprinklerApp.ViewModels
{
    public class TankDisplayModel
    {
        private readonly Tank _tank;

        public Tank GetTank()
        {
            return _tank;
        }

        public TankDisplayModel(Tank tank)
        {
            _tank = tank;
            Name = _tank.Name;
            Id = _tank.Id;
            FillLevel = _tank.FillLevel;
            Capacity = _tank.TankCapacity;
        }

        public string Name { get; private set; }
        public long Id { get; private set; }
        public double FillLevel { get; private set; }
        public double Capacity { get; private set; }

    }
}
