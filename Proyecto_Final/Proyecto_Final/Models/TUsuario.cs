using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models
{
    public partial class TUsuario
    {
        public TUsuario()
        {
            TProgramarRecoleccions = new HashSet<TProgramarRecoleccion>();
        }

        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo de Nombre es obligatorio.")]
        [Display(Name = "Nombre Completo Requerido")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El campo Correo Apellidos es obligatorio.")]
        [Display(Name = "Apellidos Completo Requerido")]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "El campo Número de Identificacion es obligatorio.")]
        [Display(Name = "Ingrese un número de Identificacion")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "El número de Identificacion debe tener exactamente 9 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ingrese solo dígitos.")]
        public string Indentificacion { get; set; } = null!;

        [Required(ErrorMessage = "El campo Número de Teléfono es obligatorio.")]
        [Display(Name = "Ingrese un número de teléfono")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El número de teléfono debe tener exactamente 8 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ingrese solo dígitos.")]
        public string NumTelefono { get; set; }

        [Required(ErrorMessage = "El campo Correo Electrónico es obligatorio.")]
        [Display(Name = "Correo Electrónico válido")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@gmail\.com$", ErrorMessage = "Ingrese un correo electrónico válido de Gmail.")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "El campo Contrasena es obligatorio.")]
        [Display(Name = "Contrasena requerida")]
        public string Contrasena { get; set; } = null!;


        [Required(ErrorMessage = "El campo RolId es obligatorio.")]
        public int RolId { get; set; }

        public virtual TRole Rol { get; set; } = null!;
        public virtual ICollection<TProgramarRecoleccion> TProgramarRecoleccions { get; set; }
    }
}
