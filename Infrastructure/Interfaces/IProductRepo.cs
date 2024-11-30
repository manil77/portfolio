using Core.Entities;

namespace Infrastructure.Interfaces
{
    public interface IProductRepo
    {
        void PostData(string a, string b);
        IEnumerable<Product> GetAllProducts();
    }
}
