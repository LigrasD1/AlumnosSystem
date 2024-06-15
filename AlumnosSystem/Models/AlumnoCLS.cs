using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlumnosSystem.Models
{
    public class AlumnoCLS
    {
        [Display(Name = "ID de alumno")]
        [Key]
        public int _IdAlumno { get; set; }
        [Display(Name = "ID Usuario")]
        [ForeignKey("IdUsuario")]
        public int _IdUsuario { get; set; }
        [Required(ErrorMessage = "Debe completar el campo Nombre")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        [MinLength(1)]
        [Display(Name = "Nombre y Apellido")]
        public string? _Apenom { get; set; }
        [StringLength(50, ErrorMessage = "El DNI no puede superar los 50 caracteres")]
        [MinLength(1)]
        [Required(ErrorMessage = "Debe completar el campo DNI")]
        [Display(Name = "DNI")]
        public string? _Dni { get; set; }
        [Required(ErrorMessage = "Debe completar el campo Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "No es un correo electronico")]
        [StringLength(50, ErrorMessage = "El Email no puede superar los 50 caracteres")]
        [MinLength(1)]
        [Display(Name = "Email")]
        public string? _Email { get; set; }
        [Required(ErrorMessage = "Debe completar el campo Telefono")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "No es un Numero telefonico")]
        [MaxLength(50)]
        [Display(Name = "Teléfono")]

        public string? _Telefono { get; set; }

        public bool _Estado { get; set; }

    }
}
