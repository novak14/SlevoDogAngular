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

        public async Task<User> GetUserByUniqueString(string idString)
        {
            User user = new User();
            string sql = @"SELECT * FROM UserProfile WHERE UniqueString = @IdString;";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { IdString = idString });
            }
            return user;
        }

        public async Task InsertUserAsync(string username, string email, string uniqueString)
        {
            string sql = @"INSERT INTO UserProfile(Username, Email, UniqueString) VALUES(@Username, @Email, @UniqueString);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                var idUser = await connection.QuerySingleOrDefaultAsync<int>(sql, new { Username = username, Email = email, UniqueString = uniqueString });
            }
            throw new NotImplementedException();
        }
    }
}
