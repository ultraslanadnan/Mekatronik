
using Mekatronik.Abstract;
using Mekatronik.Data;
using Mekatronik.Models;

namespace Mekatronik.Concrete
{

    public class ProductManager : IProduct
    {




        public void CreateProduct(Product product)
        {
            using (var c = new DataContext())
            {
                c.Products.Add(product);
                c.SaveChanges();

            }

        }

        public void DeleteProduct(Product product)
        {
            using (var c = new DataContext())
            {
                c.Products.Remove(product);
                c.SaveChanges();
            }

        }

        public List<Product> GetListAllProduct()
        {
            using (var c = new DataContext())
            {
                return c.Products.ToList();
            }

        }

        public Product GetProductById(int id)
        {
            using (var c = new DataContext())
            {
                return c.Products.Find(id);
            }

        }

        public void UpdateProduct(Product product)
        {
            using (var c = new DataContext())
            {
                c.Products.Update(product);
                c.SaveChanges();
            }

        }
    }
}
