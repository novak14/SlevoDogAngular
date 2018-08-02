using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task<User> GetUserByCookieAsync(string cookie);
        Task<string> InsertUserAsync(string username, string uniqueString);
    }
}
