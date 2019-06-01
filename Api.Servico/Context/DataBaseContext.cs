using System.Data.Entity;
using API.Entidade;
using API.Migrations;

namespace API.Persistencia
{
    public class DataBaseContext : DbContext, IDbContext
    {
        public DataBaseContext() : base("Conexao")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataBaseContext, API.Migrations.Configuration>("Conexao"));
            this.Database.CommandTimeout = 300;
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
