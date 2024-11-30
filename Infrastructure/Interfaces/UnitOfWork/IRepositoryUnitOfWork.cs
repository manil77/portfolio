using Infrastructure.Repository;

namespace Infrastructure.Interfaces.UnitOfWork
{
    public interface IRepositoryUnitOfWork
    {
        IProductRepo Product { get; }
    }
}
