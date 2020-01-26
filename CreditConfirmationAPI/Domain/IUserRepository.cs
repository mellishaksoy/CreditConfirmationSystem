using CreditConfirmation.API.Entities;
using DataAccessHandler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditConfirmation.API.Domain
{
    public interface IUserRepository : IMongoRepository<UserEntity>
    {

    }
}
