using System.ComponentModel.DataAnnotations;

namespace EpsmGest.ViewModel.Users
{
	public class EditUserPasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password e confirmação de passaword não são iguais.")]
		public string ConfirmPassword { get; set; }
	}
}
