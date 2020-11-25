using System.Security.Principal;
using Donor.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Donor.Business {
    public static class UserUtils {
        private static readonly ApplicationDbContext DbContext = ApplicationDbContext.Create();
        private static readonly OrmDonor ContextDonor = new OrmDonor();

        public static string GetUserName(string user){
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DbContext));
            var loggedUser = userManager.FindById(user);
            if(loggedUser.IdUsuario != null)
                return ContextDonor.Usuario.Find(loggedUser.IdUsuario)?.Nome;
            return "";
        }

        public static int? GetId(IPrincipal user){
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DbContext));
            var loggedUser = userManager.FindById(user.Identity.GetUserId());
            return ContextDonor.Usuario.Find(loggedUser.IdUsuario)?.IdUsuario;
        }
    }
}