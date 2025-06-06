
namespace SprinklerApp.Helpers.Interfaces
{
    public interface ISelectableCollection
    {
        long Id { get; }
        bool IsSelected { get; set; }
    }
}
