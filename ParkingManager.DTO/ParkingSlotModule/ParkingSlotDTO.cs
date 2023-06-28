

namespace ParkingManager.DTO.ParkingSlotModule
{
    public class ParkingSlotDTO
    {
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string? UpdatedBy { get; set; }
		public string? ModifiedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public byte Status { get; set; }
        public bool IsBooked { get; set; }

    }
}
