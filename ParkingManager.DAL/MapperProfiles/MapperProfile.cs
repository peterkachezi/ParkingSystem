using AutoMapper;
using ParkingManager.DAL.Modules;
using ParkingManager.DTO.ApplicationUserModule;
using ParkingManager.DTO.BookingModule;
using ParkingManager.DTO.CustomerModule;
using ParkingManager.DTO.MpesaStkModule;
using ParkingManager.DTO.ParkingSlotModule;



namespace ParkingManager.DAL.MapperProfiles
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<Booking, BookingDTO>().ReverseMap();

			CreateMap<AppUser, ApplicationUserDTO>().ReverseMap();

			CreateMap<MpesaPayment, MpesaPaymentDTO>().ReverseMap();

			CreateMap<Customer, CustomerDTO>().ReverseMap();

			CreateMap<ParkingSlot, ParkingSlotDTO>().ReverseMap();

		}
	}
}
