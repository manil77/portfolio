using Core.Entities;

namespace Application.RepositoryInterfaces
{
    public interface IProductRepo
    {
        void PostData(string a, string b);
        IEnumerable<Product> GetAllProducts();
    }
}
