using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Donor.Models;

namespace Donor.Controllers
{
    public class DoacaoController : Controller
    {
        private OrmDonor db = new OrmDonor();

        // GET: Doacao
        public ActionResult Index()
        {
            var doacao = db.Doacao.Include(d => d.TipoDoacao).Include(d => d.Usuario);
            return View(doacao.ToList());
        }

        // GET: Doacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doacao doacao = db.Doacao.Find(id);
            if (doacao == null)
            {
                return HttpNotFound();
            }
            return View(doacao);
        }

        // GET: Doacao/Create
        public ActionResult Create()
        {
            ViewBag.idTipoDoacao = new SelectList(db.TipoDoacao, "IdTipoDoacao", "TipoDoacao1");
            ViewBag.IdPontoDeDoacao = new SelectList(db.PontoDeDoacao, "IdPontoDeDoacao", "Nome");
            return View();
        }

        // POST: Doacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDoacao,Data,IdPontoDeDoacao,idTipoDoacao")] Doacao doacao)
        {
            if (ModelState.IsValid)
            {
                db.Doacao.Add(doacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTipoDoacao = new SelectList(db.TipoDoacao, "IdTipoDoacao", "TipoDoacao1", doacao.IdTipoDoacao);
            ViewBag.IdPontoDoacao = new SelectList(db.PontoDeDoacao, "IdPontoDeDoacao", "Nome", doacao.IdPontoDoacao);
            return View(doacao);
        }

       // GET: Doacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doacao doacao = db.Doacao.Find(id);
            if (doacao == null)
            {
                return HttpNotFound();
            }
            return View(doacao);
        }

        // POST: Doacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doacao doacao = db.Doacao.Find(id);
            db.Doacao.Remove(doacao);
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
