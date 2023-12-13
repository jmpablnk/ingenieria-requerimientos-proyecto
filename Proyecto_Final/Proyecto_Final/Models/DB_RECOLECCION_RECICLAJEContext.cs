using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proyecto_Final.Models
{
    public partial class DB_RECOLECCION_RECICLAJEContext : DbContext
    {
        public DB_RECOLECCION_RECICLAJEContext()
        {
        }

        public DB_RECOLECCION_RECICLAJEContext(DbContextOptions<DB_RECOLECCION_RECICLAJEContext> options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TFecha>(entity =>
            {
                entity.HasKey(e => e.FechaId)
                    .HasName("PK_FECHA");

                entity.ToTable("T_FECHA", "SCH_RECIQUEST");

                entity.Property(e => e.FechaId).HasColumnName("FECHA_ID");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("FECHA");
            });

            modelBuilder.Entity<THora>(entity =>
            {
                entity.HasKey(e => e.HoraId)
                    .HasName("PK__HORA");

                entity.ToTable("T_HORA", "SCH_RECIQUEST");

                entity.Property(e => e.HoraId).HasColumnName("HORA_ID");

                entity.Property(e => e.Hora)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("HORA");
            });

            modelBuilder.Entity<TMateriale>(entity =>
            {
                entity.HasKey(e => e.MaterialId);

                entity.ToTable("T_MATERIALES", "SCH_RECIQUEST");

                entity.Property(e => e.MaterialId).HasColumnName("MATERIAL_ID");

                entity.Property(e => e.NombreMaterialId).HasColumnName("NOMBRE_MATERIAL_ID");

                entity.Property(e => e.Peso)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("PESO");

                entity.Property(e => e.PeticionId).HasColumnName("PETICION_ID");

                entity.HasOne(d => d.NombreMaterial)
                    .WithMany(p => p.TMateriales)
                    .HasForeignKey(d => d.NombreMaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MATERIALES_NOMBRE_MATERIAL");

                entity.HasOne(d => d.Peticion)
                    .WithMany(p => p.TMateriales)
                    .HasForeignKey(d => d.PeticionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MATERIALES_PETICION_ID");
            });

            modelBuilder.Entity<TNombreMaterial>(entity =>
            {
                entity.HasKey(e => e.NombreMaterialId)
                    .HasName("PK__NOMBRE_MATERIAL");

                entity.ToTable("T_NOMBRE_MATERIAL", "SCH_RECIQUEST");

                entity.Property(e => e.NombreMaterialId).HasColumnName("NOMBRE_MATERIAL_ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<TProgramarRecoleccion>(entity =>
            {
                entity.HasKey(e => e.PeticionId)
                    .HasName("PK__PROGRAMAR_RECOLECCION");

                entity.ToTable("T_PROGRAMAR_RECOLECCION", "SCH_RECIQUEST");

                entity.Property(e => e.PeticionId).HasColumnName("PETICION_ID");

                entity.Property(e => e.Canton)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CANTON");

                entity.Property(e => e.CodigoPostal).HasColumnName("CODIGO_POSTAL");

                entity.Property(e => e.DetallesEdificio)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DETALLES_EDIFICIO");

                entity.Property(e => e.Fecha).HasColumnName("FECHA");

                entity.Property(e => e.Hora).HasColumnName("HORA");

                entity.Property(e => e.Municipio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MUNICIPIO");

                entity.Property(e => e.Provincia).HasColumnName("PROVINCIA");

                entity.HasOne(d => d.FechaNavigation)
                    .WithMany(p => p.TProgramarRecoleccions)
                    .HasForeignKey(d => d.Fecha)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROGRAMAR_RECOLECCION_FECHA");

                entity.HasOne(d => d.HoraNavigation)
                    .WithMany(p => p.TProgramarRecoleccions)
                    .HasForeignKey(d => d.Hora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROGRAMAR_RECOLECCION_HORA");

                entity.HasOne(d => d.ProvinciaNavigation)
                    .WithMany(p => p.TProgramarRecoleccions)
                    .HasForeignKey(d => d.Provincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROGRAMAR_RECOLECCION_PROVINCIA");
            });

            modelBuilder.Entity<TProvincium>(entity =>
            {
                entity.HasKey(e => e.ProvinciaId)
                    .HasName("PK__PROVINCIA");

                entity.ToTable("T_PROVINCIA", "SCH_RECIQUEST");

                entity.Property(e => e.ProvinciaId).HasColumnName("PROVINCIA_ID");

                entity.Property(e => e.Provincia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PROVINCIA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
