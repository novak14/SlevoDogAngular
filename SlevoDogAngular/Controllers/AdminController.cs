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
        public void InsertItem([FromBody]SaleAdminViewModel saleAdminViewModel)
        {
            if (ModelState.IsValid)
            {
                SaleAdmin saleAdmin = Mapper.Map<SaleAdmin>(saleAdminViewModel);

                _adminService.InsertSale(saleAdmin);
            }
        }

        [HttpPost("[action]")]
        public void InsertAny(string Name)
        {
            var test = Name;
            //if (ModelState.IsValid)
            //{
            //    SaleAdmin saleAdmin = Mapper.Map<SaleAdmin>(saleAdminViewModel);

            //    _adminService.InsertSale(saleAdmin);
            //}
        }

        [HttpPost("[action]")]
        public void Register(string email)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            //    var result = await _userManager.CreateAsync(user, model.Password);
            //    if (result.Succeeded)
            //    {
            //        _logger.LogInformation("User created a new account with password.");

            //        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //       // var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            //       // await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        _logger.LogInformation("User created a new account with password.");
            //        return RedirectToLocal(returnUrl);
            //    }
            //    AddErrors(result);
            //}

            // If we got this far, something failed, redisplay form
            var test = email;
        }
    }
}
