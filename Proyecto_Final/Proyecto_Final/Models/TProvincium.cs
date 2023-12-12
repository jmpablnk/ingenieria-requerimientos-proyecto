using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models
{
    public partial class TProvincium
    {
        public TProvincium()
        {
            TProgramarRecoleccions = new HashSet<TProgramarRecoleccion>();
        }

        public int ProvinciaId { get; set; }
        public string Provincia { get; set; } = null!;

        public virtual ICollection<TProgramarRecoleccion> TProgramarRecoleccions { get; set; }
    }
}
