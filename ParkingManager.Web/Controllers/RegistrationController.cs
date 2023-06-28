using Microsoft.AspNetCore.Mvc;

namespace ParkingManager.Web.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
