using AlumnosSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlumnosSystem.Controllers
{
    [Authorize(Roles = "AdminPolicy")]
    [Authorize(Policy = "AdministradorPolicy")]
    public class CarreraController : Controller
	{
		private readonly AlumnosDBContext _dbContext;
		public CarreraController(AlumnosDBContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IActionResult> VCarrera()
		{
			try
			{
				List<Carrera> Carreras = await _dbContext.Carreras.ToListAsync();
				return View(Carreras);

			}
			catch (Exception)
			{	
				ViewData["Mensaje"] = "No se ha podido acceder a la base de datos";
				return View();
			}

		}
		public async Task<IActionResult> EditarCarrera(int? id)
		{
			try
			{
				Carrera? CarrerEncontrada= await _dbContext.Carreras.FirstOrDefaultAsync(x=>x.IdCarrera==id);
				if (CarrerEncontrada != null) 
				{
					return View(CarrerEncontrada);
				}
				else
				{
					ViewData["Mensaje"] = "Carrera no encontrada";
					return RedirectToAction("VCarrera");
				}
			}
			catch (Exception) 
			{
				ViewData["Mensaje"] = "No se ha podido acceder a la base de datos";
				return RedirectToAction("VCarrera");
			}
		}

		public async Task<IActionResult>Editar(Carrera car)
		{
            try
            {
                var carrera = await _dbContext.Carreras.FirstOrDefaultAsync(a => a.IdCarrera == car.IdCarrera);

                if (carrera == null)
                {
                    ViewBag.ErrorMessage = "Carrera no existente";
                }
                else
                {
                    carrera.NombreCarrera = car.NombreCarrera;
					carrera.Estado = car.Estado;
                    _dbContext.Carreras.Update(carrera);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "No se ha podido accder a la base de datos";
            }


			return RedirectToAction("VCarrera");

        }
		public IActionResult AgregarCarrera()
		{
			return View(); 
		}
    }
}
