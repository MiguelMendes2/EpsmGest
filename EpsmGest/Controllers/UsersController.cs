using EpsmGest.Data;
using EpsmGest.Services.Utilizadores;
using EpsmGest.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EpsmGest.Controllers
{
    [Route("Utilizadores/")]
    [Route("Users/")]
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private IUsersService UsersService { get; set; }
        private readonly UserManager<IdentityUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly EpsmGestDbContext Context;

        public UsersController(IUsersService _usersService, EpsmGestDbContext _context, UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            UsersService = _usersService;
            UserManager = _userManager;
            RoleManager = _roleManager;
            Context = _context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var model = UsersService.GetUsers();
            return View(model);
        }

        [HttpGet]
        [Route("Criar")]
        public IActionResult Create()
        {
            ViewBag.Roles = UsersService.GetRoles();
            return View();
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user, model.Role);
                    if (result.Succeeded)
                    {
                        TempData["Success"] = "Utilizador criado com sucesso!";
                        return RedirectToAction("Index");
                    }
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Não foi possivel adicionar este utilizador ao cargo introduzido");

                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Route("Detalhes/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var user = Context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
			{
                TempData["Error"] = "Utilizador não foi encontrado.";
                return RedirectToAction("Index");
            }
            var roles = await UserManager.GetRolesAsync(user);
            if (roles == null)
			{
                TempData["Error"] = "Utilizador que selecionou não possui cargos, contacte o administrador.";
                return RedirectToAction("Index");
			}
            var model = new UserViewModel
            {
                Id = id,
                UserName = user.UserName,
                Email = user.Email,
                Role = roles[0]
            };
            ViewBag.Roles = UsersService.GetRoles().Select(x=> new DropdownViewModel 
            { 
                Id= x.Name,
                Name = x.Name
            });
            return View(model);
        }

        [HttpPost]
        [Route("AlterarCargo")]
        public async Task<IActionResult> EditRole(UserViewModel model)
        {
            string currentUserName = User.Identity.Name;
            var user = await UserManager.FindByIdAsync(model.Id);
            if (user.UserName == currentUserName)
            {
                TempData["Error"] = "Não é possivel alterar o cargos da sua conta!";
                return RedirectToAction("Detalhes", new { id = model.Id });
            }
            var roles = await UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                if (model.Role == role)
                {
                    TempData["Error"] = "não é possivel alterar o cargo para o mesmo cargo";
                    return RedirectToAction("Detallhes", "Utilizadores", new { id = model.Id });
                }
            }
            var result = await UserManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Não foi possivel remover os cargos dos utlizador";
                return RedirectToAction("Detallhes", "Utilizadores", new { id = model.Id });
            }

            result = await UserManager.AddToRoleAsync(user, model.Role);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Não foi possivel adicionar o cargo selecionado ao utilizador";
            }
            TempData["Success"] = "Cargo de utilizador alterado com sucesso!";
            return RedirectToAction("Detalhes", "Utilizadores", new { id = model.Id });
        }

        [HttpGet]
        [Route("AlterarPassword/{id}")]
        public IActionResult ChangePassword(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [Route("AlterarPassword/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(EditUserPasswordViewModel model, string id)
        {
            if (User.Identity == null)
                return NotFound();
            string currentUserName = User.Identity.Name;
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["Error"] = "Utilizador não encontrado!";
                return RedirectToAction("Index", "Utilizadores");
            }
            if (user.UserName == currentUserName)
            {
                TempData["Error"] = "Não é possivel resetar a password da sua conta! Dirija-se às defenições da sua conta para alterar a sua password";
                return RedirectToAction("Detalhes", "Utilizadores", new { id = id });
            }
            if (ModelState.IsValid)
            {
                var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                var result = await UserManager.ResetPasswordAsync(user, token, model.Password);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Password alterada com sucess!";
                    return RedirectToAction("Detalhes", "Utilizadores", new { id = id });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewBag.Id = id;
            return View(model);
        }

        [HttpGet]
        [Route("RemoveUser/{id}")]
        public async Task<IActionResult> RemoveUser(string id)
        {
            if (User.Identity == null)
                return NotFound();
            string currentUserName = User.Identity.Name;
            var user = await UserManager.FindByIdAsync(id);
            if (user.UserName == currentUserName)
            {
                TempData["Error"] = "Não é possivel remover o seu proprio utilizador!";
                return RedirectToAction("Detalhes", "Utilizadores", new { id = id });
            }
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Success"] = "Utilizador removido com sucesso!";
                return RedirectToAction("Index", "Utilizadores");
            }
            foreach (var error in result.Errors)
            {
                TempData["Error"] = error.Description;
            }
            return RedirectToAction("Detalhes", "Utilizadores", new { id = id });
        }
    }
}
