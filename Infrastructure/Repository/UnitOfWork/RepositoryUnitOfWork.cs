using Infrastructure.SQLHelper;
using Application.RepositoryInterfaces;
using Application.RepositoryInterfaces.UnitOfWork;

namespace Infrastructure.Repository.UnitOfWork
{
    public class RepositoryUnitOfWork : IRepositoryUnitOfWork
    {
        public IProductRepo _repoProduct;
        public ISQLHelper _sqlHelper;

        public RepositoryUnitOfWork(ISQLHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public IProductRepo Product
        {
            get
            {
                if (_repoProduct is null)
                {
                    return new ProductRepo(_sqlHelper);
                }
                return _repoProduct;
            }
        }
    }
}
