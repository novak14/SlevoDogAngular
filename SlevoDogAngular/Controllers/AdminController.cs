using Admin.Business;
using Admin.Dal.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlevoDogAngular.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            if (ModelState.IsValid)
            {
                SaleAdmin saleAdmin = Mapper.Map<SaleAdmin>(saleAdminViewModel);
                var searchShopName = Regex.Replace(saleAdmin.NameShop, @"\s+", "").ToLower();

                saleAdmin.FkShop = (await _adminService.GetShopByName(searchShopName))?.Id ?? (await _adminService.InsertShop(saleAdmin.NameShop, searchShopName));

                await _adminService.InsertSaleAsync(saleAdmin);
            }
        }
        
        [HttpGet("[action]")]
        public async Task<List<Category>> GetCategories()
        {
            var categories = await _adminService.GetCategories();
            return categories;
        }

        [HttpGet("[action]")]
        public async Task<List<Shops>> GetShops(string shopName)
        {
            var shops = await _adminService.GetShops(shopName);
            return shops;
        }

        [HttpGet("[action]")]
        public IActionResult CheckUser()
        {
            var currentUser = HttpContext.User;
            var role = User.Claims.Where(a => a.Value.Equals("Admin")).FirstOrDefault().Value;

            return BadRequest();
        }

        [HttpGet("[action]")]
        public async Task<List<KeyWords>> GetKeyWordsSuggest(string keyword)
        {
            return await _adminService.GetKeyWordsSuggest(keyword);
        }
    }
}
