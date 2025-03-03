using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PTemp_Lemus.Models;
using PagedList;

namespace PTemp_Lemus.Controllers
{
    [Authorize]
    public class ReclamoController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Reclamo
        public ActionResult Index( int? page)
        {
            var pageNumber = page ?? 1; // Si no hay número de página, inicia en la página 1.
            var pageSize = 10; // Número de elementos por página.
            var reclamos = db.ReclamoModel.ToList(); // Reemplaza esto con tu consulta.
                                                     // Paginar la lista de reclamos
            foreach (var reclamo in reclamos)
            {
                var estado = db.estadoModels.FirstOrDefault(e => e.idEstado == reclamo.idEstado);
                if (estado != null)
                {
                    reclamo.nombreEstado = estado.nombreEstado; // Asumiendo que la tabla c_Estado tiene una columna descripcionEstado
                }
            }
            var pagedReclamos = reclamos.ToPagedList(pageNumber, pageSize);
            return View(pagedReclamos);
            //return View(db.ReclamoModel.ToList());
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
            var model = new ReclamoConsumidor
            {
                Reclamo = new ReclamoModel(),
                Consumidor = new ConsumidorModel()
            };

            return View(model);
        }

        // POST: Reclamo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReclamoConsumidor model)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    //try
                    //{
                        // Guardar los modelos de Reclamo y Consumidor
                        model.Consumidor.direccion = "No";
                        model.Consumidor.activo = true;
                        db.ConsumidorModels.Add(model.Consumidor);
                        model.Reclamo.idConsumidor = model.Consumidor.idConsumidor;
                        model.Reclamo.idEmpleado = 1;
                        model.Reclamo.activo = true;
                        model.Reclamo.fechaIngreso = DateTime.Now;
                        model.Reclamo.idEstado = 1;
                        db.ReclamoModel.Add(model.Reclamo);
                        db.SaveChanges();
                        transaction.Commit();

                        return RedirectToAction("Index");
                    //}
                    //catch (Exception ex)
                    //{
                    //    // Si ocurre un error, revertir la transacción
                    //    transaction.Rollback();
                    //    var errorMessage = "Ocurrió un error al guardar los datos: " + ex.Message;
                    //    var innerExceptionMessage = ex.InnerException?.Message ?? "No hay detalles de InnerException";
                    //    var stackTrace = ex.StackTrace;

                    //    // Mostrar en la consola o en los logs de la aplicación
                    //    Console.WriteLine("Mensaje de Error: " + errorMessage);
                    //    Console.WriteLine("Mensaje de InnerException: " + innerExceptionMessage);
                    //    Console.WriteLine("StackTrace: " + stackTrace);

                    //    // Agregar el error al ModelState para mostrarlo en la vista
                    //    ModelState.AddModelError("", errorMessage);
                    //    ModelState.AddModelError("", "Detalles del error: " + innerExceptionMessage);
                    //    ModelState.AddModelError("", "StackTrace: " + stackTrace);
                    ////}
                }
            }
            return View(model);
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
            var estados = db.estadoModels.ToList();
            ViewBag.Estados = new SelectList(estados, "idEstado", "nombreEstado", reclamoModel.idEstado);
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
                reclamoModel.fechaIngreso = DateTime.Now;
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
