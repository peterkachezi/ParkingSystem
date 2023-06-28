using ParkingManager.BLL.Repositories.TimeSlotModule;
using ParkingManager.DAL.Modules;
using ParkingManager.DTO.AppointmentModule;
using ParkingManager.Services.EmailModule;
using ParkingManager.Services.SMSModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ParkingManager.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentsController : Controller
    {

        private readonly IMessagingService messagingService;

        private readonly IMailService mailService;

        private readonly ITimeSlotRepository timeSlotRepository;

        private readonly UserManager<AppUser> userManager;
        public AppointmentsController(

            ITimeSlotRepository timeSlotRepository, 

            IMailService mailService,

            UserManager<AppUser> userManager, 

            IMessagingService messagingService

            )
        {

            this.messagingService = messagingService;

            this.userManager = userManager;

            this.mailService = mailService;

            this.timeSlotRepository = timeSlotRepository;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                TempData["Error"] = "Something went wrong";

                return RedirectToAction("Login", "Account", new { area = "" });
            }
        }
        public IActionResult Test()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                TempData["Error"] = "Something went wrong";

                return RedirectToAction("Login", "Account", new { area = "" });
            }
        }
        public IActionResult ApprovedAppointments()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                TempData["Error"] = "Something went wrong";

                return RedirectToAction("Login", "Account", new { area = "" });
            }
        }
        //public async Task<IActionResult> GetNewAppointments()
        //{
        //    try
        //    {
        //        var slots = (await appointmentRepository.GetAll()).Where(x => x.Status == 0).ToList();

        //        return Json(new { data = slots });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        TempData["Error"] = "Something went wrong";

        //        return RedirectToAction("Login", "Account", new { area = "" });
        //    }
        //}
        //public async Task<IActionResult> GetAppointments()
        //{
        //    try
        //    {
        //        var slots = (await appointmentRepository.GetAll()).Where(x => x.Status == 1).ToList();

        //        return Json(new { data = slots });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        TempData["Error"] = "Something went wrong";

        //        return RedirectToAction("Login", "Account", new { area = "" });
        //    }
        //}
        [HttpGet]
        //public async Task<ActionResult> GetAppointments1()
        //{
        //    try
        //    {
        //        var appointments = (await appointmentRepository.GetAll()).Where(x => x.IsCompleted == 0).OrderBy(x => x.CreateDate);

        //        var withSequence = appointments.AsEnumerable()

        //                                .Select((a, index) => new
        //                                {
        //                                    a.Id,

        //                                    a.Status,

        //                                    a.CreateDate,

        //                                    a.AppointmentDate,

        //                                    a.FirstName,

        //                                    a.LastName,

        //                                    a.FullName,

        //                                    a.ParkingSlotId,

        //                                    a.FromTime,

        //                                    a.ToTime,

        //                                    a.TimeSlot,

        //                                    a.NewAppDate,

        //                                    a.NewCreateDate,

        //                                    SequenceNo = index + 1
        //                                }); ;

        //        return Ok(withSequence);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        TempData["Error"] = "Something went wrong";

        //        return RedirectToAction("Login", "Account", new { area = "" });
        //    }

        //}
 
        //public async Task<IActionResult> GetSumAppointments()
        //{
        //    var appointment = (await appointmentRepository.GetAll()).Where(x => x.Status == 0).Count();

        //    return Json(new { data = appointment });
        //}
        //public async Task<IActionResult> GetById(Guid Id)
        //{
        //    try
        //    {
        //        var appointment = await appointmentRepository.GetById(Id);

        //        if (appointment != null)
        //        {
        //            var file = new AppointmentDTO
        //            {
        //                Id = appointment.Id,

        //                Status = appointment.Status,

        //                CreateDate = appointment.CreateDate,

        //                AppointmentDate = appointment.AppointmentDate,

        //                FirstName = appointment.FirstName,

        //                PhoneNumber = appointment.PhoneNumber,

        //                Email = appointment.Email,

        //                LastName = appointment.LastName,

        //                ParkingSlotId = appointment.ParkingSlotId,

        //                TimeSlot = appointment.TimeSlot,
        //            };

        //            return Json(new { data = file });
        //        }

        //        return Json(new { data = false });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        TempData["Error"] = "Something went wrong";

        //        return RedirectToAction("Login", "Account", new { area = "" });
        //    }

        //}
        //public async Task<IActionResult> ApproveAppointment(AppointmentDTO appointmentDTO)
        //{
        //    //try
        //    //{
        //    //    var user = await userManager.FindByEmailAsync(User.Identity.Name);

        //    //    appointmentDTO.ApprovedBy = user.Id;

        //    //    var result = await appointmentRepository.ApproveAppointment(appointmentDTO);

        //    //    if (result == true)
        //    //    {
        //    //        //var get_appointment = (await appointmentRepository.GetById(appointmentDTO.Id));

        //    //        //var url = "https://www.ParkingManager.co.ke/VideoChat";

        //    //        //var appointment = new AppointmentDTO()
        //    //        //{
        //    //        //    FirstName = get_appointment.FirstName,

        //    //        //    VideoURL = url,

        //    //        //    PhoneNumber = get_appointment.PhoneNumber,

        //    //        //    AppointmentDate = get_appointment.AppointmentDate,

        //    //        //    Email = get_appointment.Email,

        //    //        //    CreateDate = get_appointment.CreateDate,

        //    //        //    FromTime = get_appointment.FromTime,

        //    //        //    ToTime = get_appointment.ToTime,

        //    //        //    TimeSlot = get_appointment.FromTime.ToString("h:mm tt") + " - " + get_appointment.ToTime.ToString("h:mm tt"),
        //    //        //};

        //    //        //var sendSMS = messagingService.ApprovalNotificationSMS(appointment);

        //    //        //var sendMail = mailService.AppointmentApprovalNotification(appointment);

        //    //        return Json(new { success = true, responseText = "Appointment has been successfully approved" });
        //    //    }
        //    //    else
        //    //    {
        //    //        return Json(new { success = false, responseText = "Appointment has not been  approved" });
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine(ex.Message);

        //    //    return null;
        //    //}

        //}
      
        //public async Task<ActionResult> GetAppointmentsSummary()
        //{
        //    try
        //    {
        //        var enquiries = (await appointmentRepository.GetAll()).Where(x => x.Status == 0).Take(6).OrderBy(x => x.CreateDate);

        //        return Json(enquiries.Select(x => new
        //        {
        //            x.Id,

        //            x.FullName,

        //            x.PhoneNumber,

        //            x.Email,

        //            x.NewCreateDate,

        //        }).ToList());

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return null;
        //    }
        //}
        public async Task<IActionResult> GetSlotsByAppointmentDate(DateTime AppointmentDate)
        {
            try
            {
                var getSlot = await timeSlotRepository.GetAll();

                var timeslot = getSlot.Where(x => x.IsBooked == 0 && x.AppointmentDate == AppointmentDate).OrderBy(x => x.FromTime);

                return Json(timeslot.Select(x => new
                {
                    SlotId = x.Id,

                    SlotName = x.TimeSlot

                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                TempData["Error"] = "Something went wrong";

                return RedirectToAction("", "Home", new { area = "" });
            }

        }
    }
}
