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
    public class TurnosController : Controller
    {
        private CalendarManagerEntities db = new CalendarManagerEntities();

        // GET: Turnos
        public ActionResult Index()
        {
            var turnos = db.Turnos.Include(t => t.Paciente).Include(t => t.Prestador);
            return View(turnos.ToList());
        }

        // GET: Turnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turnos.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // GET: Turnos/Create
        public ActionResult Create()
        {
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre");
            ViewBag.IdPrestador = new SelectList(db.Prestadors, "Id", "Nombre");
            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,NumeroSesion,IdPaciente,IdPrestador,Monto")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                db.Turnos.Add(turno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", turno.IdPaciente);
            ViewBag.IdPrestador = new SelectList(db.Prestadors, "Id", "Nombre", turno.IdPrestador);
            return View(turno);
        }

        // GET: Turnos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turnos.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", turno.IdPaciente);
            ViewBag.IdPrestador = new SelectList(db.Prestadors, "Id", "Nombre", turno.IdPrestador);
            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,NumeroSesion,IdPaciente,IdPrestador,Monto")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", turno.IdPaciente);
            ViewBag.IdPrestador = new SelectList(db.Prestadors, "Id", "Nombre", turno.IdPrestador);
            return View(turno);
        }

        // GET: Turnos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turnos.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turno turno = db.Turnos.Find(id);
            db.Turnos.Remove(turno);
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
