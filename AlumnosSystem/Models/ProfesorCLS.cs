using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlumnosSystem.Models
{
    public class ProfesorCLS
    {
        public int _IdProfesor { get; set; }

        [Required]
        [Display (Name ="ID de usuario")]
        public int _IdUsuario { get; set; }
        [Required (ErrorMessage ="El nombre del profesor es requerido")]
        [Display (Name ="Nombre completo")]
        public string? _ApenomProfesor { get; set; }
        [Required (ErrorMessage ="El DNI del profesor es requerido")]
        [Display (Name ="DNI del profesor")]
        public string? _Dni { get; set; }
        [Required (ErrorMessage ="EL Email es requerido")]
        [Display (Name ="Email")]
        public string? _Email { get; set; }
        [Required (ErrorMessage ="La fecha de ingreso es requerida")]
        [Display (Name ="Fecha de ingreso")]
        public DateOnly _FechaIngreso { get; set; }
        [Required (ErrorMessage ="El telefono es requerido")]
        [Display (Name ="Telefono")]
        public string? _Telefono { get; set; }
    }
}
