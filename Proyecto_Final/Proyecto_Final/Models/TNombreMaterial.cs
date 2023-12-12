using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models
{
    public partial class TNombreMaterial
    {
        public TNombreMaterial()
        {
            TMateriales = new HashSet<TMateriale>();
        }

        public int NombreMaterialId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<TMateriale> TMateriales { get; set; }
    }
}
