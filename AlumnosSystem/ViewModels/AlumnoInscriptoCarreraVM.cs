using System.ComponentModel.DataAnnotations;

namespace AlumnosSystem.ViewModels
{
    public class AlumnoInscriptoCarreraVM
    {
		[StringLength(50, ErrorMessage = "El Nombre no puede superar los 100 caracteres")]
		[MinLength(1)]
		[Required(ErrorMessage = "Debe completar el campo Nombre")]
		[Display(Name = "Nombre Completo")]
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
        public int idCarrera { get; set; }
    }
}
