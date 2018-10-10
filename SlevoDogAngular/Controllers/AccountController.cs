using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SlevoDogAngular.Models;
using SlevoDogAngular.Models.AccountViewModels;
using IdentityServer4.AccessTokenValidation;
using System.Collections.Generic;
using MlkPwgen;
using System.Text;
using SlevoDogAngular.Services;

namespace SlevoDogAngular.Controllers
{
    [Authorize]
    [Route("api/User")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private readonly AccountService accountService;
        //private IConfigurationRoot _configurationRoot;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger,
            AccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            this.accountService = accountService;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var role = User?.Claims?.Where(a => a.Value.Equals("Admin")).FirstOrDefault()?.Value ?? null;
                    string token = accountService.CreateToken(model.Email, role);
                    return Ok(new { Token = token });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return BadRequest();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest();
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest();
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterTest([FromBody]RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                    //Kontrola jestli uzivatel uz neexistuje
                    if (String.IsNullOrEmpty(user.NormalizedEmail))
                        user.NormalizedEmail = user.Email;

                    var exist = await _userManager.GetUserIdAsync(user);

                    if (String.IsNullOrEmpty(exist))
                    {
                        return BadRequest();
                    }

                    //osetreni username
                    // string usernameTemp = model.Email.Split('@')[0];

                    // user.UserName = usernameTemp;

                    //Create AspNet Identity User
                    IdentityResult res = await _userManager.CreateAsync(user, model.Password);
                    IdentityResult res2 = null;
                    if (res.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);

                        //prirazeni uzivatele do Role
                        string role = "Basic User";
                        res2 = await _userManager.AddToRoleAsync(user, role);

                        if (res2.Succeeded)
                        {
                            string token = accountService.CreateToken(model.Email, role);
                            return Ok(new { Token = token });
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                    return BadRequest();
                }

                // If we got this far, something failed, redisplay form
                return BadRequest();

            }
            catch (Exception e)
            {
                throw new Exception(nameof(e));
            }
            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            string role = "Basic User";

            if (result.Succeeded)
            {
                if (await _roleManager.FindByNameAsync(role) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                await _userManager.AddToRoleAsync(user, role);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", role));

                return Ok(new ProfileViewModel(user));
            }
            return BadRequest(result.Errors);
        }
    }
}
