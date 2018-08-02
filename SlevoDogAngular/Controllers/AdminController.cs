using Admin.Business;
using Admin.Dal.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlevoDogAngular.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("[action]")]
        public async Task InsertItemAsync([FromBody]SaleAdminViewModel saleAdminViewModel)
        {
            if (ModelState.IsValid)
            {
                SaleAdmin saleAdmin = Mapper.Map<SaleAdmin>(saleAdminViewModel);

                await _adminService.InsertSaleAsync(saleAdmin);
            }
        }
    }
}
