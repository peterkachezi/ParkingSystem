using ParkingManager.BLL.Repositories.ApplicationUserModule;
using ParkingManager.BLL.Repositories.MpesaStkModule;
using ParkingManager.DTO.DashboardModule;
using Microsoft.AspNetCore.Mvc;
using ParkingManager.BLL.Repositories.BookingModule;

namespace ParkingManager.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DashboardController : Controller
	{
		private readonly IPaymentRepository paymentRepository;

		private readonly IBookingRepository bookingRepository;

		private readonly IApplicationUserRepository applicationUserRepository;
		public DashboardController(

			IApplicationUserRepository applicationUserRepository,

			IPaymentRepository paymentRepository,

			 IBookingRepository bookingRepository

		   )
		{

			this.paymentRepository = paymentRepository;

			this.applicationUserRepository = applicationUserRepository;

			this.bookingRepository = bookingRepository;
		}

		public async Task<IActionResult> Index()
		{
			try
			{

				var revenue = paymentRepository.GetTotalRevenue();

				var users = await applicationUserRepository.GetAllUsers();

				ViewBag.Revenue = revenue;

				DashboardDTO data = new DashboardDTO()
				{

				};

				data.customers = bookingRepository.GetAllCustomer().Take(5).OrderBy(x => x.CreateDate).ToList();

				data.bookingDTOs = (await bookingRepository.GetAll()).Take(5).OrderBy(x => x.CreateDate).ToList();

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
