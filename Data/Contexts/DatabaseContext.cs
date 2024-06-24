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

            });

            modelBuilder.Entity<ColetaModel>(entity =>
            {
                entity.ToTable("Coleta");
            });

            modelBuilder.Entity<RotaModel>(entity =>
            {
                entity.ToTable("Rota");
            });

            modelBuilder.Entity<StatusModel>(entity =>
            {
                entity.ToTable("Status");
            });


        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext()
        {
        }
    }
}
