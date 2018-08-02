using Admin.Configuration;
using Admin.Dal.Entities;
using Admin.Dal.Repository.Abstraction;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                            VALUES(@Name, @PriceAfterSale, @AveragePrice, @OriginPrice, @Image, @DateInsert, @ValidFrom, @ValidTo, @LinkFirm, @Description, @bDisabled, @PercentSale);";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                var affRows = await connection.ExecuteAsync(sql, new
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
            }
        }
    }
}
