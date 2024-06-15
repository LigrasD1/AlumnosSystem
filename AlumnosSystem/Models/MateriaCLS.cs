using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlumnosSystem.Models
{
    public class MateriaCLS
    {
        [Display(Name = "ID de la materia:")]
        [Key]
        public int _IdMateria { get; set; }
        [Display(Name = "ID de la carrera:")]
        [Required (ErrorMessage="La ID de carrera es requerida")]
        public int _IdCarrera { get; set; }

        [Required(ErrorMessage = "La ID de materia es requerida")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        [Display(Name = "Nombre de la materia:")]
        public string? _NombreMateria { get; set; }
        [Required(ErrorMessage = "El periodo es requerido")]
        [Display(Name = "Periodo de la materia:")]
        public DateOnly _Periodo { get; set; }
    }
}
