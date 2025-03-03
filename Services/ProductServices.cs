using Microsoft.EntityFrameworkCore;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Services
{
    public class ProductServices : IProduct
    {
        private readonly ApplicationContext _context;

        public ProductServices(ApplicationContext context)
        {
            _context = context;
        }

        public List<ProductDTO> GetAllProducts()
        {
            var products = _context.Products
                .Include(x => x.Supplier)
                .Where(x => x.ProductStatus != GeneralStatusData.deleted)
                .Select(x => new ProductDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Stock = x.Stock,
                    ProductStatus = x.ProductStatus,
                    SupplierName = x.Supplier.SupplierName,
                })
                .ToList();
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.Where(x => x.Id == id && x.ProductStatus != GeneralStatusData.deleted).FirstOrDefault();

            if (product == null)
            {
                return new Product();
            }

            return product;
        }

        public bool AddProduct(ProductDTO product)
        {
            var datas = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
                ProductStatus = product.ProductStatus,
                IdSupplier = product.IdSupplier
            };
 
            _context.Add(datas);
            _context.SaveChanges();
            return true;
        }

        public bool EditProduct(ProductDTO product)
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

            product.ProductStatus = GeneralStatusData.deleted; 
            _context.Products.Update(product);
            _context.SaveChanges();
            return true;
        }
    }
}
