

namespace ParkingManager.DTO.CustomerModule
{
    public class CustomerDTO
	{
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }      
        public DateTime CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public string CarRegNo { get; set; }
    }
}
