using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface ISaleRepository
    {
        Task<List<Sale>> LoadAllAsync();
        Task<Sale> LoadByIdAsync(int id);
        Task<List<Sale>> LoadCheapestAsync();
        Task<List<Sale>> LoadNewestAsync();
        Task<List<Sale>> LoadBiggestSaleAsync();
        Task<List<Sale>> GetCategoryItems(int categoryId);
        Task AddRank(int saleId, int rank);
        Task<List<Category>> GetCategories();
        Task<int> CheckRankUser(int saleId, int userId);
        Task ConnectUserRank(int saleId, int userId);
    }
}
