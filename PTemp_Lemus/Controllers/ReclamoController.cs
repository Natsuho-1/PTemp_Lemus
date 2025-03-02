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
    public class ReclamoController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Reclamo
        public ActionResult Index()
        {
            return View(db.ReclamoModel.ToList());
        }

        // GET: Reclamo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReclamoModel reclamoModel = db.ReclamoModel.Find(id);
            if (reclamoModel == null)
            {
                return HttpNotFound();
            }
            return View(reclamoModel);
        }

        // GET: Reclamo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reclamo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idReclamo,nombreProveedor,direccionProveedor,detalleReclamo,telefonoProveedor,montoReclamo,fechaIngreso,fechaRevision,idEmpleado,idConsumidor,idEstado,activo")] ReclamoModel reclamoModel)
        {
            if (ModelState.IsValid)
            {
                db.ReclamoModel.Add(reclamoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reclamoModel);
        }

        // GET: Reclamo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReclamoModel reclamoModel = db.ReclamoModel.Find(id);
            if (reclamoModel == null)
            {
                return HttpNotFound();
            }
            return View(reclamoModel);
        }

        // POST: Reclamo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idReclamo,nombreProveedor,direccionProveedor,detalleReclamo,telefonoProveedor,montoReclamo,fechaIngreso,fechaRevision,idEmpleado,idConsumidor,idEstado,activo")] ReclamoModel reclamoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reclamoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reclamoModel);
        }

        // GET: Reclamo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReclamoModel reclamoModel = db.ReclamoModel.Find(id);
            if (reclamoModel == null)
            {
                return HttpNotFound();
            }
            return View(reclamoModel);
        }

        // POST: Reclamo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReclamoModel reclamoModel = db.ReclamoModel.Find(id);
            db.ReclamoModel.Remove(reclamoModel);
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
