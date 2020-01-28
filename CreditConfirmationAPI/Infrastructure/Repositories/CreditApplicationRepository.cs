using CreditConfirmation.API.Domain;
using CreditConfirmation.API.Entities;
using CreditConfirmation.API.Settings;
using DataAccessHandler.Infrastructure.Contexts;
using DataAccessHandler.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Infrastructure.Repositories
{
    public class CreditApplicationRepository : BaseMongoRepository<CreditApplication>, ICreditApplicationRepository
    {
        public CreditApplicationRepository(IOptions<DbSettings> dbSettings) : base(dbSettings.Value.ConnectionString)
        {
        }
       
    }
}
