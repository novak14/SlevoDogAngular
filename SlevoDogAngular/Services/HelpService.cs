using Microsoft.AspNetCore.Identity;
using SlevoDogAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SlevoDogAngular.Services
{
    public class HelpService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HelpService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Check if current user exist
        /// </summary>
        /// <param name="user">User from controller type of ClaimsPrincipal</param>
        /// <returns>User, if doesn't exist null</returns>
        public async Task<ApplicationUser> ExistUser(object user)
        {
            var checkUser = await _userManager.GetUserAsync((ClaimsPrincipal)user);

            if (checkUser == null)
                return null;
            return checkUser;
        }

        /// <summary>
        /// For nice print of date of added comment
        /// </summary>
        /// <param name="dt">some datetime</param>
        /// <returns></returns>
        public string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("před {0} {1}",
                years, years == 1 ? "rokem" : "roky");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days - (months * 30) > 15)
                    months += 1;
                return String.Format("před {0} {1}",
                months, months == 1 ? "měsícem" : "měsíci");
            }
            if (span.Days > 0)
                return String.Format("před {0} {1}",
                span.Days, span.Days == 1 ? "dnem" : "dny");
            if (span.Hours > 0)
                return String.Format("před {0} hod",
                span.Hours);
            if (span.Minutes > 0)
                return String.Format("před {0} min",
                span.Minutes);
            if (span.Seconds > 5)
                return String.Format("před {0} sekundami", span.Seconds);
            if (span.Seconds <= 5)
                return "právě teď";
            return string.Empty;
        }
    }
}
