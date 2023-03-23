using cotacao_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cotacao_api.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<CotacaoDM> CotacaoDMs { get; set; }
        public DbSet<CotacaoItemDM> CotacaoItemDMs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CotacaoItemDM>()
                .HasOne(p => p.Cotacao)
                .WithMany(b => b.CotacaoItens)
                .HasForeignKey(p => p.IdCotacao);
        }
    }
}
