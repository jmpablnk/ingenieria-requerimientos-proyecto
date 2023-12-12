using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models
{
    public partial class TFecha
    {
        public TFecha()
        {
            TProgramarRecoleccions = new HashSet<TProgramarRecoleccion>();
        }

        public int FechaId { get; set; }
        public string Fecha { get; set; } = null!;

        public virtual ICollection<TProgramarRecoleccion> TProgramarRecoleccions { get; set; }
    }
}
