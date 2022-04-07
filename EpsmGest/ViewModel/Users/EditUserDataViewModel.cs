using System.ComponentModel.DataAnnotations;

namespace EpsmGest.ViewModel.Users
{
	public class EditUserDataViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Role { get; set; }
	}
}
