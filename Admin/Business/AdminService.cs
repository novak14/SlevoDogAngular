using Admin.Dal.Entities;
using Admin.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Business
{
    public class AdminService
    {
        private readonly IInsertAdminRepository _insertAdminRepository;

        public AdminService(IInsertAdminRepository insertAdminRepository)
        {
            _insertAdminRepository = insertAdminRepository;
        }

        public void InsertSale(SaleAdmin saleAdmin)
        {
            saleAdmin.DateInsert = DateTime.Now;
            _insertAdminRepository.Insert(saleAdmin);
        }
    }
}
