using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Bodega
{
    public class SoyongContext : DbContext
    {
        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<Movimientos> Movimientos { get; set; }

        public SoyongContext(DbContextOptions<SoyongContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movimientos>()
                .HasOne(m => m.Articulos)
                .WithMany()
                .HasForeignKey(m => m.ArticuloId);
        }
    }
}
