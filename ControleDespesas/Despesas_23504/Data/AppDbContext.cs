using Microsoft.EntityFrameworkCore;
using Despesas_23504.Models;

namespace Despesas_23504.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Despesa> Despesas { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            // Configurações adicionais (opcional)
            //modelBuilder.Entity<Despesa>().ToTable("Despesas");
        //}
    }
}

