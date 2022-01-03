using EPSMGest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EpsmGest.Data
{
    public class EpsmGestDbContext : IdentityDbContext
    {
        public EpsmGestDbContext(DbContextOptions<EpsmGestDbContext> options)
            : base(options)
        { }

        public DbSet<ComprasModel> Compra { get; set; }

        public DbSet<DepartamentoModel> Departamento { get; set; }

        public DbSet<FaturasModel> Fatura { get; set; }

        public DbSet<FornecedorModel> Fornecedor { get; set; }

        public DbSet<RequisicoesModel> Requisicoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ComprasModel>();

            builder.Entity<DepartamentoModel>();

            builder.Entity<FaturasModel>();

            builder.Entity<RequisicoesModel>();

            builder.Entity<FornecedorModel>();

        }
    }
}