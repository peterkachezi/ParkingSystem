using ParkingManager.DTO.ApplicationUserModule;
using ParkingManager.DTO.BookingModule;
using ParkingManager.DTO.CustomerModule;

namespace ParkingManager.DTO.DashboardModule
{
	public class DashboardDTO
    {
        public List<CustomerDTO> customers { get; set; }
        public List<ApplicationUserDTO> SystemsUsers { get; set; }
        public List<BookingDTO>  bookingDTOs { get; set; }

    }
}

