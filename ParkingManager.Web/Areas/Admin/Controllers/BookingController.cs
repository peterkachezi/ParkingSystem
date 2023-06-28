using ParkingManager.DAL.Modules;
using ParkingManager.Services.EmailModule;
using ParkingManager.Services.SMSModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParkingManager.BLL.Repositories.BookingModule;

namespace ParkingManager.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BookingController : Controller
	{

		private readonly IBookingRepository bookingRepository;

		private readonly UserManager<AppUser> userManager;
		public BookingController(

			IBookingRepository bookingRepository,

			IMailService mailService,

			UserManager<AppUser> userManager,

			IMessagingService messagingService

			)
		{

			this.userManager = userManager;


			this.bookingRepository = bookingRepository;
		}

		public async Task<IActionResult> Index()
		{
			try
			{
				var booking = (await bookingRepository.GetAll()).OrderBy(x => x.CreateDate).ToList();

				return View(booking);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				TempData["Error"] = "Something went wrong";

				return RedirectToAction("Login", "Account", new { area = "" });
			}
		}
		


	}
}
