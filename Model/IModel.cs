using Model.Dto;

namespace Model
{
    public interface IModel
    {
        long Id { get; }

        IDto ToDto();
    }
}