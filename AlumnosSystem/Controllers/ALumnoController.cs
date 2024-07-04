using AlumnosSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AlumnosSystem.Controllers
{
    [Authorize (Roles = "admin,administrador")]

    public class ALumnoController : Controller
    {
        AlumnosDBContext _dbContext;
        public ALumnoController(AlumnosDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Inscripcion()
        {
            return View();
        }
        public async Task<IActionResult> VAlumno()
        {
            try
            {
                List<Alumno> LAlumno = await _dbContext.Alumnos.ToListAsync();

                return View(LAlumno);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "No se han podido recuperar los alumnos";
                return View();
            }

        }
        
        public async Task<IActionResult> EditarAlumno(int? id)
        {
            try
            {
                var AlumnoEncontrado = await _dbContext.Alumnos.FirstOrDefaultAsync(a => a.IdAlumno == id);

                if (AlumnoEncontrado == null)
                {
                    ViewBag.ErrorMessage = "Alumno no encontrado";
                    return RedirectToAction("VAlumno");
                }
                else
                {
                    AlumnoCLS Alum = new AlumnoCLS
                    {
                        _IdAlumno = AlumnoEncontrado.IdAlumno,
                        _IdUsuario = AlumnoEncontrado.IdUsuario,
                        _Apenom = AlumnoEncontrado.Apenom,
                        _Dni = AlumnoEncontrado.Dni,
                        _Email = AlumnoEncontrado.Email,
                        _Telefono = AlumnoEncontrado.Telefono,
                        _Estado = AlumnoEncontrado.Estado,
                    };
                    return View(Alum);

                }
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "No se ha podido acceder a los datos";
                return RedirectToAction("VAlumno");

            }

        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<ActionResult> Editar(AlumnoCLS alumCLS)
        {
            try
            {
                var alumno = await _dbContext.Alumnos.FirstOrDefaultAsync(a => a.IdAlumno == alumCLS._IdAlumno);

                if (alumno == null)
                {
                    ViewBag.ErrorMessage = "Alumno no existente";
                }
                else
                {
                    alumno.Apenom = alumCLS._Apenom;
                    alumno.Dni = alumCLS._Dni;
                    alumno.Telefono = alumCLS._Telefono;
                    alumno.Email = alumCLS._Email;
                    alumno.Estado = alumCLS._Estado;
                    _dbContext.Alumnos.Update(alumno);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex) 
            {
                ViewBag.ErrorMessage = "No se ha podido accder a la base de datos";
            }


            return RedirectToAction("VAlumno");


        }

        public IActionResult AgregarAlumno()
        {
            return View();
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<ActionResult> AddAlumno(AlumnoCLS alum)
        {
            try
            {
                var alumno = await _dbContext.Alumnos.FirstOrDefaultAsync(a=>a.IdUsuario==alum._IdUsuario);
                if (alumno == null)
                {
                    ViewBag.ErrorMessage = "Ya existe el alumno para este usuario";
                    return RedirectToAction("AgregarAlumno");
                }
                Alumno NuevoAlumno = new Alumno
                {
                    IdUsuario = alum._IdUsuario,
                    Apenom = alum._Apenom,
                    Dni = alum._Dni,
                    Email = alum._Email,
                    Telefono = alum._Telefono,
                    Estado = alum._Estado,

                };
                _dbContext.Alumnos.Add(NuevoAlumno);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("VAlumno");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "No se ha podiddo acceder a la base de datos";
                return RedirectToAction("VAlumno");
            }
            
        }
    }
}
