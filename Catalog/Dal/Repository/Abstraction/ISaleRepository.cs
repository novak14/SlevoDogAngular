using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface ISaleRepository
    {
        /// <summary>
        /// Load all sales unfiltered
        /// </summary>
        /// <returns></returns>
        Task<List<Sale>> LoadAllAsync();

        /// <summary>
        /// Load one Sale by its Id
        /// </summary>
        /// <param name="id">Unique identification of sale</param>
        /// <returns></returns>
        Task<Sale> LoadByIdAsync(int id);

        /// <summary>
        /// Load all sales from cheapest one
        /// </summary>
        /// <returns></returns>
        Task<List<Sale>> LoadCheapestAsync();

        /// <summary>
        /// Load all sales from newest one
        /// </summary>
        /// <returns></returns>
        Task<List<Sale>> LoadNewestAsync();

        /// <summary>
        /// Load all sales from biggest sale
        /// </summary>
        /// <returns></returns>
        Task<List<Sale>> LoadBiggestSaleAsync();

        /// <summary>
        /// Load collection of sales, which is in specific category
        /// </summary>
        /// <param name="categoryId">Unique identification of category</param>
        /// <returns></returns>
        Task<List<Sale>> GetCategoryItems(int categoryId);

        /// <summary>
        /// Update sale by rank
        /// </summary>
        /// <param name="saleId">Unique identification of sale</param>
        /// <param name="rank">New rank for sale</param>
        /// <returns></returns>
        Task AddRank(int saleId, int rank);

        /// <summary>
        /// Load all categories
        /// </summary>
        /// <returns></returns>
        Task<List<Category>> GetCategories();

        /// <summary>
        /// Check if user already ranked specific sale
        /// </summary>
        /// <param name="saleId">Unique identification of sale</param>
        /// <param name="userId">Unique identification of user</param>
        /// <returns></returns>
        Task<int> CheckRankUser(int saleId, int userId);

        /// <summary>
        /// Add saleId and userId to RankSaleUser 
        /// </summary>
        /// <param name="saleId">Unique identification of sale</param>
        /// <param name="userId">Unique identification of user</param>
        /// <returns></returns>
        Task ConnectUserRank(int saleId, int userId);

        /// <summary>
        /// Load collection of sales, based on keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<Sale>> GetSalesSuggest(string keyword);
    }
}
