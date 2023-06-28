using ParkingManager.BLL.Repositories.ApplicationUserModule;
using ParkingManager.BLL.Repositories.MpesaStkModule;
using ParkingManager.DTO.DashboardModule;
using Microsoft.AspNetCore.Mvc;
using ParkingManager.BLL.Repositories.BookingModule;
using ParkingManager.BLL.Repositories.ParkingSlotModule;

namespace ParkingManager.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DashboardController : Controller
	{
		private readonly IPaymentRepository paymentRepository;

		private readonly IBookingRepository bookingRepository;

		private readonly IParkingSlotRepository parkingSlotRepository;

		private readonly IApplicationUserRepository applicationUserRepository;
		public DashboardController(

			IApplicationUserRepository applicationUserRepository,

			IPaymentRepository paymentRepository,

			 IBookingRepository bookingRepository,

			 IParkingSlotRepository parkingSlotRepository

		   )
		{

			this.paymentRepository = paymentRepository;

			this.applicationUserRepository = applicationUserRepository;

			this.bookingRepository = bookingRepository;

			this.parkingSlotRepository = parkingSlotRepository;
		}

		public async Task<IActionResult> Index()
		{
			try
			{

				var revenue = paymentRepository.GetTotalRevenue();

				var users = await applicationUserRepository.GetAllUsers();

				var customers = bookingRepository.GetAllCustomer().ToList();

				var bookings = await bookingRepository.GetAll();

				var parkingLots = await parkingSlotRepository.GetAll();

				ViewBag.Revenue = revenue;

				ViewBag.Customers = customers.Count();

				ViewBag.Booking = bookings.Count();

				ViewBag.AvailableParkingLots = parkingLots.Where(x => x.IsBooked == false).Count();

				ViewBag.BookedParkingLots = parkingLots.Where(x => x.IsBooked == true).Count();

				DashboardDTO data = new DashboardDTO()
				{

				};

				data.customers = customers.Take(5).OrderBy(x => x.CreateDate).ToList();

				data.bookingDTOs = bookings.Take(5).OrderBy(x => x.CreateDate).ToList();

				data.SystemsUsers = users.Where(x => x.RoleName == "Doctor").Take(5).ToList();

				return View(data);

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
