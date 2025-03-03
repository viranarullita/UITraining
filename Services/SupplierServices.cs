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
            var datas = _context.Suppliers.Select(x => new SelectListItem
            {
                Text = x.SupplierName,
                Value = x.Id.ToString()
            }).ToList();
            return datas;
        }
    }
}
