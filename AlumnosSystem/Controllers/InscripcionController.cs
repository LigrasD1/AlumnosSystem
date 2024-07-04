using AlumnosSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AlumnosSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
namespace AlumnosSystem.Controllers
{
    [Authorize(Roles = "admin,alumno")]
    public class InscripcionController : Controller
    {
        private readonly AlumnosDBContext _dbContext;
        public InscripcionController(AlumnosDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
		[HttpGet]
        public async Task<IActionResult> VInscripcion()
        {
            try
            {
				List<Carrera> Carreras = await _dbContext.Carreras.ToListAsync();
                List<string> CarreNames = new List<string>();
                foreach (var carrera in Carreras)
                {
                    CarreNames.Add(carrera.NombreCarrera);
                }
                ViewBag.Nombres= CarreNames;
                return View();

			}
			catch (Exception) 
            {
                ViewData["Mensaje"] = "No se ha podido acceder a la base de datos";
                return View();

			}

		}
        [HttpPost]
		[AutoValidateAntiforgeryToken]

		public async Task<IActionResult> VInscripcion(AlumnoInscriptoCarreraVM Alumno)
        {

            var userType = User.Claims
                           .Where(c => c.Type == ClaimTypes.NameIdentifier)
                           .Select(c => c.Value)
                           .SingleOrDefault();
			Alumno NuevoAlumno = new Alumno()
			{
				IdUsuario = int.Parse(userType),
				Apenom = Alumno._Apenom,
				Dni = Alumno._Dni,
				Email = Alumno._Email,
				Telefono = Alumno._Telefono,
				Estado = false, //si es 1 aceptan al alumno, si es 0 se queda rechazado.
			};
			try
			{
                await _dbContext.Alumnos.AddAsync(NuevoAlumno);
                await _dbContext.SaveChangesAsync();
			}
			catch (Exception) 
            {
				ViewData["Mensaje"] = "No se ha podido guardar el alumno";
				return View();
			}

            if (NuevoAlumno.IdUsuario != 0)
            {
				AlumnoInscriptoCarrera NuevaInscripcion = new AlumnoInscriptoCarrera()
				{
					IdCarrera = Alumno.idCarrera,
					IdAlumno = NuevoAlumno.IdAlumno,
                    FechaInscripcion=DateOnly.FromDateTime(DateTime.Now),
                    Estado="Pendiente",
				};
				try
				{
					await _dbContext.AlumnoInscriptoCarreras.AddAsync(NuevaInscripcion);
					await _dbContext.SaveChangesAsync();
				}
				catch (Exception) 
                {
					ViewData["Mensaje"] = "No se ha podido guardar la inscripcion";
					return View();
				}
            }
            else
            {
                ViewData["Mensaje"] = "No se ha podido generar el alumno";
                return View();
            }
			
            return View();

        }
        

    }
}
