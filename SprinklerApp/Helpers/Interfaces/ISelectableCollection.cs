
namespace SprinklerApp.Helpers.Interfaces
{
    public interface ISelectableCollection
    {
        long Id { get; set; }
        bool IsSelected { get; set; }
    }
}
