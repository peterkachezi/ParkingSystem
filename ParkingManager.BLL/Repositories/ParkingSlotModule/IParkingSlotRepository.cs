using ParkingManager.DTO.ParkingSlotModule;

namespace ParkingManager.BLL.Repositories.ParkingSlotModule
{
    public interface IParkingSlotRepository
    {
        Task<ParkingSlotDTO> Create(ParkingSlotDTO parkingSlotDTO);
        Task<ParkingSlotDTO> Update(ParkingSlotDTO parkingSlotDTO);
        Task<List<ParkingSlotDTO>> GetAll();
        Task<ParkingSlotDTO> GetById(Guid Id);
        Task<bool> Delete(Guid Id);
        Task<bool> CheckOut(Guid Id);
    }
}