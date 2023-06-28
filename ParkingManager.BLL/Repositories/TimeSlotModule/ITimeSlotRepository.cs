using ParkingManager.DTO.TimeSlotModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManager.BLL.Repositories.TimeSlotModule
{
    public interface ITimeSlotRepository
    {
        Task<TimeSlotDTO> Create(TimeSlotDTO timeSlotDTO);
        Task<TimeSlotDTO> Update(TimeSlotDTO timeSlotDTO);
        Task<bool> Delete(Guid Id);    
        Task<List<TimeSlotDTO>> GetAll();
        List<TimeSlotDTO> GetAllGroupByDate();
        TimeSlotDTO GetById(Guid Id);
    }
}