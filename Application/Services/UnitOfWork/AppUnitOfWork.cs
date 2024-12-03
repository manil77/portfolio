using Application.ApplicationInterfaces;
using Application.ApplicationInterfaces.UnitOfWork;
using Application.RepositoryInterfaces.UnitOfWork;

namespace Application.Services.UnitOfWork
{
    public class AppUnitOfWork:IAppUnitOfWork
    {

        public IProductService _appProduct;
        private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

        public AppUnitOfWork(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }

        public IProductService Product
        {
            get {
                if (_appProduct is null) { 
                    return new ProductService(_repositoryUnitOfWork);
                }
                return _appProduct;
            }
        }
    }
}
