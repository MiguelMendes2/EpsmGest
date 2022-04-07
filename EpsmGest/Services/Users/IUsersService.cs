using EpsmGest.ViewModel;
using EpsmGest.ViewModel.Users;

namespace EpsmGest.Services.Users
{
	public interface IUsersService
	{
		public IEnumerable<UserViewModel> GetUsers();

		public List<DropdownViewModel> GetRoles();
	}
}
