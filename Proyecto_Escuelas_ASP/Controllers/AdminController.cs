using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proyecto_Escuelas_ASP.Models;

namespace Proyecto_Escuelas_ASP.Controllers
{
    public class AdminController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;



        public AdminController(RoleManager<IdentityRole> Role, UserManager<IdentityUser> user)
        {
            _roleManager = Role;
            _userManager = user;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            // Verificar si el rol existe   
            if (!await _roleManager.RoleExistsAsync(role.Name))
            {
                await _roleManager.CreateAsync(new IdentityRole(role.Name));
            }
            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public IActionResult AsignarRol()
        {
            var roles = _roleManager.Roles.ToList();
            var users = _userManager.Users.ToList();
            var identityUserRoleList = new List<IdentityUserRole<string>>();

            foreach (var user in users)
            {
                var userRoles = _userManager.GetRolesAsync(user).Result;
                foreach (var roleName in userRoles)
                {
                    var role = _roleManager.Roles.Single(r => r.Name == roleName);
                    var identityUserRole = new IdentityUserRole<string> { UserId = user.Id, RoleId = role.Id };
                    identityUserRoleList.Add(identityUserRole);
                }
            }

            var viewmodel = new viewModelUsers
            {
                Usuarios = users,
                roles = roles,
                rolesFk = identityUserRoleList
            };



            return View(viewmodel);
        }
        [HttpGet]
        public IActionResult AsignarRolUser(string? ID)
        {

            var roles = _roleManager.Roles.ToList();

            ViewBag.rol = roles;
            ViewBag.id = ID;

            return View();
        }
        [HttpPost]
        public IActionResult PonerRol(PonerRol conten)
        {
            var users = _userManager.Users.Where(x => x.Id == conten.UserId).FirstOrDefault();

            _userManager.AddToRoleAsync(users, conten.RoleName).GetAwaiter().GetResult();
            return RedirectToAction(nameof(AsignarRol));
        }

    }


}
