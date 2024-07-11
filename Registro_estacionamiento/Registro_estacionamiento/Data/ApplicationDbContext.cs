using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Registro_estacionamiento.Models;

namespace Registro_estacionamiento.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<VehiculosModel> Vehiculos { get; set; }
        public DbSet<RegistrosModel> Registros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehiculosModel>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<VehiculosModel>()
                .Property(v => v.NumeroDePlaca)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<VehiculosModel>()
                .HasMany(v => v.RegistrosModel)
                .WithOne(r => r.VehiculosModel)
                .HasForeignKey(r => r.VehiculoId);

            modelBuilder.Entity<RegistrosModel>()
            .HasKey(r => r.Id);

            modelBuilder.Entity<RegistrosModel>()
                .Property(r => r.CostoPorHora)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<RegistrosModel>()
                .Property(r => r.CostoTotal)
                .HasColumnType("decimal(10, 2)");
        }
    }
}
