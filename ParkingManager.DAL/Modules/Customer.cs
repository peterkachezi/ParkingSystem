namespace ParkingManager.DAL.Modules
{
	public partial class Customer
    {
        public Guid  Id { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string  Email { get; set; }
        public string  PhoneNumber { get; set; }
        public DateTime  CreateDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
