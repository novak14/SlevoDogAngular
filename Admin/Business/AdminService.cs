using Admin.Dal.Entities;
using Admin.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Business
{
    public class AdminService
    {
        private readonly IInsertAdminRepository _insertAdminRepository;

        public AdminService(IInsertAdminRepository insertAdminRepository)
        {
            _insertAdminRepository = insertAdminRepository;
        }

        public async Task InsertSaleAsync(SaleAdmin saleAdmin)
        {
            saleAdmin.DateInsert = DateTime.Now;
            await _insertAdminRepository.InsertAsync(saleAdmin);
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _insertAdminRepository.GetCategories();
            return categories;
        }

        public async Task<List<Shops>> GetShops()
        {
            var shops = await _insertAdminRepository.GetShops();
            return shops;
        }

        public async Task<Shops> GetShopByName(string name)
        {
            var shop = await _insertAdminRepository.GetShopByName(name);
            return shop;
        }

        public async Task<int> InsertShop(string name, string searchName)
        {
            var shopId = await _insertAdminRepository.InsertShop(name, searchName);
            return shopId;
        }
    }
}
