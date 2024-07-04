using AlumnosSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AlumnosSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;

namespace AlumnosSystem.Controllers
{
    [Authorize(Roles = "admin,alumno")]


    public class CertificadosController : Controller
    {
        private readonly AlumnosDBContext _dbContext;
        public CertificadosController(AlumnosDBContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<IActionResult> Inscripcion()
        {

            List<Carrera>carrera=await _dbContext.Carreras.ToListAsync();
            return View(carrera);
        }
        [HttpPost]
        public async Task<IActionResult> Inscripcion(string IdCarrera)
        {

            string? idusuario = User.Claims
                           .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .SingleOrDefault();
            try
            {
                Alumno? alumno = await _dbContext.Alumnos.FirstOrDefaultAsync(x => x.IdUsuario == int.Parse(idusuario));


                AlumnoInscriptoCarrera? Alumno_Encontrado = await _dbContext.AlumnoInscriptoCarreras.FirstOrDefaultAsync(x => x.IdAlumno == alumno.IdAlumno);
                Carrera? Carrera_Encontrada = await _dbContext.Carreras.FirstOrDefaultAsync(x => x.IdCarrera == int.Parse(IdCarrera));

                if (Alumno_Encontrado == null)
                {
                    ViewData["Mensaje"] = "El alumno no está inscripto a ninguna carrera";
                    return View();
                }
                if (Alumno_Encontrado.IdCarrera == int.Parse(IdCarrera))
                {
                    ViewData["Mensaje"] = "El alumno esta inscripto a la carrera " + Carrera_Encontrada.NombreCarrera;
                    return View();
                }
                else
                {
                    ViewData["Mensaje"] = "El alumno no esta inscripto a la carrera " + Carrera_Encontrada.NombreCarrera;
                    return View();
                }
            }
            catch
            {
                ViewData["Mensaje"] = "No se ha podido recuperar datos del usuario";
                    return View();
            }
           
        }
    }
}
