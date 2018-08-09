using Admin.Configuration;
using Admin.Dal.Entities;
using Admin.Dal.Repository.Abstraction;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Dal.Repository.Implementation
{
    public class InsertAdminRepository : IInsertAdminRepository
    {
        private readonly AdminOptions _options;

        public InsertAdminRepository(IOptions<AdminOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
        }

        public async Task InsertAsync(SaleAdmin saleAdmin)
        {
            string sql = @"INSERT INTO Sale(Name, PriceAfterSale, AveragePrice, OriginPrice, Image, DateInsert, ValidFrom, ValidTo, LinkFirm, Description, bDisabled, PercentSale) 
                            VALUES(@Name, @PriceAfterSale, @AveragePrice, @OriginPrice, @Image, @DateInsert, @ValidFrom, @ValidTo, @LinkFirm, @Description, @bDisabled, @PercentSale);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

            string sql2 = @"INSERT INTO CategorySale(FkCategoryId,FkSaleId) VALUES (@FkCategoryId, @FkSaleId);";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                var saleId = await connection.QuerySingleOrDefaultAsync<int>(sql, new
                {
                    Name = saleAdmin.Name,
                    PriceAfterSale = saleAdmin.PriceAfterSale,
                    AveragePrice = saleAdmin.AveragePrice,
                    OriginPrice = saleAdmin.OriginPrice,
                    Image = saleAdmin.Image,
                    DateInsert = saleAdmin.DateInsert,
                    ValidFrom = saleAdmin.ValidFrom,
                    ValidTo = saleAdmin.ValidTo,
                    LinkFirm = saleAdmin.LinkFirm,
                    Description = saleAdmin.Description,
                    bDisabled = saleAdmin.Disabled,
                    PercentSale = saleAdmin.PercentSale
                });

                try
                {
                    foreach (var item in saleAdmin.CheckedCategories)
                    {
                        var test = await connection.ExecuteAsync(sql2, new { FkCategoryId = item, FkSaleId = saleId });

                    }
                } catch( Exception e)
                {
                    var test = e;
                }
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            using (var connection = new SqlConnection(_options.connectionString))
            {
                var categories = (await connection.QueryAsync<Category>("SELECT * FROM Category")).ToList();
                return categories;
            }
        }
    }
}
