using CRM.DAL.Realization.Contexts;
using DataAccessLayer.Realization;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class MsSqlUnitOfWork : UnitOfWork
    {
        public MsSqlUnitOfWork(IConfiguration configuration) : base()
        {
            db = new MsSqlContext(configuration);
        }
    }
}
