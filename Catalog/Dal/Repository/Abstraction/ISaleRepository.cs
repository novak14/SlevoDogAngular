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
        Task AddRank(int saleId, int rank);
    }
}
