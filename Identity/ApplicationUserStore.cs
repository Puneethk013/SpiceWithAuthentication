using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpiceWithoutAuthentication.Identity
{
    public class ApplicationUserStore:UserStore<AppUser>
    {
        public ApplicationUserStore(ApplicationDbContext dbContext):base(dbContext)
        {

        }
    }
}