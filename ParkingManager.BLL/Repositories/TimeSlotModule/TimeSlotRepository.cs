using AutoMapper;
using ParkingManager.DAL.Modules;
using ParkingManager.DTO.TimeSlotModule;
using Microsoft.EntityFrameworkCore;

namespace ParkingManager.BLL.Repositories.TimeSlotModule
{
	public class TimeSlotRepository : ITimeSlotRepository
	{
		private readonly ApplicationDbContext context;

		private readonly IMapper mapper;
		public TimeSlotRepository(IMapper mapper, ApplicationDbContext context)
		{
			this.context = context;

			this.mapper = mapper;
		}
		public async Task<TimeSlotDTO> Create(TimeSlotDTO timeSlotDTO)
		{
			try
			{
				timeSlotDTO.Id = Guid.NewGuid();

				timeSlotDTO.IsBooked = 0;

				timeSlotDTO.CreateDate = DateTime.Now;

				timeSlotDTO.UpdatedBy = timeSlotDTO.CreatedBy;

				var slot = mapper.Map<ParkingSlot>(timeSlotDTO);

				context.ParkingSlots.Add(slot);

				await context.SaveChangesAsync();

				return timeSlotDTO;
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

				var slot = await context.ParkingSlots.FindAsync(Id);

				if (slot != null)
				{
					context.ParkingSlots.Remove(slot);

					await context.SaveChangesAsync();

					return true;
				}

				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return false;
			}
		}

		public async Task<List<TimeSlotDTO>> GetAll()
		{
			try
			{
				var data = (from slots in context.ParkingSlots

							join user in context.AppUsers on slots.CreatedBy equals user.Id

							select new TimeSlotDTO
							{
								Id = slots.Id,

								//IsBooked = slots.IsBooked,

								//AppointmentDate = slots.AppointmentDate,

								//CreateDate = slots.CreateDate,

								//FromTime = slots.FromTime,

								//ToTime = slots.ToTime,

								CreatedBy = slots.CreatedBy,

								UpdatedBy = slots.UpdatedBy,

								CreatedByName = user.FirstName + " " + user.LastName,

							}).ToListAsync();

				return await data;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}

		public List<TimeSlotDTO> GetAllGroupByDate()
		{
			try
			{
				var allSlots = (from timeSlots in context.ParkingSlots

								join user in context.AppUsers on timeSlots.CreatedBy equals user.Id

								select new TimeSlotDTO
								{
									Id = timeSlots.Id,

									CreateDate = timeSlots.CreateDate,

									CreatedByName = user.FirstName + " " + user.LastName,

								}).ToList();


				var uniqueSlots = from p in allSlots

								  group p by new { p.AppointmentDate } //or group by new {p.ID, p.Name, p.Whatever}

								   into mygroup

								  select mygroup.FirstOrDefault();

				var slots = new List<TimeSlotDTO>();

				foreach (var item in uniqueSlots)
				{
					var data = new TimeSlotDTO()
					{
						Id = item.Id,

						CreateDate = item.CreateDate,

						CreatedByName = item.CreatedByName,

						AppointmentDate = item.AppointmentDate
					};

					slots.Add(data);

				}

				return slots;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}

		public TimeSlotDTO GetById(Guid Id)
		{
			try
			{
				var data = context.ParkingSlots.Find(Id);

				var slot = mapper.Map<TimeSlotDTO>(data);

				return slot;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}

		public async Task<TimeSlotDTO> Update(TimeSlotDTO timeSlotDTO)
		{
			try
			{
				var getData = await context.ParkingSlots.FindAsync(timeSlotDTO.Id);

				if (getData != null)
				{
					using (var transaction = context.Database.BeginTransaction())
					{
						var data = getData;

						mapper.Map(timeSlotDTO, data);

						context.Entry(data).State = EntityState.Modified;

						transaction.Commit();

						await context.SaveChangesAsync();
					}

					return timeSlotDTO;
				}

				return null;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}
	}
}
