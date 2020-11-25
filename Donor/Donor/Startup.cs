using Donor;
using Donor.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Donor
{
    public partial class Startup
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();        

        public void ConfigureRoles(){
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

            if (!roleManager.RoleExists("Administrador")){
                var role = new IdentityRole{Name = "Administrador"};
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Doador")) {
                var role = new IdentityRole{Name = "Doador"};
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("PontoDoacao")) {
                var role = new IdentityRole { Name = "PontoDoacao" };
                roleManager.Create(role);
            }            
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureRoles();
        }
    }
}
