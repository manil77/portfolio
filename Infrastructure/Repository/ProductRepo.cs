using Core.Entities;
using Infrastructure.Interfaces;
using DapperHelper = Infrastructure.SQLHelper.ISQLHelper;

namespace Infrastructure.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly DapperHelper _sqlHelper;
        public ProductRepo(DapperHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }
        public void PostData(string a, string b)
        {
            var result = _sqlHelper.ExecuteSqlScript<Product>("insert into products");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var result = _sqlHelper.ExecuteSqlScript<Product>("select * from products");
            return result;  
        }

    }
}
