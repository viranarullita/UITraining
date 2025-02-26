using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;

namespace UITraining.Services
{
    public class ProductServices : IProduct
    {
        private readonly ApplicationContext _context;

        public ProductServices(ApplicationContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            var products = _context.Products.Where(x => x.ProductStatus != ProductStatus.deleted).ToList();
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.Where(x => x.Id == id && x.ProductStatus != ProductStatus.deleted).FirstOrDefault();

            if (product == null)
            {
                return new Product();
            }

            return product;
        }

        public bool EditProduct(Product product)
        {
            var data = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (data == null)
            {
                return false;
            }

            data.Name = product.Name;
            data.Stock = product.Stock;
            data.Description = product.Description;
            data.Price = product.Price;
            data.ProductStatus = product.ProductStatus;

            _context.Products.Update(data);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
    }
}
