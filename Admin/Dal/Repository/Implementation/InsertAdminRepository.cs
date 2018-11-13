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

        public async Task<int> InsertAsync(SaleAdmin saleAdmin)
        {
            string sql = @"INSERT INTO Sale(Name, PriceAfterSale, AveragePrice, OriginPrice, Image, DateInsert, ValidFrom, ValidTo, LinkFirm, Description, bDisabled, PercentSale, FkShop) 
                            VALUES(@Name, @PriceAfterSale, @AveragePrice, @OriginPrice, @Image, @DateInsert, @ValidFrom, @ValidTo, @LinkFirm, @Description, @bDisabled, @PercentSale, @FkShop);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

            string sql2 = @"INSERT INTO CategorySale(FkCategoryId,FkSaleId) VALUES (@FkCategoryId, @FkSaleId);";
            int saleId;
            
            using (var connection = new SqlConnection(_options.connectionString))
            {
                saleId = await connection.QuerySingleOrDefaultAsync<int>(sql, new
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
                    PercentSale = saleAdmin.PercentSale,
                    FkShop = saleAdmin.FkShop
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
            return saleId;
        }

        public async Task<List<Category>> GetCategories()
        {
            using (var connection = new SqlConnection(_options.connectionString))
            {
                var categories = (await connection.QueryAsync<Category>("SELECT * FROM Category")).ToList();
                return categories;
            }
        }

        public async Task<List<Shops>> GetShops(string shopName)
        {
            using (var connection = new SqlConnection(_options.connectionString))
            {
                try {
                    shopName = shopName + "%";
                    var shops = (await connection.QueryAsync<Shops>("SELECT * FROM Shops WHERE SearchString LIKE @shopName", new { shopName = shopName})).ToList();
                return shops;

                }
                catch(Exception e) {
                    var tmp = e;
                }
            }
            return new List<Shops>();
        }

        public async Task<List<KeyWords>> GetKeyWordsSuggest(string keyword, int[] keywordIds)
        {
            //List<int> lst = keywordIds.OfType<int>().ToList();
            using (var connection = new SqlConnection(_options.connectionString))
            {
                try
                {
                    keyword = keyword + "%";
                    var keywords = (await connection.QueryAsync<KeyWords>("SELECT * FROM KeyWords WHERE Keyword LIKE @Keyword AND Id NOT IN @Ids", 
                        new { Keyword = keyword, Ids = keywordIds })
                        ).ToList();
                    return keywords;

                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
            return new List<KeyWords>();
        }

        public async Task<Shops> GetShopByName(string name)
        {
            using (var connection = new SqlConnection(_options.connectionString))
            {
                var shop = await connection.QueryFirstOrDefaultAsync<Shops>("SELECT * FROM Shops WHERE SearchString = @Name", new { Name = name });
                return shop;
            }
        }

        public async Task<int> InsertShop(string name, string searchName)
        {
            string sql = @"INSERT INTO Shops(Name, SearchString) VALUES(@Name, @SearchString);
                            SELECT CAST(SCOPE_IDENTITY() as int);";
            int shopId;
            using (var connection = new SqlConnection(_options.connectionString))
            {
                shopId = await connection.ExecuteAsync(sql, new { Name = name, SearchString = searchName });
            }
            return shopId;
        }

        public async Task InsertWholeKeyword(string fullKeyword, string keywords, int saleId)
        {
            string sql = @"INSERT INTO KeyWords(Keyword, FullKeyword) VALUES(@Keyword, @FullKeyword);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

            string sql2 = @"INSERT INTO KeywordSale(FkKeyWords, FkSaleId) VALUES(@FkKeyWords, @FkSaleId);";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                var keyWordId = await connection.ExecuteAsync(sql, new { Keyword = keywords, FullKeyword = fullKeyword });
                var affRows = await connection.ExecuteAsync(sql2, new { FkKeyWords = keyWordId, FkSaleId = saleId });
            }
        }

        public async Task<(bool keyword, bool keywordSale, int? keyWordId)> IsKeywordExist(string keyword, int saleId)
        {
            string sql = @"SELECT COUNT(*) AS Amount FROM KeywordSale WHERE FkKeyWords = @FkKeyWords AND FkSaleId = @FkSaleId";
            int keywordSales = 0;
            KeyWords keyWordId = null;
            using (var connection = new SqlConnection(_options.connectionString))
            {
                try
                {
                    keyWordId = await connection.QueryFirstOrDefaultAsync<KeyWords>("SELECT KeyWords.Id FROM KeyWords WHERE Keyword = @Keyword", new { Keyword = keyword });
                    if (keyWordId != null)
                    {
                        var keySales = await connection.QueryFirstOrDefaultAsync(sql, new { FkKeyWords = keyWordId.Id, FkSaleId = saleId });
                        keywordSales = (int)keySales.Amount;
                    }
                }
                catch(Exception e)
                {
                    throw new Exception(nameof(e));
                }
            }
            return (keyWordId != null ? true : false, keywordSales > 0 ? true : false, keyWordId?.Id);

        }

        public async Task InsertOnlyKeywordSale(int keywordId, int saleId)
        {
            string sql = @"INSERT INTO KeywordSale(FkKeyWords, FkSaleId) VALUES(@FkKeyWords, @FkSaleId);";

            using (var connection = new SqlConnection(_options.connectionString))
            {
                var affRows = await connection.ExecuteAsync(sql, new { FkKeyWords = keywordId, FkSaleId = saleId });
            }
        }
    }
}
