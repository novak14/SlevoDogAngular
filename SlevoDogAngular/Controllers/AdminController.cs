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
    [Authorize(Roles = "Admin")]
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
            var role = User.Claims.Where(a => a.Value.Equals("Admin")).FirstOrDefault().Value;

            if (ModelState.IsValid && role.Equals("Admin"))
            {
                SaleAdmin saleAdmin = Mapper.Map<SaleAdmin>(saleAdminViewModel);

                await _adminService.InsertSaleAsync(saleAdmin);
            }
        }
        
        [HttpGet("[action]")]
        public async Task<List<Category>> GetCategories()
        {
            var currentUser = HttpContext.User;

            var categories = await _adminService.GetCategories();
            return categories;
        }

        [HttpGet("[action]")]
        public IActionResult CheckUser()
        {
            var currentUser = HttpContext.User;
            var role = User.Claims.Where(a => a.Value.Equals("Admin")).FirstOrDefault().Value;


            return BadRequest();
        }
    }
}
