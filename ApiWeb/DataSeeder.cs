using BDRiccosModel;
using Microsoft.AspNetCore.Identity;

namespace ApiWeb
{

	public class DataSeeder
	{
		public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			string[] roleNames = { "Administrador", "Vendedor", "Mozo", "Cocina" };
			IdentityResult roleResult;

			foreach (var roleName in roleNames)
			{
				var roleExist = await roleManager.RoleExistsAsync(roleName);
				if (!roleExist)
				{
					roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
				}
			}
		}
	}

}
