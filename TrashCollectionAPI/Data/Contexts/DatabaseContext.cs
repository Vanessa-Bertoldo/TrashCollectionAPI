using Microsoft.EntityFrameworkCore;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<CaminhaoModel> Caminhao { get; set; }
        public virtual DbSet<ColetaModel> Coleta { get; set; }
        public virtual DbSet<RotaModel> Rota { get; set; }
        public virtual DbSet<StatusModel> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaminhaoModel>(entity =>
            {
                entity.ToTable("Caminhao"); 
                entity.HasKey(e => e.IdCaminhao); 

                entity.Property(e => e.HNumeroMaxCapacidade).IsRequired(); 

                entity.HasOne(e => e.Status) 
                      .WithMany() 
                      .HasForeignKey(e => e.IdStatus) 
                      .IsRequired(); 
            });
            modelBuilder.Entity<ColetaModel>(entity =>
            {
                entity.ToTable("Coleta");
                entity.HasKey(e => e.IdColeta);
                entity.Property(e => e.NomeBairro).IsRequired();
                entity.Property(e => e.NumeroVolume).IsRequired();
                entity.Property(e => e.DataColeta).IsRequired();
                entity.Property(e => e.DataRegistro).IsRequired();

                entity.HasMany(e => e.Rotas)
                      .WithOne(r => r.Coleta)
                      .HasForeignKey(r => r.IdColeta)
                      .IsRequired();
            });

            modelBuilder.Entity<RotaModel>(entity =>
            {
                entity.ToTable("Rota");
                entity.HasKey(e => e.IdRota);
                entity.Property(e => e.NomeRota).IsRequired();
                entity.Property(e => e.DescricaoRota).IsRequired();

                entity.HasOne(r => r.Coleta)
                      .WithMany(c => c.Rotas)
                      .HasForeignKey(r => r.IdColeta)
                      .IsRequired();
            });

            modelBuilder.Entity<StatusModel>(entity =>
            {
                entity.ToTable("Status"); 
                entity.HasKey(e => e.IdStatus);
               
            });
        }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected DatabaseContext() { }
    }
}
