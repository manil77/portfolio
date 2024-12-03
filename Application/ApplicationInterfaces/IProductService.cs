using Core.Entities;

namespace Application.ApplicationInterfaces
{
    public interface IProductService
    {
        void PostData(string value1, string value2);
        IEnumerable<Product> GetAllProducts();
    }
}
