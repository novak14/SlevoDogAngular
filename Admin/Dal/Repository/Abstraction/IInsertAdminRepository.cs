using Admin.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Dal.Repository.Abstraction
{
    public interface IInsertAdminRepository
    {
        Task<int> InsertAsync(SaleAdmin saleAdmin);
        Task<List<Category>> GetCategories();
        Task<List<Shops>> GetShops();
        Task<Shops> GetShopByName(string name);
        Task<int> InsertShop(string name, string searchName);
        Task InsertWholeKeyword(string keywords, int saleId);
        Task InsertOnlyKeywordSale(int keywordId, int saleId);
        Task<(bool keyword, bool keywordSale, int? keyWordId)> IsKeywordExist(string keyword, int saleId);
    }
}
