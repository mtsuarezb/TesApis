using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using WATest.Entities;
using WATest.Entities.DTO;
using Newtonsoft.Json;
using WATest.Service;
using System.Net.Http;
using System.Linq;

namespace WATest.APIS.Controllers
{
    public class LoansController : Controller
    {
        WATest.Service.CallApis _calls;

        public LoansController() => _calls = new CallApis();

        public async Task<ActionResult> Index()
        {
            List<Prestamo> data = await _calls.GetAllLoans();
            List<Estudiante> dataest = await _calls.GetAllStudents(true);
            List<Libro> datalib = await _calls.GetAllBooks(true);

            dataest = dataest.OrderBy(e => e.IdLector).ToList();
            datalib = datalib.OrderBy(e => e.IdLibro).ToList();

            ViewBag.IdEstudiantes = new SelectList(dataest, "IdLector", "Nombre");
            ViewBag.IdLibros = new SelectList(datalib, "IdLibro", "Titulo");
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> Index(FormCollection formCollection)
        {
            int idLector = Convert.ToInt32(Request.Form["IdEstudiantes"]);
            int idLibro = Convert.ToInt32(Request.Form["IdLibros"]);
            DateTime? fdesde = null, fhasta = null, fDevolucion = null;


            if (Request.Form["fdesde"].Length > 0) { fdesde = Convert.ToDateTime(Request.Form["fdesde"]); }
            if (Request.Form["fhasta"].Length > 0) { fhasta = Convert.ToDateTime(Request.Form["fhasta"]); }
            if (Request.Form["fDevolucion"].Length > 0) { fDevolucion = Convert.ToDateTime(Request.Form["fDevolucion"]); }


            List<Prestamo> data = await _calls.GetAllLoans(idLector, idLibro, fdesde, fhasta, fDevolucion);

            List<Estudiante> dataest = await _calls.GetAllStudents(true);
            List<Libro> datalib = await _calls.GetAllBooks(true);

            dataest = dataest.OrderBy(e => e.IdLector).ToList();
            datalib = datalib.OrderBy(e => e.IdLibro).ToList();

            if (idLector != 0)
            {
                ViewBag.IdEstudiantes = new SelectList(dataest, "IdLector", "Nombre", idLector);
            }
            else
            {
                ViewBag.IdEstudiantes = new SelectList(dataest, "IdLector", "Nombre");

            }

            if (idLibro != 0)
            {
                ViewBag.IdLibros = new SelectList(datalib, "IdLibro", "Titulo", idLibro);
            }
            else
            {
                ViewBag.IdLibros = new SelectList(datalib, "IdLibro", "Titulo");
            }
            return View(data);
        }

        public async Task<ActionResult> Details(int? idLector, int? idLibro, DateTime? fechaPrestamo)
        {
            if (idLector == null || idLibro == null || fechaPrestamo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Prestamo data = await _calls.GetLoan(idLector, idLibro, fechaPrestamo);

            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        public async Task<ActionResult> Create()
        {
            List<Estudiante> dataest = await _calls.GetAllStudents(false);
            List<Libro> datalib = await _calls.GetAllBooks(false);

            ViewBag.IdLector = new SelectList(dataest, "IdLector", "Nombre");
            ViewBag.IdLibro = new SelectList(datalib, "IdLibro", "Titulo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdLector,IdLibro,FechaPrestamo,FechaDevolucion,Devuelvo")] Prestamo prestamo)
        {
            bool Valida = true;

            if (ModelState.IsValid)
            {
                Valida = true;
            }

            //Realizamos ciertas validaciones en el proceso de crear
            ViewBag.Mensaje = "";
            if (prestamo.FechaPrestamo < DateTime.Now.Date)
            {
                Valida = false;
                ViewBag.Mensaje = "La fecha de prestamo no puede ser menor a la fecha actual";
            }

            if (prestamo.FechaPrestamo != null && prestamo.FechaDevolucion != null)
            {
                Valida = false;
                ViewBag.Mensaje = ViewBag.Mensaje + "<br />" + "No se puede prestar y devolver el libro el mismo día";
            }

            List<Prestamo> prestamos = await _calls.GetAllLoans(prestamo.IdLector, prestamo.IdLibro);
           
            if (prestamos.Where(p => p.FechaDevolucion == null).Any())
            {
                Valida = false;
                ViewBag.Mensaje = ViewBag.Mensaje + "<br />" + "Este mismo libro ya fue prestado por el estudiante y aún no ha sido devuelto. ";

            }

            if (Valida)
            {
                var res = await _calls.AddOrUpdateLoans(prestamo);
                return RedirectToAction("Index");
            }

            List<Estudiante> dataest = await _calls.GetAllStudents(false);
            List<Libro> datalib = await _calls.GetAllBooks(false);

            ViewBag.IdLector = new SelectList(dataest, "IdLector", "Nombre", prestamo.IdLector);
            ViewBag.IdLibro = new SelectList(datalib, "IdLibro", "Titulo", prestamo.IdLibro);
            return View(prestamo);
        }

        public async Task<ActionResult> Edit(int? idLector, int? idLibro, DateTime? fechaPrestamo)
        {
            if (idLector == null || idLibro == null || fechaPrestamo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Prestamo data = await _calls.GetLoan(idLector, idLibro, fechaPrestamo);

            if (data == null)
            {
                return HttpNotFound();
            }

            List<Estudiante> dataest = await _calls.GetAllStudents(false);
            List<Libro> datalib = await _calls.GetAllBooks(false);

            ViewBag.IdLector = new SelectList(dataest, "IdLector", "Nombre", data.IdLector);
            ViewBag.IdLibro = new SelectList(datalib, "IdLibro", "Titulo", data.IdLibro);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdLector,IdLibro,FechaPrestamo,FechaDevolucion,Devuelvo")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                var res = await _calls.AddOrUpdateLoans(prestamo);
                return RedirectToAction("Index");

            }

            List<Estudiante> dataest = await _calls.GetAllStudents(false);
            List<Libro> datalib = await _calls.GetAllBooks(false);

            ViewBag.IdLector = new SelectList(dataest, "IdLector", "Nombre", prestamo.IdLector);
            ViewBag.IdLibro = new SelectList(datalib, "IdLibro", "Titulo", prestamo.IdLibro);

            return View(prestamo);
        }

        public async Task<ActionResult> Delete(int? idLector, int? idLibro, DateTime? fechaPrestamo)
        {
            if (idLector == null || idLibro == null || fechaPrestamo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Prestamo data = await _calls.GetLoan(idLector, idLibro, fechaPrestamo);

            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int idLector, int idLibro, DateTime fechaPrestamo)
        {
            var resp = await _calls.DeleteLoans(idLector, idLibro, fechaPrestamo);
            return RedirectToAction("Index");

        }


    }
}
