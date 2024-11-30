using Infrastructure.Interfaces;
using Infrastructure.Interfaces.UnitOfWork;
using Infrastructure.SQLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
