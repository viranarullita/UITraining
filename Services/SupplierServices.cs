using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using static UITraining.Models.GeneralStatus;
using UITraining.Models.DTO;

namespace UITraining.Services
{
    public class SupplierServices : ISupplier
    {
        private readonly ApplicationContext _context;

        public SupplierServices(ApplicationContext context)
        {
            _context = context;
        }

        public List<SelectListItem> Suppliers()
        {
            var datas = _context.Suppliers.Where(x => x.SupplierStatus != GeneralStatusData.suspended)
            .Select(x => new SelectListItem
            {
                Text = x.SupplierName,
                Value = x.Id.ToString()
            }).ToList();
            return datas;
        }

        public Supplier GetByIdSupplier(int id)
        {
            var supplier = _context.Suppliers
                .Where(x => x.Id == id && x.SupplierStatus != GeneralStatusData.suspended)
                .FirstOrDefault();

            if (supplier == null)
            {
                return new Supplier();
            }
            return supplier;
        }

        public List<SupplierDTO> GetAllSupplier()
        {
            var suppliers = _context.Suppliers.Where(x => x.SupplierStatus != GeneralStatusData.suspended)
                .Select(x => new SupplierDTO
                {
                    Id = x.Id,
                    SupplierName = x.SupplierName,
                    SupplierAddress = x.SupplierAddress,
                    SupplierStatus = x.SupplierStatus
                }).ToList();
            return suppliers;
        }

        public bool AddSupplier(SupplierDTO supplier)
        {
            try
            {
                var insertSupplier = new Supplier
                {
                    SupplierName = supplier.SupplierName,
                    SupplierAddress = supplier.SupplierAddress,
                    SupplierStatus = supplier.SupplierStatus
                };
                _context.Suppliers.Add(insertSupplier);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditSupplier(SupplierDTO supplier)
        {
            try
            {
                var dataSupplier = _context.Suppliers.FirstOrDefault(x => x.Id == supplier.Id);
                if (dataSupplier != null)
                {
                    dataSupplier.SupplierName = supplier.SupplierName;
                    dataSupplier.SupplierAddress = supplier.SupplierAddress;
                    dataSupplier.SupplierStatus = supplier.SupplierStatus;

                    _context.Suppliers.Update(dataSupplier);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteSupplier(int id)
        {
            try
            {
                var dataSupplier = _context.Suppliers.FirstOrDefault(DEL => DEL.Id == id);
                if (dataSupplier != null)
                {
                    dataSupplier.SupplierStatus = GeneralStatusData.suspended;
                    _context.Suppliers.Update(dataSupplier);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
