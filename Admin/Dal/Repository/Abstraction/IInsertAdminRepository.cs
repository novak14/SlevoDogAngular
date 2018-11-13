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
        Task<List<Shops>> GetShops(string shopName);
        Task<Shops> GetShopByName(string name);
        Task<int> InsertShop(string name, string searchName);
        Task InsertWholeKeyword(string fullKeyword, string keyword, int saleId);
        Task InsertOnlyKeywordSale(int keywordId, int saleId);
        Task<(bool keyword, bool keywordSale, int? keyWordId)> IsKeywordExist(string keyword, int saleId);
        Task<List<KeyWords>> GetKeyWordsSuggest(string keyword, int[] keywordIds);
    }
}
