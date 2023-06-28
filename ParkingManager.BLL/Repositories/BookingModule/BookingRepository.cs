using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParkingManager.BLL.Utils;
using ParkingManager.DAL.Modules;
using ParkingManager.DTO.BookingModule;
using ParkingManager.DTO.CustomerModule;

namespace ParkingManager.BLL.Repositories.BookingModule
{
	public class BookingRepository : IBookingRepository
	{
		private readonly ApplicationDbContext context;

		private readonly IMapper mapper;
		public BookingRepository(IMapper mapper, ApplicationDbContext context)
		{
			this.context = context;

			this.mapper = mapper;
		}
		public async Task<BookingDTO> Create(BookingDTO bookingDTO)
		{
			try
			{
				var slotStatus = await context.ParkingSlots.FindAsync(bookingDTO.ParkingSlotId);

				string receipt_number = ReceiptNumber.GenerateUniqueNumber();

				bookingDTO.ReceiptNumber = "P" + "" + receipt_number;

				if (slotStatus.IsBooked == false)
				{
					var createCustomer = await CreateCustomer(bookingDTO);

					bookingDTO.Id = Guid.NewGuid();

					bookingDTO.CreateDate = DateTime.Now;

					bookingDTO.CreatedBy = bookingDTO.CreatedBy;

					bookingDTO.CustomerId = createCustomer.CustomerId;

					var booking = mapper.Map<Booking>(bookingDTO);

					context.Bookings.Add(booking);

					await context.SaveChangesAsync();

					await BookParking(bookingDTO);

					return bookingDTO;
				}

				return null;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}

		public async Task<BookingDTO> CreateCustomer(BookingDTO bookingDTO)
		{
			try
			{
				string customerId = null;

				var IsExist = await context.Customers.FirstOrDefaultAsync(x => x.PhoneNumber == bookingDTO.PhoneNumber && x.FirstName == bookingDTO.FirstName);

				if (IsExist != null)
				{
					bookingDTO.CustomerId = IsExist.Id;

					return bookingDTO;
				}

				var customer = new Customer
				{
					Id = bookingDTO.CustomerId,

					FirstName = bookingDTO.FirstName,

					LastName = bookingDTO.LastName,

					Email = bookingDTO.Email,

					PhoneNumber = bookingDTO.PhoneNumber,

					CreateDate = DateTime.Now,

					CreatedBy = bookingDTO.CreatedBy,
				};

				context.Customers.Add(customer);

				await context.SaveChangesAsync();

				return bookingDTO;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}

		public async Task<bool> BookParking(BookingDTO bookingDTO)
		{
			try
			{
				bool result = false;

				var slot = await context.ParkingSlots.FindAsync(bookingDTO.ParkingSlotId);

				if (slot != null)
				{
					slot.IsBooked = true;

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

		public async Task<List<BookingDTO>> GetAll()
		{
			try
			{
				var data = (from b in context.Bookings

							join s in context.ParkingSlots on b.ParkingSlotId equals s.Id   

							join c in context.Customers on b.CustomerId equals c.Id

							select new BookingDTO
							{
								Id = b.Id,

								ReceiptNumber = b.ReceiptNumber,

								CreatedBy = b.CreatedBy,

								CreateDate = b.CreateDate,

								UpdatedBy = b.UpdatedBy,

								FirstName = c.FirstName,

								LastName = c.LastName,

								SlotName = s.Name,

							}).ToListAsync();

				var k = await data;

				return await data;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}

		public BookingDTO GetById(Guid Id)
		{
			try
			{
				var data = context.Bookings.Find(Id);

				var slot = mapper.Map<BookingDTO>(data);

				return slot;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}

		public CustomerDTO GetCustomerById(Guid Id)
		{
			try
			{
				var data = context.Customers.Find(Id);

				var slot = mapper.Map<CustomerDTO>(data);

				return slot;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}


		public List<CustomerDTO> GetAllCustomer()
		{
			try
			{
				var data = context.Customers.ToList();

				var slot = mapper.Map<List<CustomerDTO>>(data);

				return slot;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return null;
			}
		}

		public async Task<BookingDTO> Update(BookingDTO bookingDTO)
		{
			try
			{
				var getData = await context.ParkingSlots.FindAsync(bookingDTO.Id);

				if (getData != null)
				{
					using (var transaction = context.Database.BeginTransaction())
					{
						var data = getData;

						mapper.Map(bookingDTO, data);

						context.Entry(data).State = EntityState.Modified;

						transaction.Commit();

						await context.SaveChangesAsync();
					}

					return bookingDTO;
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
