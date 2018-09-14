using jeive.Include;
using Microsoft.EntityFrameworkCore;

namespace jeive.Models
{
    public class DBConnect : DbContext
    {
        public DbSet<Variable.Config> api_config { get; set; } // Configuração API
        public DbSet<Variable.RegistroTransactions> api_Transactions { get; set; } // Registro 

        public DBConnect(DbContextOptions<DBConnect> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Maps.ConfigApi(builder);
            Maps.TransactionsApi(builder);
        } 
    }
}
