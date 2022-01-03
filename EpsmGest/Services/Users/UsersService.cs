using EpsmGest.Data;
using EpsmGest.Services.Utilizadores;
using EpsmGest.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace EpsmGest.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly EpsmGestDbContext Context;

        public UsersService(EpsmGestDbContext _context, UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            UserManager = _userManager;
            RoleManager = _roleManager;
            Context = _context;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = Context.Users.ToList();
            var userRoles = Context.UserRoles.ToList();
            var roles = Context.Roles.ToList();
            return users.Join(userRoles,
                        user => user.Id,
                        userRole => userRole.UserId,
                        (user, userRole) => new UserViewModel
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            Role = userRole.RoleId
                        }).Join(roles,
                        user => user.Role,
                        role => role.Id,
                        (user, role) => new UserViewModel
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            Role = role.Name
                        });
        }

        public List<DropdownViewModel> GetRoles()
        {
            var UserRoles = new List<DropdownViewModel>();
            foreach (var role in RoleManager.Roles)
            {
                var userRole = new DropdownViewModel()
                {
                    Id = role.Id,
                    Name = role.Name
                };
                UserRoles.Add(userRole);
            }
            return UserRoles;
        }
    }
}
