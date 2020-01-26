using CreditConfirmation.API.Domain;
using CreditConfirmation.API.Entities;
using CreditConfirmation.API.Settings;
using DataAccessHandler.Infrastructure.Contexts;
using DataAccessHandler.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Infrastructure.Repositories
{
    public class UserRepository : BaseMongoRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IOptions<DbSettings> dbSettings) : base(dbSettings.Value.ConnectionString)
        {
        }
       
    }
}
