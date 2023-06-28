

using ParkingManager.DTO.ApplicationUserModule;
using ParkingManager.DTO.AppointmentModule;


namespace ParkingManager.Services.EmailModule
{
    public interface IMailService
    {
        bool AppointmentEmailNotification(AppointmentDTO appointmentDTO);
        Task<bool> RescheduleAppointmentEmailNotificationAsync(AppointmentDTO appointmentDTO);
        Task<bool> ParkingManagerEmailNotification(AppointmentDTO appointmentDTO);
        bool PasswordResetEmailNotification(ResetPasswordDTO resetPasswordDTO);
        bool AccountEmailNotification(ApplicationUserDTO applicationUserDTO);
        bool AppointmentApprovalNotification(AppointmentDTO appointmentDTO);
    }
}