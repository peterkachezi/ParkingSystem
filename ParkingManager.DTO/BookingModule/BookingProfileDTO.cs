using ParkingManager.DTO.CustomerModule;
using ParkingManager.DTO.ParkingSlotModule;

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
