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

namespace Catalog.Dal.Repository.Implementation
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly CatalogOptions _options;

        public CommentsRepository(IOptions<CatalogOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
        }

        public List<Comments> GetComments(int saleId)
        {
            using (var connection = new SqlConnection(_options.connectionString))
            {
                return connection.Query<Comments>("SELECT * FROM Comments WHERE FkSale = @Id", new { Id = saleId }).ToList();
            }
        }
    }
}
