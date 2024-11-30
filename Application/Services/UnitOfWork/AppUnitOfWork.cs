using Application.Interfaces;
using Application.Interfaces.UnitOfWork;
using Infrastructure.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
