using AlumnosSystem.Models;
using Microsoft.AspNetCore.Mvc;
using AlumnosSystem.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace AlumnosSystem.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AlumnosDBContext _dbContext;
        public AccesoController(AlumnosDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(LoginVM modelo)
        {
            Usuario? Usuario_Encontrado=await _dbContext.Usuarios.Where(a=>a.Correo==modelo.Correo && a.Contraseña==modelo.Contraseña).FirstOrDefaultAsync();
            if (Usuario_Encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontro el usuario";
                return View();
            }
            
            List<Claim> claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.Name,Usuario_Encontrado.NombreUsuario),
                new Claim(ClaimTypes.NameIdentifier,Usuario_Encontrado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, Usuario_Encontrado.TipoUsuario),
            };
            ClaimsIdentity claimsIdentity=new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties=new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult Registro()
        {

            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult>Registro(UsuarioVM modelo)
        {
            if (modelo.Contraseña != modelo.ConfirmarContraseña)
            {
                ViewData["Mensaje"] = "Las contraeñas no coinciden";
                return View();  
            }
            Usuario NuevoUsuario = new Usuario()
            {
                NombreUsuario=modelo.NombreUsuario,
                Contraseña=modelo.Contraseña,
                Correo=modelo.Correo,
                TipoUsuario="Sin asignar",
            };
            await _dbContext.Usuarios.AddAsync(NuevoUsuario);
            await _dbContext.SaveChangesAsync();

            if (NuevoUsuario.IdUsuario != 0) return RedirectToAction("Index","Home");
            ViewData["Mensaje"] = "No se ha podido crear el usuario";
            return View();

        }
    }
}
