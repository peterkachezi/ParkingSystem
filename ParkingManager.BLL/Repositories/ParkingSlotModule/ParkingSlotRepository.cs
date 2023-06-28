using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParkingManager.DAL.Modules;
using ParkingManager.DTO.ParkingSlotModule;

namespace ParkingManager.BLL.Repositories.ParkingSlotModule
{
    public class ParkingSlotRepository : IParkingSlotRepository
    {
        private readonly ApplicationDbContext context;

        private IMapper mapper;
        public ParkingSlotRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;

            this.mapper = mapper;
        }
        public async Task<ParkingSlotDTO> Create(ParkingSlotDTO parkingSlotDTO)
        {
            try
            {
                var isCustomerExist = await context.ParkingSlots.AnyAsync(x => x.Name == parkingSlotDTO.Name);

                if (isCustomerExist == false)
                {
                    parkingSlotDTO.Id = Guid.NewGuid();

                    parkingSlotDTO.CreateDate = DateTime.Now;

                    parkingSlotDTO.Status =1;

                    var data = mapper.Map<ParkingSlot>(parkingSlotDTO);

                    context.ParkingSlots.Add(data);

                    await context.SaveChangesAsync();

                    return parkingSlotDTO;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
        public async Task<ParkingSlotDTO> Update(ParkingSlotDTO parkingSlotDTO)
        {
            try
            {
                var getSlolt = await context.ParkingSlots.FindAsync(parkingSlotDTO.Id);

                if (getSlolt != null)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        getSlolt.Name = parkingSlotDTO.Name;          

                        getSlolt.UpdatedBy = parkingSlotDTO.UpdatedBy;

                        getSlolt.ModifiedDate = DateTime.Now;

                        transaction.Commit();
                    }

                    await context.SaveChangesAsync();

                    return parkingSlotDTO;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<List<ParkingSlotDTO>> GetAll()
        {
            try
            {
                var data = await context.ParkingSlots.AsNoTracking().ToListAsync();

                var list = mapper.Map<List<ParkingSlotDTO>>(data);

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ParkingSlotDTO> GetById(Guid Id)
        {
            try
            {
                var data = await context.ParkingSlots.FindAsync(Id);

                var list = mapper.Map<ParkingSlotDTO>(data);

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool result = false;

                var getSlolt = await context.ParkingSlots.FindAsync(Id);

                if (getSlolt != null)
                {
                    context.ParkingSlots.Remove(getSlolt);

                    await context.SaveChangesAsync();

                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
