using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pizzeria_Toscana.Models;

namespace Pizzeria_Toscana.Models
{
    public class PizzerieContext : IdentityDbContext<User>
    {
        public PizzerieContext(DbContextOptions<PizzerieContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingrediente { get; set; }
        public DbSet<Comanda> Comenzi { get; set; }
        public DbSet<Produs> Produse { get; set; }
        public DbSet<Produs_Ingredient> Produs_Ingredient { get; set; }
        public DbSet<Comanda_Produs> Comanda_Produs { get; set; }
        public DbSet<Cos> Cos { get; set; }
        public DbSet<Cos_Produs>? Cos_Produs { get; set; }
        public DbSet<Categorie>? Categorie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Produs_Ingredient>()
               .HasOne(pi => pi.Produs)
               .WithMany(p => p.Produs_Ingredient)
               .HasForeignKey(pi => pi.COD_Produs)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cos_Produs>()
                .HasOne(cp => cp.Cos)
                .WithMany(c => c.CosProdus)
                .HasForeignKey(cp => cp.ID_Cos)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cos_Produs>()
                .HasOne(cp => cp.Produs)
                 .WithMany(p => p.CosProdus)
                .HasForeignKey(cp => cp.COD_Produs)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comanda_Produs>()
            .HasOne(cp => cp.Comanda)
            .WithMany(c => c.Comanda_Produs)
            .HasForeignKey(cp => cp.ComandaId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comanda_Produs>()
                .HasOne(cp => cp.Produs)
                .WithMany(p => p.Comanda_Produs)
                .HasForeignKey(cp => cp.ProdusId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }
    }
}
