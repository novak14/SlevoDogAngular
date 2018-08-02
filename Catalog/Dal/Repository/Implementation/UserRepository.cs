using Catalog.Configuration;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly CatalogOptions _options;

        public UserRepository(IOptions<CatalogOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
        }

        public async Task<User> GetUserByCookieAsync(string cookie)
        {
            User user = new User();
            string sql = @"SELECT * FROM UserProfile WHERE UniqueString = @Cookie;";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Cookie = cookie });
            }
            return user;
        }

        public async Task<string> InsertUserAsync(string username, string uniqueString)
        {
            string sql = @"INSERT INTO UserProfile(Username, UniqueString) VALUES(@Username, @UniqueString);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                var idUser = await connection.QuerySingleOrDefaultAsync<int>(sql, new { Username = username, UniqueString = uniqueString });
                string userCookie = await connection.QueryFirstOrDefaultAsync<string>("SELECT UniqueString FROM UserProfile WHERE Id = @id", new { id = idUser });
                return userCookie;
            }
            throw new NotImplementedException();
        }
    }
}
