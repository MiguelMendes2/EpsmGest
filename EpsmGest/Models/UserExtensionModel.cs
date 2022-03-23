using Microsoft.AspNetCore.Identity;

namespace EpsmGest.Models
{
	public class UserExtensionModel : IdentityUser
	{
		public bool IsDriver { get; set; }
	}
}
