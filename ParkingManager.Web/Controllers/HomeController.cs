using ParkingManager.BLL.Repositories.MpesaStkModule;
using ParkingManager.DTO.MpesaStkModule;
using ParkingManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using SkiCareHMS.Data.DTOs.MpesaStkModule;
using System.Diagnostics;
using ParkingManager.BLL.Repositories.BookingModule;
using ParkingManager.DTO.DashboardModule;
using ParkingManager.DTO.BookingModule;
using ParkingManager.BLL.Repositories.ParkingSlotModule;
using ParkingManager.BLL.Repositories.ParkingChargeModule;

namespace ParkingManager.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPaymentRepository paymentRepository;

		private readonly IBookingRepository bookingRepository;

		private readonly IParkingSlotRepository parkingSlotRepository;

		private readonly IParkingChargeRepository parkingChargeRepository;

		private readonly ILogger<HomeController> _logger;
		public HomeController(IParkingChargeRepository parkingChargeRepository, IParkingSlotRepository parkingSlotRepository, IBookingRepository bookingRepository, IPaymentRepository paymentRepository, ILogger<HomeController> logger)
		{
			_logger = logger;

			this.paymentRepository = paymentRepository;

			this.bookingRepository = bookingRepository;

			this.parkingSlotRepository = parkingSlotRepository;

			this.parkingChargeRepository = parkingChargeRepository;
		}

		public async Task<IActionResult> Index()
		{
			try
			{
				var parkingFee = await parkingChargeRepository.GetRecentParkingFee();

				if (parkingFee == null)
				{
					ViewBag.Fees = 10;
				}
				if (parkingFee != null)
				{
					ViewBag.Fees = parkingFee.ParkingFee;
				}
				return View();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				TempData["Error"] = "Something went wrong";

				return RedirectToAction("Index", "Home", new { area = "" });
			}
		}

		public async Task<IActionResult> Receipt(Guid Id)
		{
			try
			{
				BookingProfileDTO data = new BookingProfileDTO()
				{

				};

				var parkingFee = await parkingChargeRepository.GetRecentParkingFee();

				if (parkingFee == null)
				{
					ViewBag.Fees = 10;
				}
				if (parkingFee != null)
				{
					ViewBag.Fees = parkingFee.ParkingFee;
				}

				var booking = bookingRepository.GetById(Id);

				var customer = bookingRepository.GetCustomerById(booking.CustomerId);

				var slots = await parkingSlotRepository.GetById(booking.ParkingSlotId);

				data.customerDetails = customer;

				data.bookingDetails = booking;

				data.parkingSlotDetails = slots;

				return View(data);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				TempData["Error"] = "Something went wrong";

				return RedirectToAction("Index", "Home", new { area = "" });
			}
		}
		public void SaveCallBack([FromBody] DarajaResponseAfterUserEntersPin darajaResponse)
		{
			try
			{
				if (darajaResponse.Body.stkCallback.ResultCode == 0)
				{
					var transaction = new MpesaPaymentDTO
					{
						CheckoutRequestID = darajaResponse.Body.stkCallback.CheckoutRequestID,

						MerchantRequestID = darajaResponse.Body.stkCallback.MerchantRequestID,

						ResultCode = darajaResponse.Body.stkCallback.ResultCode,

						ResultDesc = darajaResponse.Body.stkCallback.ResultDesc,

						Amount = Convert.ToDecimal(darajaResponse.Body.stkCallback.CallbackMetadata.Item.Where(p => p.Name.Contains("Amount")).FirstOrDefault().Value.ToString()),

						TransactionNumber = darajaResponse.Body.stkCallback.CallbackMetadata.Item.Where(p => p.Name.Contains("MpesaReceiptNumber")).FirstOrDefault().Value.ToString(),

						TransactionDate = darajaResponse.Body.stkCallback.CallbackMetadata.Item.Where(p => p.Name.Contains("TransactionDate")).FirstOrDefault().Value.ToString(),

						PhoneNumber = darajaResponse.Body.stkCallback.CallbackMetadata.Item.Where(p => p.Name.Contains("PhoneNumber")).FirstOrDefault().Value.ToString(),

					};

					paymentRepository.SaveSTKCallBackResponse(transaction);

				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				// return null;
			}
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
