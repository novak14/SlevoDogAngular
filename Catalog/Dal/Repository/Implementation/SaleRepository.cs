using Catalog.Configuration;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Catalog.Dal.Context;
using System.Linq;
using Dapper;

namespace Catalog.Dal.Repository.Implementation
{
    public class SaleRepository : ISaleRepository
    {
        private readonly CatalogOptions _options;
        private readonly CatalogDbContext _context;

        public SaleRepository(IOptions<CatalogOptions> options, CatalogDbContext context)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _context = context;
        }

        public IEnumerable<Sale> LoadAll()
        {
            IEnumerable<Sale> sale = new List<Sale>();
            string sql = @"Select * from Sale WHERE bDisabled = 0";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                sale = connection.Query<Sale>(sql);
            }
            return sale;
        }

        public async Task<Sale> LoadByIdAsync(int id)
        {
            Sale sale = new Sale();

            string sql = @"SELECT * FROM Sale
                        LEFT JOIN Comments 
                        ON Sale.Id = Comments.FkSale
                        AND Comments.Disabled = 0
                        WHERE Sale.bDisabled = 0 AND Sale.Id = @Id";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                await connection.OpenAsync();
                var lookup = new Dictionary<int, Sale>();

                var test = await connection.QueryAsync<Sale, Comments, Sale>(
                    sql,
                    (saleQuery, comments) =>
                    {
                        Sale saleItem;
                        if (!lookup.TryGetValue(saleQuery.Id, out saleItem))
                            lookup.Add(saleQuery.Id, saleItem = saleQuery);

                        if (comments != null)
                            saleItem.Comments.Add(comments);
                        return saleQuery;
                    }, new { Id = id });

                sale = test.FirstOrDefault();
            }
            return sale;
        }

        public void InsertComment(Comments comments)
        {
            string sql = @"INSERT INTO Comments(FkSale, DateInsert, FkUser, Name, Rank, Text, FkParrentComment, Disabled) 
                            VALUES(@FkSale, @DateInsert, @FkUser, @Name, @Rank, @Text, @FkParrentComment, @Disabled);";
            try
            {
                using (var connection = new SqlConnection(_options.connectionString))
                {
                    var affRows = connection.Execute(sql, new
                    {
                        FkSale = comments.FkSale,
                        DateInsert = comments.DateInsert,
                        FkUser = comments.FkUser,
                        Name = comments.Name,
                        Rank = comments.Rank,
                        Text = comments.Text,
                        FkParrentComment = comments.FkParrentComment,
                        Disabled = comments.Disabled
                    });
                }
            }
            catch (Exception e)
            {
                var ts = e;
            }
        }

        public List<Sale> LoadCheapest()
        {
            IEnumerable<Sale> sale = new List<Sale>();
            string sql = @"Select * from Sale WHERE bDisabled = 0 ORDER BY PriceAfterSale ASC";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                sale = connection.Query<Sale>(sql);
            }
            return sale.ToList();
        }

        public List<Sale> LoadNewest()
        {
            IEnumerable<Sale> sale = new List<Sale>();
            string sql = @"Select * from Sale WHERE bDisabled = 0 ORDER BY DateInsert DESC";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                sale = connection.Query<Sale>(sql);
            }
            return sale.ToList();
        }

        public List<Sale> LoadBiggestSale()
        {
            IEnumerable<Sale> sale = new List<Sale>();
            string sql = @"Select * from Sale WHERE bDisabled = 0 ORDER BY PercentSale DESC";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                sale = connection.Query<Sale>(sql);
            }
            return sale.ToList();
        }
    }
}
