using ElCamino.AspNet.Identity.AzureTable;
using ElCamino.AspNet.Identity.AzureTable.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }

    public class ApplicationDbContext : IdentityCloudContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base()
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> userStore)
            : base(userStore)
        {
        }

        public static async Task StartupAsync()
        {
            var azureStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            await azureStore.CreateTablesIfNotExists();
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> identitiyOptions, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            return manager;
        }
    }
}
