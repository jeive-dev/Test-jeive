using Microsoft.EntityFrameworkCore;
using static jeive.Include.Variable;

namespace jeive.Models
{
    public class Maps 
    {

        public static void ConfigApi(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.Entity<Config>(set =>
            {
                set.ToTable("api_config");
                set.HasKey(c => c.Id).HasName("id");
                set.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
                set.Property(c => c.Loja).HasColumnName("loja").HasMaxLength(100);
                set.Property(c => c.Master).HasColumnName("Master").HasMaxLength(30);
                set.Property(c => c.Visa).HasColumnName("Visa").HasMaxLength(30);
                set.Property(c => c.Safety).HasColumnName("Safety").HasMaxLength(30);
            });
        }

        public static  void TransactionsApi(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.Entity<RegistroTransactions>(set =>
            {
                set.ToTable("api_Transactions");
                set.HasKey(p => p.Id).HasName("id");
                set.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
                set.Property(c => c.Loja).HasColumnName("loja").HasMaxLength(100);
                set.Property(c => c.Registro).HasColumnName("registro").HasMaxLength(100);
            });
        }
    }
}
