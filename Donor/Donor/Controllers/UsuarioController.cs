using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Donor.Extension;
using Donor.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Donor.Controllers
{
    [System.Web.Mvc.Authorize]
    public class UsuarioController : Controller
    {
        private readonly OrmDonor _db = new OrmDonor();
        private readonly ApplicationDbContext _dbAuth = new ApplicationDbContext();
        // GET: Usuario
        [System.Web.Mvc.Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var usuario = _db.Usuario.Include(u => u.Municipio).Include(u => u.TipoUsuario);
            return View(usuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = _db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create(){           
            ViewBag.IdMunicipio = new SelectList(_db.Municipio.OrderBy(p => p.Municipio1), "IdMunicipio", "Municipio1");            
            ViewBag.IdTipoUsuario = new SelectList(_db.TipoUsuario, "IdTipoUsuario", "TipoUsuario1");
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,Nome,Sexo,Email,TipoSanguineo,Telefone,IdMunicipio,IdTipoUsuario,ContaInativa,DoadorMedula,Token")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _db.Usuario.Add(usuario);
                _db.SaveChanges();

//                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbAuth));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_dbAuth));
                var loggedUser = userManager.FindById(User.Identity.GetUserId());
                if (loggedUser != null){
                    var strTipoUsuario = _db.TipoUsuario.Find(usuario.IdTipoUsuario)?.TipoUsuario1;
                    userManager.AddToRole(loggedUser.Id, strTipoUsuario);
                    loggedUser.IdUsuario = usuario.IdUsuario;
                    userManager.CreateIdentity(loggedUser, DefaultAuthenticationTypes.ApplicationCookie);
                    User.AddUpdateClaim("Nome", usuario.Nome);
                    User.AddUpdateClaim("IdUsuario", Convert.ToString(usuario.IdUsuario));
                    userManager.Update(loggedUser);
                    _dbAuth.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.IdMunicipio = new SelectList(_db.Municipio, "IdMunicipio", "Municipio1", usuario.IdMunicipio);
            ViewBag.IdTipoUsuario = new SelectList(_db.TipoUsuario, "IdTipoUsuario", "TipoUsuario1", usuario.IdTipoUsuario);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = _db.Usuario.Include("Municipios").Include("Municipio").FirstOrDefault(usuario1 => usuario1.IdUsuario == id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoSel = usuario.Municipio.Estado;
            ViewBag.Municipios = usuario.Municipios;
            ViewBag.IdMunicipio = new SelectList(_db.Municipio, "IdMunicipio", "Municipio1", usuario.IdMunicipio);
            ViewBag.IdTipoUsuario = new SelectList(_db.TipoUsuario, "IdTipoUsuario", "TipoUsuario1", usuario.IdTipoUsuario);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,Nome,Sexo,Email,TipoSanguineo,Telefone,IdMunicipio,IdTipoUsuario,ContaInativa,DoadorMedula,Token")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(usuario).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMunicipio = new SelectList(_db.Municipio, "IdMunicipio", "Municipio1", usuario.IdMunicipio);
            ViewBag.IdTipoUsuario = new SelectList(_db.TipoUsuario, "IdTipoUsuario", "TipoUsuario1", usuario.IdTipoUsuario);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = _db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuario = _db.Usuario.Find(id);
            if (usuario != null){
                usuario.ContaInativa = true;
                //_db.Usuario.Remove(usuario);
                _db.Entry(usuario);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult AddEstados([FromBody]UsuarioViewModel usuario){

            var u = _db.Usuario.Include("Municipios").FirstOrDefault(usuario1 => usuario1.IdUsuario == usuario.IdUsuario);

            if (u != null){
                foreach (var usuarioListCidade in usuario.ListCidades){
                    if(!u.Municipios.Contains(_db.Municipio.Find(usuarioListCidade)))
                        u.Municipios.Add(_db.Municipio.Find(usuarioListCidade));
                }
                _db.Entry(u);
                _db.SaveChanges();
            }

            return Json(new{ usuario });
        }

        public JsonResult GetMunicipiosByEstado([FromBody]string estado) {
            var estadosObj = _db.Municipio.Where(municipio => municipio.Estado == estado).ToList();

            return Json(estadosObj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEstados(){
            var municipiosObj = (from st in _db.Municipio
                              group st by st.Estado into g
                              orderby g.Key
                              select g.Key).ToList();
            
            return Json(municipiosObj, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
