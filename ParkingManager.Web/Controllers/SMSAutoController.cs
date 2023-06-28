using ParkingManager.DTO.AppointmentModule;
using ParkingManager.Services.SMSModule;
using Microsoft.AspNetCore.Mvc;

namespace ParkingManager.Web.Controllers
{
    public class SMSAutoController : Controller
    {
   

        private readonly IMessagingService messagingService;
        public SMSAutoController(IMessagingService messagingService)
        {

            this.messagingService = messagingService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var appointment = await GetData();

                return Json(appointment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<List<AppointmentDTO>> GetData()
        {
            //DateTime date = DateTime.Now;

            //int newday = date.Day + 2;

            //int month = date.Month;

            //int year = date.Year;

            //DateTime newdate = Convert.ToDateTime(month + "/" + newday + "/" + year);

            //var data = await appointmentRepository.GetAll();

            //var appointment = data.Where(x => x.AppointmentDate == newdate).ToList();

            //Console.WriteLine(date); // 2/22/2022 12:

            //var sendBulk = messagingService.AppointmentNotificationSMSAsync(appointment);

            return null;
        }

    }
}
