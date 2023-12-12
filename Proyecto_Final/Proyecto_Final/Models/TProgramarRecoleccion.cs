using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models
{
    public partial class TProgramarRecoleccion
    {
        public TProgramarRecoleccion()
        {
            TMateriales = new HashSet<TMateriale>();
        }

        public int PeticionId { get; set; }
        public string DetallesEdificio { get; set; } = null!;
        public int CodigoPostal { get; set; }
        public string Municipio { get; set; } = null!;
        public int Provincia { get; set; }
        public string Canton { get; set; } = null!;
        public int Fecha { get; set; }
        public int Hora { get; set; }
        public int UsuarioId { get; set; }

        public virtual TFecha FechaNavigation { get; set; } = null!;
        public virtual THora HoraNavigation { get; set; } = null!;
        public virtual TProvincium ProvinciaNavigation { get; set; } = null!;
        public virtual TUsuario Usuario { get; set; } = null!;
        public virtual ICollection<TMateriale> TMateriales { get; set; }
    }
}
