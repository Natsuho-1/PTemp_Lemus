using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PTemp_Lemus.Models;

namespace PTemp_Lemus.Controllers
{
    [Authorize]
    public class ConsumidorController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Consumidor
        public ActionResult Index()
        {
            return View(db.ConsumidorModels.ToList());
        }

        // GET: Consumidor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumidorModel consumidorModel = db.ConsumidorModels.Find(id);
            if (consumidorModel == null)
            {
                return HttpNotFound();
            }
            return View(consumidorModel);
        }

        // GET: Consumidor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consumidor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idConsumidor,nombreConsumidor,apellidoConsumidor,direccion,correoElectronico,duiConsumidor,activo")] ConsumidorModel consumidorModel)
        {
            if (ModelState.IsValid)
            {
                db.ConsumidorModels.Add(consumidorModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consumidorModel);
        }

        // GET: Consumidor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumidorModel consumidorModel = db.ConsumidorModels.Find(id);
            if (consumidorModel == null)
            {
                return HttpNotFound();
            }
            return View(consumidorModel);
        }

        // POST: Consumidor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idConsumidor,nombreConsumidor,apellidoConsumidor,direccion,correoElectronico,duiConsumidor,activo")] ConsumidorModel consumidorModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumidorModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consumidorModel);
        }

        // GET: Consumidor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumidorModel consumidorModel = db.ConsumidorModels.Find(id);
            if (consumidorModel == null)
            {
                return HttpNotFound();
            }
            return View(consumidorModel);
        }

        // POST: Consumidor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConsumidorModel consumidorModel = db.ConsumidorModels.Find(id);
            db.ConsumidorModels.Remove(consumidorModel);
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
