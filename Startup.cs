using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SpiceWithoutAuthentication.Identity;
using SpiceWithoutAuthentication.Models;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(SpiceWithoutAuthentication.Startup))]

namespace SpiceWithoutAuthentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Account/Login") });
            this.CreateRoleAndUsers();
        }
        public void CreateRoleAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var appDbContext = new ApplicationDbContext();
            var appUserStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(appUserStore);

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }


            if (userManager.FindByName("admin") == null)
            {
                var user = new AppUser();
                user.Name = "admin";
                user.Email = "admin@gmail.com";
                string userPassword = "Temp@1234";
                var chkUser = userManager.Create(user, userPassword);
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
            //create Customer user
            if (userManager.FindByName("customer") == null)
            {
                var user = new AppUser();
                user.Name = "customer";
                user.Email = "customer@gmail.com";
                string userPassword = "Temp@1234";
                var chkuser = userManager.Create(user, userPassword);
                if (chkuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                }
            }
        }
    }
}
