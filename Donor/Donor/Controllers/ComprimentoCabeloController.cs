using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Donor.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Donor.Controllers
{
    public class ComprimentoCabeloController : Controller
    {
        private OrmDonor db = new OrmDonor();

        private readonly ApplicationDbContext _dbAuth = new ApplicationDbContext();

        public ComprimentoCabeloController()
        {

        }

        // GET: ComprimentoCabelo
        public ActionResult Index()
        {

            var identity = (ClaimsIdentity)User.Identity;
            int idUsuarioLogado = Convert.ToInt32(identity.Claims.FirstOrDefault(claim => claim.Type == "IdUsuario")?.Value);
            var comprimentoCabelo = db.ComprimentoCabelo.Include(c => c.Usuario).Where(c => c.IdUsuario == idUsuarioLogado);
            ViewBag.IdUsuarioLogado = idUsuarioLogado;
            return View(comprimentoCabelo.ToList());
        }

        // GET: ComprimentoCabelo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComprimentoCabelo comprimentoCabelo = db.ComprimentoCabelo.Find(id);
            if (comprimentoCabelo == null)
            {
                return HttpNotFound();
            }
            return View(comprimentoCabelo);
        }

        // GET: ComprimentoCabelo/Create
        public ActionResult Create()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var idUsuarioLogado = Convert.ToInt32(identity.Claims.FirstOrDefault(claim => claim.Type == "IdUsuario")?.Value);
            ViewBag.IdUsuario = idUsuarioLogado;
            return View();
        }

        // POST: ComprimentoCabelo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Comprimento,DataRegistro,IdUsuario,DataDoacaoCabelo")] ComprimentoCabelo comprimentoCabelo)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var idUsuarioLogado = Convert.ToInt32(identity.Claims.FirstOrDefault(claim => claim.Type == "IdUsuario")?.Value);

            if (ModelState.IsValid)
            {

                if (comprimentoCabelo.Comprimento < 15)
                {

                    // calculo simples com base na média de crescimento de cabelo: 1cm/mês
                    // não considera as datas anteriores, apenas a ultima registrada
                    comprimentoCabelo.DataDoacaoCabelo = comprimentoCabelo.DataRegistro.AddMonths(15 - Convert.ToInt32(comprimentoCabelo.Comprimento));
                    comprimentoCabelo.IdUsuario = idUsuarioLogado;
                    db.ComprimentoCabelo.Add(comprimentoCabelo);
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = idUsuarioLogado;
            return View(comprimentoCabelo);
        }

        // GET: ComprimentoCabelo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComprimentoCabelo comprimentoCabelo = db.ComprimentoCabelo.Find(id);
            if (comprimentoCabelo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nome", comprimentoCabelo.IdUsuario);
            return View(comprimentoCabelo);
        }

        // POST: ComprimentoCabelo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdComprimentoCabelo,Comprimento,DataRegistro,IdUsuario")] ComprimentoCabelo comprimentoCabelo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comprimentoCabelo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nome", comprimentoCabelo.IdUsuario);
            return View(comprimentoCabelo);
        }

        // GET: ComprimentoCabelo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComprimentoCabelo comprimentoCabelo = db.ComprimentoCabelo.Find(id);
            if (comprimentoCabelo == null)
            {
                return HttpNotFound();
            }
            return View(comprimentoCabelo);
        }

        // POST: ComprimentoCabelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComprimentoCabelo comprimentoCabelo = db.ComprimentoCabelo.Find(id);
            db.ComprimentoCabelo.Remove(comprimentoCabelo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
