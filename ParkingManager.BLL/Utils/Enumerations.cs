using System.ComponentModel;

namespace ParkingManager.BLL
{
    public class Enumerations
    {
        public enum EnquiryStatus
        {
            [Description("Pending")]
            Pending,

            [Description("Read")]
            Read
        }

        public enum Appointment
        {
            [Description("Pending")]
            Pending,

            [Description("Approved")]
            Approved,

            [Description("ApprovedAppointment")]
            ApprovedAppointment,
        }

    }
}
