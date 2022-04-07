using System.ComponentModel.DataAnnotations;

namespace EpsmGest.ViewModel.Users
{
	public class RegisterViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password e confirmação de passaword não são iguais.")]
		public string ConfirmPassword { get; set; }

		[Required]
		public string Role { get; set; }
	}
}
