using ParkingManager.DTO.BookingModule;
using ParkingManager.DTO.CustomerModule;
using ParkingManager.DTO.ParkingChargeModule;

namespace ParkingManager.BLL.Repositories.ParkingChargeModule
{
	public interface IParkingChargeRepository
	{
		Task<ParkingChargeDTO> Create(ParkingChargeDTO parkingChargeDTO);
		Task<ParkingChargeDTO> Update(ParkingChargeDTO parkingChargeDTO);
		Task<bool> Delete(Guid Id);
		Task<List<ParkingChargeDTO>> GetAll();
		Task<ParkingChargeDTO> GetById(Guid Id);
		Task<ParkingChargeDTO> GetRecentParkingFee();
	}
}