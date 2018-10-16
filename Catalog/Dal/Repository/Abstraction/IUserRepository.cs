using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task<User> GetUserByUniqueString(string idString);
        Task InsertUserAsync(string username, string email, string uniqueString);
    }
}
