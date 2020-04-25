using Microsoft.AspNet.Identity;
using Staticsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace Staticsite.Models
{
   
        public static class IdentityExtensions
        {
        public static string GetuserType(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Usertype");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static async Task<ApplicationUser> FindByNameOrEmailAsync
       (this UserManager<ApplicationUser> userManager, string usernameOrEmail, string password)
        {
            var username = usernameOrEmail;
            if (usernameOrEmail.Contains("@"))
            {
                var userForEmail = await userManager.FindByEmailAsync(usernameOrEmail);
                if (userForEmail != null)
                {
                    username = userForEmail.UserName;
                }
            }
            return await userManager.FindAsync(username, password);
        }
    }
    
}