using Bleeter.Model;
using ElCamino.AspNet.Identity.AzureTable;
using ElCamino.AspNet.Identity.AzureTable.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter
{
    public class AuthRepository : IDisposable
    {
        private UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public async Task<ApplicationUser> FindUserAsync(string username, string password)
        {
            var user = await _userManager.FindAsync(username, password);
            return user;
        }

        public async Task<IdentityResult> RegisterUserAsync(string username, string displayName, string password)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = username,
                DisplayName = displayName
            };
            return await _userManager.CreateAsync(user, password);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
