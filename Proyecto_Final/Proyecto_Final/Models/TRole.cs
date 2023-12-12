using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models
{
    public partial class TRole
    {
        public TRole()
        {
            TUsuarios = new HashSet<TUsuario>();
        }

        public int RolId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Permiso { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual ICollection<TUsuario> TUsuarios { get; set; }
    }
}
