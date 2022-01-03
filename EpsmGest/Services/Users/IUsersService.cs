using EpsmGest.ViewModel;

namespace EpsmGest.Services.Utilizadores
{
    public interface IUsersService
    {
        public IEnumerable<UserViewModel> GetUsers();

        public List<DropdownViewModel> GetRoles();
    }
}
