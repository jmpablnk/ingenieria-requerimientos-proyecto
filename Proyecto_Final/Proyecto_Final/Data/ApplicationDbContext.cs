using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models;

namespace Proyecto_Final.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<TFecha> TFecha { get; set; }
        public virtual DbSet<THora> THora { get; set; }
        public virtual DbSet<TMateriale> TMateriale { get; set; }
        public virtual DbSet<TNombreMaterial> TNombreMaterial { get; set; }
        public virtual DbSet<TProgramarRecoleccion> TProgramarRecoleccion { get; set; }
        public virtual DbSet<TProvincium> TProvincium { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MSI\\SQL2019_DEV;Initial Catalog=DB_RECOLECCION_RECICLAJE;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False; Trusted_Connection=True; TrustServerCertificate=True;");
            }
        }
    }
}