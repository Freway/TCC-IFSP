using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Donor.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual Usuario Usuario{ get; set; }
        public virtual PontoDeDoacao PontoDeDoacao{ get; set; }
        public int? IdUsuario{ get; set; }
        public int? IdPontoDeDoacao{ get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Observe que o authenticationType deve corresponder àquele definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            var ormDonor = new OrmDonor();
            var usuario = ormDonor.Usuario.Find(IdUsuario);
            if (usuario != null){
                userIdentity.AddClaim(new Claim("Nome", usuario.Nome));
                userIdentity.AddClaim(new Claim("IdUsuario", Convert.ToString(usuario.IdUsuario)));
            }
            // Adicionar declarações de usuário personalizado aqui
            return userIdentity;
        }

        public ApplicationUser(){
            IdUsuario = null;
            IdPontoDeDoacao = null;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ORMDonor", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}