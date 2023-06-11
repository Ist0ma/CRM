using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CRM.DAL.Realization.Contexts
{
    class MsSqlContext : Context
    {
        public MsSqlContext(IConfiguration configuration) : base (configuration) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetSection("DataAccessLayerAdapterSettings").GetSection("ConnectionStringMsSql").Value);
        }
    }
}
