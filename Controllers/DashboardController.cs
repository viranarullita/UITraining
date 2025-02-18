using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UITraining.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
