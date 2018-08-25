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

        public async Task<List<Sale>> LoadAllAsync()
        {
            IEnumerable<Sale> sale = new List<Sale>();
            string sql = @"Select * from Sale WHERE bDisabled = 0";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                sale = await connection.QueryAsync<Sale>(sql);
            }
            return sale.ToList();
        }

        public async Task<Sale> LoadByIdAsync(int id)
        {
            Sale sale = new Sale();

            string sql = @"SELECT TOP(1) * FROM Sale
                        LEFT JOIN CategorySale 
                        ON Sale.Id = CategorySale.FkSaleId
                        LEFT JOIN Category 
                        ON Category.Id = CategorySale.FkCategoryId
                        AND Category.Disabled = 0
                        WHERE Sale.bDisabled = 0 AND Sale.Id = @Id";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                await connection.OpenAsync();
                var lookup = new Dictionary<int, Sale>();

                var test = await connection.QueryAsync<Sale, Category, Sale>(
                    sql,
                    (saleQuery, category) =>
                    {
                        Sale saleItem;
                        if (!lookup.TryGetValue(saleQuery.Id, out saleItem))
                            lookup.Add(saleQuery.Id, saleItem = saleQuery);

                        if (category != null)
                            saleQuery.Category = category;
                        return saleQuery;
                    }, new { Id = id });

                sale = test.FirstOrDefault();
            }
            return sale;
        }

        public async Task<List<Sale>> LoadCheapestAsync()
        {
            IEnumerable<Sale> sale = new List<Sale>();
            string sql = @"Select * from Sale WHERE bDisabled = 0 ORDER BY PriceAfterSale ASC";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                sale = await connection.QueryAsync<Sale>(sql);
            }
            return sale.ToList();
        }

        public async Task<List<Sale>> LoadNewestAsync()
        {
            IEnumerable<Sale> sale = new List<Sale>();
            string sql = @"Select * from Sale WHERE bDisabled = 0 ORDER BY DateInsert DESC";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                sale = await connection.QueryAsync<Sale>(sql);
            }
            return sale.ToList();
        }

        public async Task<List<Sale>> LoadBiggestSaleAsync()
        {
            IEnumerable<Sale> sale = new List<Sale>();
            string sql = @"Select * from Sale WHERE bDisabled = 0 ORDER BY PercentSale DESC";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                sale = await connection.QueryAsync<Sale>(sql);
            }
            return sale.ToList();
        }

        public async Task<List<Sale>> GetCategoryItems(int categoryId)
        {
            IEnumerable<Sale> sale = new List<Sale>();
            string sql = @"SELECT * FROM Sale
                           LEFT JOIN CategorySale 
                           ON Sale.Id = CategorySale.FkSaleId
                           AND Sale.bDisabled = 0
                           LEFT JOIN Category 
                           ON Category.Id = CategorySale.FkCategoryId
                           AND Category.Disabled = 0
                           WHERE Category.Id = @CategoryId";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                sale = await connection.QueryAsync<Sale, Category, Sale>(
                    sql, 
                    (saleItem, category) => {
                     return saleItem;
                },
                new { CategoryId = categoryId });
            }
            return sale.ToList();
        }

        public async Task AddRank(int saleId, int rank)
        {
            string sql = @"UPDATE Sale SET RankSale = @Rank WHERE Id = @Id;";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                var affRows = await connection.ExecuteAsync(sql, new
                {
                    Rank = rank,
                    Id = saleId
                });
            }
        }
    }
}
