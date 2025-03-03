using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PTemp_Lemus.Models;

namespace PTemp_Lemus.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly MyDbContext _context;

        // Constructor que inicializa MyDbContext
        public EmpleadoController()
        {
            _context = new MyDbContext();  // Inicializamos el contexto de la base de datos
        }
        [HttpGet]
        public ActionResult Login(string returnUrl = null)
        {
            return View();
        }

        // Acción para procesar el login
        [HttpPost]
        public ActionResult Login(string usuario, string clave, string returnUrl = null)
        {
            // Busca al empleado por usuario y que esté activo
            var empleado = _context.EmpleadoModel
                .FirstOrDefault(e => e.usuario == usuario && e.activo);  // Asegúrate de usar los nombres correctos

            // Verifica si el empleado existe y la clave coincide
            if (empleado != null && empleado.clave == clave)
            {
                // Aquí va el proceso de autenticación (FormsAuthentication, cookie, etc.)
                var ticket = new FormsAuthenticationTicket(
                    1,
                    empleado.usuario,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    empleado.idEmpleado.ToString()  // Aquí puedes guardar más información en el ticket si lo necesitas
                );

                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                {
                    Expires = ticket.Expiration,
                    Path = FormsAuthentication.FormsCookiePath
                };

                Response.Cookies.Add(cookie);  // Guardamos la cookie

                // Redirigir a la página solicitada o al home por defecto
                return Redirect(returnUrl ?? "/Reclamo/Index");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }
        }

        // Acción para cerrar sesión
        [HttpPost]
        public ActionResult Logout()
        {
            // Cerrar sesión del usuario
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }



        private MyDbContext db = new MyDbContext();

        // GET: Empleado
        [Authorize]
        public ActionResult Index()
        {
            return View(db.EmpleadoModel.ToList());
        }

        // GET: Empleado/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            var model = new EmpleadoModel();
            return View(model);
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpleadoModel empleadoModel = db.EmpleadoModel.Find(id);
            db.EmpleadoModel.Remove(empleadoModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
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
