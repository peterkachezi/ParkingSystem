using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManager.Controllers
{
    public class Opening_HoursController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
