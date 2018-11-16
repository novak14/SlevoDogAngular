using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get user by uniqueString which is id in aspnetuser
        /// </summary>
        /// <param name="idString">Id in aspnetuser</param>
        /// <returns></returns>
        Task<User> GetUserByUniqueString(string idString);

        /// <summary>
        /// Insert user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="uniqueString">Id in aspnetuser</param>
        /// <returns></returns>
        Task InsertUserAsync(string username, string email, string uniqueString);
    }
}
