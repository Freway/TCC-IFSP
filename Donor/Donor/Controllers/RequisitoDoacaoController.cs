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
    public class RequisitoDoacaoController : Controller
    {
        private OrmDonor db = new OrmDonor();

        // GET: RequisitoDoacao
        public ActionResult Index()
        {
            ViewBag.ListaPontos = db.PontoDeDoacao.Where(doacao => doacao.IdPontoDeDoacao != -1 ).OrderBy(doacao2 => doacao2.Nome).ToList();
            var requisitoDoacao = db.RequisitoDoacao.Include(r => r.PontoDeDoacao).Where(doacao3 => doacao3.IdPontoDoacao == -1);
            return View(requisitoDoacao.ToList());
        }

        // GET: RequisitoDoacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitoDoacao requisitoDoacao = db.RequisitoDoacao.Find(id);
            if (requisitoDoacao == null)
            {
                return HttpNotFound();
            }
            return View(requisitoDoacao);
        }

        // GET: RequisitoDoacao/Create
        public ActionResult Create()
        {
            ViewBag.IdPontoDoacao = new SelectList(db.PontoDeDoacao, "IdPontoDeDoacao", "Nome");
            return View();
        }

        // POST: RequisitoDoacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRequisitoDoacao,Descricao,IdPontoDoacao")] RequisitoDoacao requisitoDoacao)
        {
            if (ModelState.IsValid)
            {
                db.RequisitoDoacao.Add(requisitoDoacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPontoDoacao = new SelectList(db.PontoDeDoacao, "IdPontoDeDoacao", "Nome", requisitoDoacao.IdPontoDoacao);
            return View(requisitoDoacao);
        }

        // GET: RequisitoDoacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitoDoacao requisitoDoacao = db.RequisitoDoacao.Find(id);
            if (requisitoDoacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPontoDoacao = new SelectList(db.PontoDeDoacao, "IdPontoDeDoacao", "Nome", requisitoDoacao.IdPontoDoacao);
            return View(requisitoDoacao);
        }

        // POST: RequisitoDoacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRequisitoDoacao,Descricao,IdPontoDoacao")] RequisitoDoacao requisitoDoacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisitoDoacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPontoDoacao = new SelectList(db.PontoDeDoacao, "IdPontoDeDoacao", "Nome", requisitoDoacao.IdPontoDoacao);
            return View(requisitoDoacao);
        }

        // GET: RequisitoDoacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitoDoacao requisitoDoacao = db.RequisitoDoacao.Find(id);
            if (requisitoDoacao == null)
            {
                return HttpNotFound();
            }
            return View(requisitoDoacao);
        }

        // POST: RequisitoDoacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequisitoDoacao requisitoDoacao = db.RequisitoDoacao.Find(id);
            db.RequisitoDoacao.Remove(requisitoDoacao);
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
