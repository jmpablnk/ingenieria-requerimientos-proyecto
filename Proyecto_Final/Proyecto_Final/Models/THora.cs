using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models
{
    public partial class THora
    {
        public THora()
        {
            TProgramarRecoleccions = new HashSet<TProgramarRecoleccion>();
        }

        public int HoraId { get; set; }
        public string Hora { get; set; } = null!;

        public virtual ICollection<TProgramarRecoleccion> TProgramarRecoleccions { get; set; }
    }
}
