using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface ISaleRepository
    {
        IEnumerable<Sale> LoadAll();
        Task<Sale> LoadByIdAsync(int id);
        void InsertComment(Comments comments);
        List<Sale> LoadCheapest();
        List<Sale> LoadNewest();
        List<Sale> LoadBiggestSale();
    }
}
