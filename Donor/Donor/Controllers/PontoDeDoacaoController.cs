using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Donor.Extension;
using Donor.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Donor.Controllers
{
    public class PontoDeDoacaoController : Controller
    {
        private readonly OrmDonor _db = new OrmDonor();
        private readonly ApplicationDbContext _dbAuth = new ApplicationDbContext();

        // GET: PontoDeDoacao
        public ActionResult Index()
        {
            var pontoDeDoacao = _db.PontoDeDoacao.Include(p => p.Municipio);
            return View(pontoDeDoacao.ToList());
        }

        // GET: PontoDeDoacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PontoDeDoacao pontoDeDoacao = _db.PontoDeDoacao.Find(id);
            if (pontoDeDoacao == null)
            {
                return HttpNotFound();
            }
            return View(pontoDeDoacao);
        }

        // GET: PontoDeDoacao/Create
        public ActionResult Create()
        {
            ViewBag.IdMunicipio = new SelectList(_db.Municipio, "IdMunicipio", "Municipio1");
            return View();
        }

        // POST: PontoDeDoacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPontoDeDoacao,Nome,Senha,ContaInativa,Token,Localizacao,IdMunicipio,NomeResponsavel,Telefone")] PontoDeDoacao pontoDeDoacao)
        {
            if (ModelState.IsValid)
            {
                _db.PontoDeDoacao.Add(pontoDeDoacao);
                _db.SaveChanges();

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_dbAuth));
                var loggedUser = userManager.FindById(User.Identity.GetUserId());
                if (loggedUser != null) {                    
                    userManager.AddToRole(loggedUser.Id, "PontoDoacao");
                    loggedUser.IdPontoDeDoacao = pontoDeDoacao.IdPontoDeDoacao;
                    userManager.CreateIdentity(loggedUser, DefaultAuthenticationTypes.ApplicationCookie);
                    User.AddUpdateClaim("Nome", pontoDeDoacao.Nome);
                    User.AddUpdateClaim("IdPontoDeDoacao", Convert.ToString(pontoDeDoacao.IdPontoDeDoacao));
                    userManager.Update(loggedUser);
                    _dbAuth.SaveChanges();
                }


                return RedirectToAction("Index");
            }

            ViewBag.IdMunicipio = new SelectList(_db.Municipio, "IdMunicipio", "Municipio1", pontoDeDoacao.IdMunicipio);
            return View(pontoDeDoacao);
        }

        // GET: PontoDeDoacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PontoDeDoacao pontoDeDoacao = _db.PontoDeDoacao.Find(id);
            if (pontoDeDoacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMunicipio = new SelectList(_db.Municipio, "IdMunicipio", "Municipio1", pontoDeDoacao.IdMunicipio);
            return View(pontoDeDoacao);
        }

        // POST: PontoDeDoacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPontoDeDoacao,Nome,Senha,ContaInativa,Token,Localizacao,IdMunicipio,NomeResponsavel,Telefone")] PontoDeDoacao pontoDeDoacao)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(pontoDeDoacao).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMunicipio = new SelectList(_db.Municipio, "IdMunicipio", "Municipio1", pontoDeDoacao.IdMunicipio);
            return View(pontoDeDoacao);
        }

        // GET: PontoDeDoacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PontoDeDoacao pontoDeDoacao = _db.PontoDeDoacao.Find(id);
            if (pontoDeDoacao == null)
            {
                return HttpNotFound();
            }
            return View(pontoDeDoacao);
        }

        // POST: PontoDeDoacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PontoDeDoacao pontoDeDoacao = _db.PontoDeDoacao.Find(id);
            _db.PontoDeDoacao.Remove(pontoDeDoacao);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
