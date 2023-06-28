using Microsoft.AspNetCore.Mvc;
using ParkingManager.BLL.Repositories.BookingModule;

namespace ParkingManager.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CustomersController : Controller
	{
		private readonly IBookingRepository bookingRepository;

		public CustomersController(IBookingRepository bookingRepository)
		{
			this.bookingRepository = bookingRepository;
		}
		public IActionResult Index()
		{
			var customers = bookingRepository.GetAllCustomer().OrderBy(x => x.CreateDate).ToList();

			return View(customers);
		}
	}
}
