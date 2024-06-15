using Microsoft.AspNetCore.Mvc;
using AlumnosSystem.Models;
using Microsoft.EntityFrameworkCore;


namespace AlumnosSystem.Controllers
{
    public class MateriaController : Controller
    {
        AlumnosDBContext _dbContext;
        public MateriaController(AlumnosDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> VMateria()
        {
            try
            {
                List<Materium>materia= await _dbContext.Materia.ToListAsync();
                return View(materia);

            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "No se ha podido acceder a los datos";
                return View();
            }

        }
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> EditarMateria(int? id)
        {
            try
            {
                Materium? materia = await _dbContext.Materia.FirstOrDefaultAsync(a => a.IdMateria == id);
                if (materia == null)
                {
                    ViewBag.ErrorMessage = "No se ha encontrado la materia";
                    return RedirectToAction("VMateria");
                }
                else
                {
                    MateriaCLS CopiaMateria = new MateriaCLS
                    {
                        _IdCarrera=materia.IdCarrera,
                        _IdMateria=materia.IdMateria,
                        _NombreMateria=materia.NombreMateria,
                        _Periodo=materia.Periodo
                    };
                    return View(CopiaMateria);
                }
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "No se ha podido acceder a la base de datos";
                return RedirectToAction("VMateria");

            }

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Editar(MateriaCLS materia)
        {
            try
            {
                Materium? MateriaEncontrada = await _dbContext.Materia.FirstOrDefaultAsync(a=>a.IdMateria==materia._IdCarrera);
                if (MateriaEncontrada == null)
                {
                    ViewBag.ErrorMessage = "No se ha encontrado la materia";
                    return RedirectToAction("EditarMateria");
                }
                else
                {
                    MateriaEncontrada.NombreMateria=materia._NombreMateria;
                    MateriaEncontrada.Periodo=materia._Periodo;
                     _dbContext.Materia.Update(MateriaEncontrada);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("VMateria");

                }
            }
            catch (Exception)
            {
                ViewBag.Erromessage = "No se ha podido acceder a la basede datos";
                return RedirectToAction("VMateria");
            }
        }
        [AutoValidateAntiforgeryToken]

        public ActionResult AgregarMateria() {
        
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddMateria(MateriaCLS materia)
        {
            try
            {
                
                Materium NuevaMateria = new Materium
                {
                   IdCarrera=materia._IdCarrera,
                   NombreMateria=materia._NombreMateria,
                   Periodo=materia._Periodo,

                };
                _dbContext.Materia.Add(NuevaMateria);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("VMateria");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "No se ha podiddo acceder a la base de datos";
                return RedirectToAction("VMateria");
            }
        }
    }
}
