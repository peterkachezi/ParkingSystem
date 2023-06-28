using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingManager.DTO.ParkingSlotModule;
using ParkingManager.DTO.PatientModule;
using ParkingManager.DTO.TimeSlotModule;

namespace ParkingManager.DTO.BookingModule
{
    public class BookingProfileDTO
    {
        public CustomerDTO customerDetails { get; set; }
        public BookingDTO bookingDetails { get; set; }
        public ParkingSlotDTO  parkingSlotDetails { get; set; }
    //public List<PrescriptionDetailDTO> prescriptionDetails { get; set; }
}
}
