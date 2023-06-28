using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParkingManager.BLL.Repositories.BookingModule;
using ParkingManager.BLL.Repositories.ParkingSlotModule;
using ParkingManager.DAL.Modules;
using ParkingManager.DTO.BookingModule;
using ParkingManager.DTO.ParkingSlotModule;

namespace ParkingManager.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository bookingRepository;

        private readonly UserManager<AppUser> userManager;

        public BookingController(UserManager<AppUser> userManager,IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;

            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
       

        public async Task<IActionResult> Create(BookingDTO bookingDTO)
        {
            try
            {
                var transId = "QE19A15WZ3";

                if (bookingDTO.TransactionNumber != transId)
                {
                    return Json(new { success = false, responseText = "Sorry , your payment transaction no could not be found " });
                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                bookingDTO.CreatedBy = user.Id;

                var results = await bookingRepository.Create(bookingDTO);

                if (results != null)
                {
                    return Json(new { success = true, responseText = bookingDTO.Id });
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to book parking" });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return Json(new { success = false, responseText = "Something went wrong" });
            }
        }

    }
}
