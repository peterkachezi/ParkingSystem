
namespace ParkingManager.DAL.Modules
{
    public class Booking
    {
		public Guid Id { get; set; }
		public byte Status { get; set; }
		public Guid CustomerId { get; set; }
		public Guid ParkingSlotId { get; set; }
        public DateTime CreateDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }
		public string ReceiptNumber { get; set; }
		public string CarRegNo { get; set; }
		public virtual ParkingSlot ParkingSlot { get; set; } = null!;
		public virtual Customer Customer { get; set; } = null!;

	}
}
