using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models
{
    public partial class TMateriale
    {
        public int MaterialId { get; set; }
        public int NombreMaterialId { get; set; }

        [Required(ErrorMessage = "El campo Peso es obligatorio.")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Por favor, ingrese un valor válido para el peso en kilogramos.")]
        public string Peso { get; set; } = null!;
        public int PeticionId { get; set; }

        public virtual TNombreMaterial NombreMaterial { get; set; } = null!;
        public virtual TProgramarRecoleccion Peticion { get; set; } = null!;
    }
}
