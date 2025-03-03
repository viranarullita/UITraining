using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _interface;
        private readonly ISupplier _supplier;

        public ProductController(IProduct interfaces, ISupplier supplier)
        {
            _interface = interfaces;
            _supplier = supplier;
        }

        public IActionResult Index()
        {
            var products = _interface.GetAllProducts();
            return View(products);
        }

        public IActionResult Edit(int Id)
        {
            ViewBag.Supplier = _supplier.Suppliers();
            var product = _interface.GetProductById(Id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductDTO product)
        {
            if (product.Id == 0)
            {
                var addProduct = _interface.AddProduct(product);
                if (addProduct)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var EditProduct = _interface.EditProduct(product);
                if (EditProduct)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        public IActionResult Delete(int Id)
        {
            var deleteProduct = _interface.DeleteProduct(Id);
            if (deleteProduct)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Gagal menghapus data produk!");
        }
    }
}
