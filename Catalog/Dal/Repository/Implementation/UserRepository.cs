﻿using Catalog.Configuration;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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

        public User GetUserByCookie(string cookie)
        {
            User user = new User();
            string sql = @"SELECT * FROM User WHERE UniqueString = @Cookie;";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                user = connection.QueryFirstOrDefault<User>(sql, new { Cookie = cookie });
            }
            return user;
        }

        public User InsertUser(string username, string uniqueString)
        {
            string sql = @"INSERT INTO User(Username, UniqueString) VALUES(@Username, @UniqueString);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                connection.Open();
                var idUser = connection.Query<int>(sql, new { Username = username, UniqueString = uniqueString }).SingleOrDefault();
                var user = connection.QueryFirstOrDefault<User>("SELECT * FROM User WHERE Id = @id", new { id = idUser });
                connection.Close();
                return user;
            }
            throw new NotImplementedException();
        }
    }
}
