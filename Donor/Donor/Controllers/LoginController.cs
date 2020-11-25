using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Donor.Extension;
using Donor.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Donor.Controllers
{
    public class LoginController : Controller
    {

        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly OrmDonor _dbContext = new OrmDonor();

        public LoginController(ApplicationSignInManager signInManager, ApplicationUserManager userManager){
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public ApplicationSignInManager SignInManager => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

        public ApplicationUserManager UserManager => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();


        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("login")]
        public async Task<JsonResult> Login([FromBody] ExternalLoginViewModel externalLogin){
            var loginInfo = new ExternalLoginInfo{
                Email = externalLogin.Email,
                DefaultUserName = externalLogin.Login,
                Login = new UserLoginInfo("Google", externalLogin.IdGoogle)                
            };

            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result){
                case SignInStatus.Success:
                    var id = Convert.ToInt32(User.GetClaimValue("IdUsuario"));
                    var usuario = _dbContext.Usuario.FirstOrDefault(usuario2 => usuario2.IdUsuario == id);
                    return Json(usuario);
                default:
                    return Json(new { erro = "Erro"});
            }
            //return Json(new Usuario());
        }
    }
}
