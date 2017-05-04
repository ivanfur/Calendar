using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalendarApp.Web.Models;

namespace CalendarApp.Web.Controllers
{
    public class PrestadoresController : Controller
    {
        private CalendarManagerEntities db = new CalendarManagerEntities();

        // GET: Prestadores
        public ActionResult Index()
        {
            var prestadors = db.Prestadors.Include(p => p.Estado);
            return View(prestadors.ToList());
        }

        // GET: Prestadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestador prestador = db.Prestadors.Find(id);
            if (prestador == null)
            {
                return HttpNotFound();
            }
            return View(prestador);
        }

        // GET: Prestadores/Create
        public ActionResult Create()
        {
            ViewBag.EstadoPrestadorId = new SelectList(db.Estadoes, "Id", "Descripcion");
            return View();
        }

        // POST: Prestadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Apellido,Telefono,Telefono2,Direccion,Email,ObraSocial,NumeroSocio,Plan,CodigoSeguridad,Descripcion,CUIL,EstadoPrestadorId")] Prestador prestador)
        {
            if (ModelState.IsValid)
            {
                db.Prestadors.Add(prestador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstadoPrestadorId = new SelectList(db.Estadoes, "Id", "Descripcion", prestador.EstadoPrestadorId);
            return View(prestador);
        }

        // GET: Prestadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestador prestador = db.Prestadors.Find(id);
            if (prestador == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoPrestadorId = new SelectList(db.Estadoes, "Id", "Descripcion", prestador.EstadoPrestadorId);
            return View(prestador);
        }

        // POST: Prestadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,Telefono,Telefono2,Direccion,Email,ObraSocial,NumeroSocio,Plan,CodigoSeguridad,Descripcion,CUIL,EstadoPrestadorId")] Prestador prestador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstadoPrestadorId = new SelectList(db.Estadoes, "Id", "Descripcion", prestador.EstadoPrestadorId);
            return View(prestador);
        }

        // GET: Prestadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestador prestador = db.Prestadors.Find(id);
            if (prestador == null)
            {
                return HttpNotFound();
            }
            return View(prestador);
        }

        // POST: Prestadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestador prestador = db.Prestadors.Find(id);
            db.Prestadors.Remove(prestador);
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
