
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParkingManager.DAL.Modules;
using ParkingManager.DTO.ParkingChargeModule;

namespace ParkingManager.BLL.Repositories.ParkingChargeModule
{
	public class ParkingChargeRepository : IParkingChargeRepository
	{
		private readonly ApplicationDbContext context;

		private IMapper mapper;
		public ParkingChargeRepository(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;

			this.mapper = mapper;
		}
		public async Task<ParkingChargeDTO> Create(ParkingChargeDTO parkingChargeDTO)
		{
			try
			{
				parkingChargeDTO.Id = Guid.NewGuid();

				parkingChargeDTO.CreateDate = DateTime.Now;

				var data = mapper.Map<ParkingCharge>(parkingChargeDTO);

				context.ParkingCharges.Add(data);

				await context.SaveChangesAsync();

				return parkingChargeDTO;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}

		}
		public async Task<ParkingChargeDTO> Update(ParkingChargeDTO parkingChargeDTO)
		{
			try
			{
				var getSlolt = await context.ParkingCharges.FindAsync(parkingChargeDTO.Id);

				if (getSlolt != null)
				{
					using (var transaction = context.Database.BeginTransaction())
					{
						getSlolt.ParkingFee = parkingChargeDTO.ParkingFee;

						getSlolt.UpdatedBy = parkingChargeDTO.UpdatedBy;

						getSlolt.ModifiedDate = DateTime.Now;

						transaction.Commit();
					}

					await context.SaveChangesAsync();

					return parkingChargeDTO;
				}

				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}
		public async Task<List<ParkingChargeDTO>> GetAll()
		{
			try
			{
				var data = await context.ParkingCharges.AsNoTracking().ToListAsync();

				var list = mapper.Map<List<ParkingChargeDTO>>(data);

				return list;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}

		public async Task<ParkingChargeDTO> GetRecentParkingFee()
		{
			try
			{
				var data = await context.ParkingCharges.FirstOrDefaultAsync();

				var list = mapper.Map<ParkingChargeDTO>(data);

				return list;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}
		public async Task<ParkingChargeDTO> GetById(Guid Id)
		{
			try
			{
				var data = await context.ParkingCharges.FindAsync(Id);

				var list = mapper.Map<ParkingChargeDTO>(data);

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

				var getSlolt = await context.ParkingCharges.FindAsync(Id);

				if (getSlolt != null)
				{
					context.ParkingCharges.Remove(getSlolt);

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
