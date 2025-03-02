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
    public class EmpleadoController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Empleado
        public ActionResult Index()
        {
            return View(db.EmpleadoModel.ToList());
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpleadoModel empleadoModel = db.EmpleadoModel.Find(id);
            if (empleadoModel == null)
            {
                return HttpNotFound();
            }
            return View(empleadoModel);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpleado,nombres,apellidos,usuario,clave,activo")] EmpleadoModel empleadoModel)
        {
            if (ModelState.IsValid)
            {
                db.EmpleadoModel.Add(empleadoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleadoModel);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpleadoModel empleadoModel = db.EmpleadoModel.Find(id);
            if (empleadoModel == null)
            {
                return HttpNotFound();
            }
            return View(empleadoModel);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmpleado,nombres,apellidos,usuario,clave,activo")] EmpleadoModel empleadoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleadoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleadoModel);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpleadoModel empleadoModel = db.EmpleadoModel.Find(id);
            if (empleadoModel == null)
            {
                return HttpNotFound();
            }
            return View(empleadoModel);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpleadoModel empleadoModel = db.EmpleadoModel.Find(id);
            db.EmpleadoModel.Remove(empleadoModel);
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
