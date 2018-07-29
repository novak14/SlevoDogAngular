using Admin.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Dal.Repository.Abstraction
{
    public interface IInsertAdminRepository
    {
        void Insert(SaleAdmin saleAdmin);
    }
}
