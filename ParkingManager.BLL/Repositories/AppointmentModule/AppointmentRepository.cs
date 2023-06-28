//using AutoMapper;
//using ParkingManager.DAL.Modules;
//using ParkingManager.DTO.AppointmentModule;
//using Microsoft.EntityFrameworkCore;


//namespace ParkingManager.BLL.Repositories.AppointmentModule
//{
//    public class AppointmentRepository : IAppointmentRepository
//    {
//        private readonly ApplicationDbContext context;

//        private readonly IMapper mapper;
//        public AppointmentRepository(IMapper mapper, ApplicationDbContext context)
//        {
//            this.context = context;

//            this.mapper = mapper;
//        }
//        public async Task<AppointmentDTO> Create(AppointmentDTO appointmentDTO)
//        {
//            try
//            {
//                appointmentDTO.Id = Guid.NewGuid();

//                var appointment = new Booking
//                {
//                    CreateDate = DateTime.Now,

//                    Id = appointmentDTO.Id,

//                    Status = 0,

//                    IsCompleted = 0,

//                    IsReminderSent = 0,

//                    IsApproved = 0,

//                    IsBookingComplete = 1,

//                    AppointmentDate = appointmentDTO.AppointmentDate,

//                    CustomerId = appointmentDTO.CustomerId,

//                    ParkingSlotId = appointmentDTO.ParkingSlotId,

//                    TransactionNumber = appointmentDTO.TransactionNumber,

//                    ApprovedBy = "",

//                };

//                context.Appointments.Add(appointment);

//                await context.SaveChangesAsync();

//                UpdateSlot(appointmentDTO);

//                return appointmentDTO;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);

//                return null;
//            }
//        }

//        public bool UpdateSlot(AppointmentDTO appointmentDTO)
//        {
//            try
//            {
//                var getSlot = context.ParkingSlots.Find(appointmentDTO.ParkingSlotId);

//                if (getSlot != null)
//                {
//                    using (var transaction = context.Database.BeginTransaction())
//                    {
//                       // getSlot.IsBooked = 1;

//                        //getSlot.AppointmentDate = appointmentDTO.AppointmentDate;

//                        transaction.Commit();

//                        context.SaveChanges();
//                    }
//                    return true;
//                }

//                return false;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);

//                return false;
//            }
//        }

//        public bool UpdatePaymentStatus(AppointmentDTO appointmentDTO)
//        {
//            var getSlot = context.MpesaPayments.Where(x => x.TransactionNumber == appointmentDTO.TransactionNumber.Trim()).FirstOrDefault();

//            if (getSlot != null)
//            {
//                using (var transaction = context.Database.BeginTransaction())
//                {
//                    getSlot.IsPaymentUsed = 1;

//                    transaction.Commit();

//                    context.SaveChanges();
//                }
//                return true;
//            }

//            return false;
//        }
//        public async Task<List<AppointmentDTO>> GetAll()
//        {
//            try
//            {
//                var appointments = (from appointment in context.Appointments

//                                    join patient in context.Customers on appointment.CustomerId equals patient.Id

//                                    join timslot in context.ParkingSlots on appointment.ParkingSlotId equals timslot.Id

//                                    select new AppointmentDTO()
//                                    {
//                                        Id = appointment.Id,

//                                        CustomerId = appointment.CustomerId,

//                                        Status = appointment.Status,

//                                        IsCompleted = appointment.IsCompleted,

//                                        CreateDate = appointment.CreateDate,

//                                        AppointmentDate = appointment.AppointmentDate,

//                                        FirstName = patient.FirstName,

//                                        LastName = patient.LastName,

//                                        ParkingSlotId = appointment.ParkingSlotId,

//                                    }).ToListAsync();


//                return await appointments;

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);

//                return null;
//            }
//        }
//        public async Task<AppointmentDTO> GetById(Guid Id)
//        {
//            try
//            {
//                var appointments = (from appointment in context.Appointments

//                                    join patient in context.Customers on appointment.CustomerId equals patient.Id

//                                    join timslot in context.ParkingSlots on appointment.ParkingSlotId equals timslot.Id

//                                    where appointment.Id == Id

//                                    select new AppointmentDTO()
//                                    {
//                                        Id = appointment.Id,

//                                        Status = appointment.Status,

//                                        IsCompleted = appointment.IsCompleted,

//                                        CreateDate = appointment.CreateDate,

//                                        AppointmentDate = appointment.AppointmentDate,

//                                        CustomerId = patient.Id,

//                                        FirstName = patient.FirstName,

//                                        PhoneNumber = patient.PhoneNumber,

//                                        Email = patient.Email,

//                                        LastName = patient.LastName,

//                                        ParkingSlotId = appointment.ParkingSlotId,                               

//                                    }).FirstOrDefaultAsync();

//                return await appointments;

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);

//                return null;
//            }
//        }

//        public async Task<AppointmentDTO> VideoCallAppointmentDetails(Guid Id)
//        {
//            try
//            {
//                var appointments = (from appointment in context.Appointments

//                                    join patient in context.Customers on appointment.CustomerId equals patient.Id

//                                    join timslot in context.ParkingSlots on appointment.ParkingSlotId equals timslot.Id

//                                    where appointment.Id == Id & appointment.Status == 1 & appointment.IsCompleted == 1

//                                    select new AppointmentDTO()
//                                    {
//                                        Id = appointment.Id,

//                                        Status = appointment.Status,

//                                        IsCompleted = appointment.IsCompleted,

//                                        CreateDate = appointment.CreateDate,

//                                        AppointmentDate = appointment.AppointmentDate,

//                                        CustomerId = patient.Id,

//                                        FirstName = patient.FirstName,

//                                        PhoneNumber = patient.PhoneNumber,

//                                        Email = patient.Email,

//                                        LastName = patient.LastName,

//                                        ParkingSlotId = appointment.ParkingSlotId,             

//                                    }).FirstOrDefaultAsync();

//                return await appointments;

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);

//                return null;
//            }
//        }


//        public AppointmentDTO GetTransaction(Guid Id)
//        {
//            try
//            {
//                var appointments = (from appointment in context.Appointments

//                                    join patient in context.Customers on appointment.CustomerId equals patient.Id

//                                    join timslot in context.ParkingSlots on appointment.ParkingSlotId equals timslot.Id

//                                    //join payment in context.MpesaPayments on appointment.TransactionNumber equals payment.TransactionNumber

//                                    where appointment.Id == Id

//                                    select new AppointmentDTO()
//                                    {
//                                        Id = appointment.Id,

//                                        Status = appointment.Status,

//                                        CreateDate = appointment.CreateDate,

//                                        AppointmentDate = appointment.AppointmentDate,

//                                        FirstName = patient.FirstName,

//                                        LastName = patient.LastName,

//                                        PhoneNumber = patient.PhoneNumber,

//                                        Email = patient.Email,

//                                        ParkingSlotId = appointment.ParkingSlotId,

//                                    }).FirstOrDefault();

//                return appointments;

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);

//                return null;
//            }
//        }
//        public async Task<bool> ApproveAppointment(AppointmentDTO appointmentDTO)
//        {
//            try
//            {
//                bool result = false;

//                var getAppointment = await context.Appointments.FindAsync(appointmentDTO.Id);

//                if (getAppointment != null)
//                {
//                    using (var transaction = context.Database.BeginTransaction())
//                    {
//                        getAppointment.Status = 1;

//                        getAppointment.ApprovedBy = appointmentDTO.ApprovedBy;

//                        transaction.Commit();

//                        await context.SaveChangesAsync();
//                    }

//                    return true;
//                }

//                return result;

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);

//                return false;
//            }
//        }
//        public async Task<bool> Delete(Guid Id)
//        {
//            try
//            {
//                bool result = false;

//                var data = await context.Appointments.FindAsync(Id);

//                if (data != null)
//                {
//                    context.Appointments.Remove(data);

//                    await context.SaveChangesAsync();

//                    result = true;
//                }

//                return result;

//            }
//            catch (Exception ex)
//            {

//                Console.WriteLine(ex.Message);

//                return false;
//            }
//        }
//        public async Task<AppointmentDTO> RescheduleAppointment(AppointmentDTO appointmentDTO)
//        {
//            try
//            {
//                var data = await context.Appointments.FindAsync(appointmentDTO.Id);

//                if (data != null)
//                {
//                    using (var transaction = context.Database.BeginTransaction())
//                    {
//                        data.AppointmentDate = appointmentDTO.AppointmentDate;

//                        data.ParkingSlotId = appointmentDTO.ParkingSlotId;

//                        transaction.Commit();
//                    }

//                    await context.SaveChangesAsync();

//                    UpdateSlot(appointmentDTO);

//                    //VacateSlot(appointmentDTO.OldParkingSlotId);

//                    return appointmentDTO;
//                }

//                return null;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);

//                return null;
//            }
//        }
//        public bool VacateSlot(Guid ParkingSlotId)
//        {
//            var getSlot = context.ParkingSlots.Find(ParkingSlotId);

//            if (getSlot != null)
//            {
//                using (var transaction = context.Database.BeginTransaction())
//                {
//                    //getSlot.IsBooked = 0;

//                    transaction.Commit();

//                    context.SaveChanges();
//                }
//                return true;
//            }

//            return false;
//        }
      
//    }
//}
