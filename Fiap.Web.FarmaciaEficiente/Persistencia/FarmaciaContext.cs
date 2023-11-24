using Microsoft.EntityFrameworkCore;
using Fiap.Web.FarmaciaEficiente.Models;

namespace Fiap.Web.FarmaciaEficiente.Persistencia
{
    public class FarmaciaContext : DbContext
    {
        public FarmaciaContext(DbContextOptions<FarmaciaContext> options) : base(options) { }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Posto> Postos { get; set; }
        public DbSet<Retirada> Retiradas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Retirada>()
                .HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Estoque>()
               .HasOne(e => e.Medicamento)
               .WithMany()
               .HasForeignKey(e => e.MedicamentoId)
               .IsRequired();

            modelBuilder.Entity<Estoque>()
                .HasOne(e => e.Posto)
                .WithMany()
                .HasForeignKey(e => e.PostoId)
                .IsRequired();





        }



    }
}
