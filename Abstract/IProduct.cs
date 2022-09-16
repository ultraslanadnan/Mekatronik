using Mekatronik.Models;

namespace Mekatronik.Abstract
{
    public interface IProduct
    {
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        List<Product> GetListAllProduct();
        Product GetProductById(int id);
    }
}
