using System.Web.Mvc;
using Donor.Models;

namespace Donor.Controllers
{
    public class TipoContaSelecaoController : Controller
    {
        // GET: TipoContaSelecao
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedirecionaTipoConta(TipoContaSelecaoViewModel model){
            if (!ModelState.IsValid){
                return View("Index");
            }

            return RedirectToAction("Create", model.TipoConta == "U" ? "Usuario" : "PontoDeDoacao");
        }
    }
}