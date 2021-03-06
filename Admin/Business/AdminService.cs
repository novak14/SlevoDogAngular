﻿using Admin.Dal.Entities;
using Admin.Dal.Repository.Abstraction;
using Shared.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
            var saleId = await _insertAdminRepository.InsertAsync(saleAdmin);

            await InsertKeyword(saleAdmin.Keywords, saleAdmin.KeywordIds, saleId);
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _insertAdminRepository.GetCategories();
            return categories;
        }

        public async Task<List<Shops>> GetShops(string shopName)
        {
            var shops = await _insertAdminRepository.GetShops(shopName);
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

        public async Task<bool> InsertKeyword(string[] keywords, int[] keywordsIds, int saleId)
        {
            foreach (var id in keywordsIds)
            {
                await _insertAdminRepository.InsertOnlyKeywordSale(id, saleId);
            }

            foreach (var item in keywords)
            {
                var editKeyword = Regex.Replace(item, @"\s+", "").ToLower();

                var keyWordsCheck = await _insertAdminRepository.IsKeywordExist(editKeyword, saleId);

                if (keyWordsCheck.keyword && keyWordsCheck.keywordSale)
                {
                    return false;
                }
                else if (keyWordsCheck.keyword && !keyWordsCheck.keywordSale)
                {
                    await _insertAdminRepository.InsertOnlyKeywordSale(keyWordsCheck.keyWordId.Value, saleId);
                }
                else
                {
                    await _insertAdminRepository.InsertWholeKeyword(item, editKeyword, saleId);
                }
            }
            return true;
        }

        public async Task<List<KeyWords>> GetKeyWordsSuggest(string keyword, int[] keywordIds)
        {
            return await _insertAdminRepository.GetKeyWordsSuggest(keyword, keywordIds);
        }
    }
}
