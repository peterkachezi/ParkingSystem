namespace ParkingManager.DTO.ParkingChargeModule
{
	public class ParkingChargeDTO
	{
		public Guid Id { get; set; }
		public decimal ParkingFee { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string? UpdatedBy { get; set; }
		public string? ModifiedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}
