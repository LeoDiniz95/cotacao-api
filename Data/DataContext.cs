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
    }
}
