using AlumnosSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AlumnosSystem.Controllers
{
    [Authorize(Roles = "admin,administrador")]

    public class ProfesorController : Controller
    {
        AlumnosDBContext _dbContext;
        public ProfesorController(AlumnosDBContext dbContext)
        {
            _dbContext = dbContext;
        }
       

        public async Task< IActionResult> VProfesor()
        {
            try
            {
                List<Profesor> LProfesor = await _dbContext.Profesors.ToListAsync();

                return View(LProfesor);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "No se han podido recuperar los alumnos";
                return View();
            }
            
        }

        public async Task<IActionResult> EditarProfesor(int? id)
        {

            try
            {
                Profesor? profe = await _dbContext.Profesors.FirstOrDefaultAsync(a => a.IdProfesor == id);

                if (profe == null)
                {
                    ViewBag.ErrorMessage = "Profesor no encontrado";
                    return RedirectToAction("VProfesor");
                }
                else {
                    ProfesorCLS CopiaProfe = new ProfesorCLS {
                        _IdProfesor = profe.IdProfesor,
                        _IdUsuario=profe.IdUsuario,
                        _ApenomProfesor=profe.ApenomProfesor,
                        _Dni=profe.Dni,
                        _Email=profe.Email,
                        _Telefono=profe.Telefono,
                        _FechaIngreso=profe.FechaIngreso
                    };
                    return View(CopiaProfe);
                }
                
            }
            catch (Exception) 
            {
                ViewBag.ErrorMessage = "No se ha podido acceder a la base de datos";
                return RedirectToAction("VProfesor");
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ProfesorCLS profe)
        {

            if (!ModelState.IsValid)
            {
                return View(profe);
            }
            try
            {
                Profesor? Profesor = await _dbContext.Profesors.FirstOrDefaultAsync(a => a.IdUsuario == profe._IdUsuario);
                if (Profesor == null)
                {
                    ViewBag.ErrorMessage = "Ya existe un este alumno";
                    return RedirectToAction("VProfesor");
                }
                else
                {
                    Profesor.ApenomProfesor = profe._ApenomProfesor;
                    Profesor.Dni = profe._Dni;
                    Profesor.FechaIngreso = profe._FechaIngreso;
                    Profesor.Email = profe._Email;
                    Profesor.Telefono = profe._Telefono;
                    _dbContext.Profesors.Update(Profesor);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("VProfesor");
                }
            }
            catch (Exception)
            {
                ViewBag.Errormessage = "No se ha podido acceder a la basede datos";
                return View(profe);
            }
            
            
            
        }
        
        public IActionResult AgregarProfesor()
        {

            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async  Task<ActionResult> AddProfesor(ProfesorCLS profe)
        {

            if (!ModelState.IsValid)
            {
                return View(profe);
            }
            try
            {
                var profesor= await _dbContext.Profesors.FirstOrDefaultAsync(a=>a.IdUsuario==profe._IdUsuario);
                if (profesor == null) {
                    Profesor NuevoProfesor = new Profesor {
                        IdUsuario=profe._IdUsuario,
                        Dni = profe._Dni,
                        ApenomProfesor=profe._ApenomProfesor,
                        Email=profe._Email,
                        Telefono=profe._Telefono,
                        FechaIngreso=profe._FechaIngreso,
                    };
                    _dbContext.Profesors.Add(NuevoProfesor);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("VProfesor");

                }
                else
                {
                    ViewBag.ErrorMessage = "Ya hay existe este profesor";
                    return View(profe);
                }
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "No se ha podido acceder a la base de datos";
                return View(profe);
            }

        }
    }
}
