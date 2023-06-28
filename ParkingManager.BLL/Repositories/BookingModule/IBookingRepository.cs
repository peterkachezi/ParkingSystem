using ParkingManager.DTO.BookingModule;
using ParkingManager.DTO.PatientModule;

namespace ParkingManager.BLL.Repositories.BookingModule
{
    public interface IBookingRepository
    {
        Task<BookingDTO> Create(BookingDTO bookingDTO);
        Task<BookingDTO> Update(BookingDTO bookingDTO);
        Task<bool> Delete(Guid Id);
        Task<List<BookingDTO>> GetAll();
        BookingDTO GetById(Guid Id);
        CustomerDTO GetCustomerById(Guid Id);
        List<CustomerDTO> GetAllCustomer();
    }
}