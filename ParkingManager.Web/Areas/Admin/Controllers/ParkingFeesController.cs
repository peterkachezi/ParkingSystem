using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParkingManager.BLL.Repositories.ParkingChargeModule;
using ParkingManager.DAL.Modules;
using ParkingManager.DTO.ParkingChargeModule;


namespace ParkingManager.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ParkingFeesController : Controller
	{
		private readonly IParkingChargeRepository parkingChargeRepository;

		private readonly UserManager<AppUser> userManager;
		public ParkingFeesController(IParkingChargeRepository parkingChargeRepository, UserManager<AppUser> userManager)
		{
			this.parkingChargeRepository = parkingChargeRepository;

			this.userManager = userManager;

		}
		public async Task<IActionResult> Index()
		{
			try
			{
				var parkingCharge = await parkingChargeRepository.GetAll();

				ViewBag.ParkingCharge = parkingCharge.Count();

				return View(parkingCharge);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}
		public async Task<IActionResult> Create(ParkingChargeDTO parkingChargeDTO)
		{
			try
			{
				var user = await userManager.FindByEmailAsync(User.Identity.Name);

				parkingChargeDTO.CreatedBy = user.Id;

				var results = await parkingChargeRepository.Create(parkingChargeDTO);

				if (results != null)
				{
					return Json(new { success = true, responseText = "Parking Charge has been successfully created" });
				}
				else
				{
					return Json(new { success = false, responseText = "Failed to create record" });
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return Json(new { success = false, responseText = "Something went wrong" });
			}
		}
		public async Task<IActionResult> Update(ParkingChargeDTO parkingChargeDTO)
		{
			try
			{
				var user = await userManager.FindByEmailAsync(User.Identity.Name);

				parkingChargeDTO.UpdatedBy = user.Id;

				var results = await parkingChargeRepository.Update(parkingChargeDTO);

				if (results != null)
				{
					return Json(new { success = true, responseText = "Parking Charge details has been successfully updated" });
				}
				else
				{
					return Json(new { success = false, responseText = "Failed to update record!" });
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return Json(new { success = false, responseText = "Something went wrong" });
			}
		}
		public async Task<ActionResult> Delete(Guid Id)
		{
			try
			{
				var results = await parkingChargeRepository.Delete(Id);

				if (results == true)
				{
					return Json(new { success = true, responseText = "Record  has been successfully deleted " });
				}
				else
				{
					return Json(new { success = false, responseText = "Record has not been deleted ,it could be in use by other files" });
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return Json(new { success = false, responseText = "Something went wrong" });
			}
		}
		public async Task<IActionResult> GetById(Guid Id)
		{
			try
			{
				var file = await parkingChargeRepository.GetById(Id);

				if (file != null)
				{
					return Json(new { data = file });
				}

				return Json(new { data = false });

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return Json(new { success = false, responseText = "Something went wrong" });
			}
		}
	}
}
