using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpiceWithoutAuthentication.Identity
{
    public class ApplicationUserManager:UserManager<AppUser>
    {
        public ApplicationUserManager(IUserStore<AppUser> store):base(store)
        {

        }
    }
}