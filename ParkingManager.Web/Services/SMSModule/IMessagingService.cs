
using ParkingManager.DTO.AppointmentModule;
using System.Threading.Tasks;

namespace ParkingManager.Services.SMSModule
{
    public interface IMessagingService
    {
        Task<AppointmentDTO> ApprovalNotificationSMS(AppointmentDTO appointmentDTO);
        Task<List<AppointmentDTO>> AppointmentNotificationSMSAsync(List<AppointmentDTO> appointmentDTO);
        Task<AppointmentDTO> AppointmentBookingNotificationSMS(AppointmentDTO appointmentDTO);
    }
}