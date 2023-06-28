using Microsoft.AspNetCore.Identity;

namespace ParkingManager.DAL.Modules
{
	public class AppUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CreatedBy { get; set; }
		public bool isActive { get; set; }
		public DateTime CreateDate { get; set; }

	}
}
