using ParkingManager.DTO.ApplicationUserModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManager.BLL.Repositories.ApplicationUserModule
{
    public interface IApplicationUserRepository
    {
        Task<List<RoleDTO>> GetAll();
        Task<List<ApplicationUserDTO>> GetAllUsers();
        Task<ApplicationUserDTO> GetById(string Id);
        Task<bool> Delete(string Id);
        Task<bool> DisableAccount(string Id);
        Task<bool> EnableAccount(string Id);
        Task<ApplicationUserDTO> Update(ApplicationUserDTO applicationUserDTO);
    }
}