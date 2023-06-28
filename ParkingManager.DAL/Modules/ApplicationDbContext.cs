using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ParkingManager.DAL.Modules
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<CheckoutRequest> CheckoutRequests { get; set; } = null!;
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<MpesaPayment> MpesaPayments { get; set; } = null!;
        public virtual DbSet<ParkingSlot> ParkingSlots { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<PaybillPayment> PaybillPayments { get; set; } = null!;
        public virtual DbSet<PaypalPayment> PaypalPayments { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MpesaPayment>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.Property(e => e.Balance).HasColumnType("decimal(18,2)");

            });

          
            modelBuilder.Entity<PaypalPayment>(entity =>
            {
                entity.Property(e => e.AmountPaid).HasColumnType("decimal(18,2)");

            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.ParkingSlot)

                    .WithMany(p => p.Bookings)

                    .HasForeignKey(d => d.ParkingSlotId)

                    .OnDelete(DeleteBehavior.ClientSetNull)

                    .HasConstraintName("FK_Appointments_TimeSlots");
            });

            modelBuilder.Entity<ParkingSlot>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

        }

    }

}
