
using ParkingManager.DAL.Modules;
using Microsoft.AspNetCore.Identity;
using System;

namespace ParkingManager.SeedAppUsers
{
	public static class SeedUsers
	{
		public static void Seed(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			CreateRoles(roleManager);

			CreateUsers(userManager);
		}

		private static void CreateRoles(RoleManager<IdentityRole> roleManager)
		{
			try
			{
				if (!roleManager.RoleExistsAsync("Admin").Result)
				{
					var role = new IdentityRole
					{
						Name = "Admin"
					};

					roleManager.CreateAsync(role).Wait();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private static void CreateUsers(UserManager<AppUser> userManager)
		{
			try
			{
	
				var doctor = userManager.FindByEmailAsync("trina@gmail.com");

				if (doctor.Result == null)
				{
					var user = new AppUser
					{
						UserName = "trina@gmail.com",

						Email = "trina@gmail.com",

						PhoneNumber = "0704509484",

						FirstName = "Agent",

						LastName = "Steve",

						EmailConfirmed = true,

						isActive = true,

						CreateDate = DateTime.Now
					};

					string userPWD = "Trina@2023";

					var chkUser = userManager.CreateAsync(user, userPWD);

					//Add default User to Role Admin    
					if (chkUser.Result.Succeeded)
					{
						userManager.AddToRoleAsync(user, "Admin").Wait();

					}
				}
			
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

			}
		}

	}
}
