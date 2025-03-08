using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplier _supplier;

        public SupplierController(ISupplier supplier)
        {
            _supplier = supplier;
        }

        public IActionResult Index()
        {
            var suppliers = _supplier.GetAllSupplier();
            return View(suppliers);
        }

        public IActionResult Edit(int id)
        {
            var product = _supplier.GetByIdSupplier(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(SupplierDTO supplier)
        {
            if (supplier.Id == 0)
            {
                var addSupplier = _supplier.AddSupplier(supplier);
                if (addSupplier)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var updateSupplier = _supplier.EditSupplier(supplier);
                if (updateSupplier)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var deleteSupplier = _supplier.DeleteSupplier(id);
            if (deleteSupplier)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Gagal menghapus data Supplier!");
        }
    }
}