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
    }
}
