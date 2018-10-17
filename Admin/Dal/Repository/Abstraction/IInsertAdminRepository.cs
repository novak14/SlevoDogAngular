using Admin.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Dal.Repository.Abstraction
{
    public interface IInsertAdminRepository
    {
        Task InsertAsync(SaleAdmin saleAdmin);
        Task<List<Category>> GetCategories();
        Task<List<Shops>> GetShops();
        Task<Shops> GetShopByName(string name);
        Task<int> InsertShop(string name, string searchName);
    }
}
