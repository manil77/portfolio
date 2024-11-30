using Application.Interfaces;
using Core.Entities;
using Infrastructure.Interfaces.UnitOfWork;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
        public ProductService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }

        public void PostData(string value1, string value2)
        {
            _repositoryUnitOfWork.Product.PostData(value1, value2);
        }
        public IEnumerable<Product> GetAllProducts() {
            var result = _repositoryUnitOfWork.Product.GetAllProducts();
            return result;
        }
    }
}
