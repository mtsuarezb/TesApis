using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WATest.Entities;
using WATest.Service;


namespace WATest.APIS.Controllers
{
    public class StudentsController : Controller
    {
        private CallApis _calls;

        public StudentsController() => _calls = new CallApis();

        public async Task<ActionResult> Index()
        {
            List<Estudiante> data = await _calls.GetAllStudents(false);
            return View(data);
       }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Estudiante data = await _calls.GetStudent(id);

            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdLector,CI,Nombre,Direccion,Carrera,Edad")] Estudiante estudiante)
        {
            bool valido = true;
            var data = await _calls.GetAllStudents("", estudiante.CI);

            if (data.Count > 0)
            {
                ViewBag.Mensaje = "Ya existe un estudiante con ese mismo CI ";
                valido = false;
            }
            if (ModelState.IsValid && valido)
            {
                var resp = await _calls.AddOrUpdateStudents(estudiante);
                return RedirectToAction("Index");
            }

            return View(estudiante);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Estudiante data = await _calls.GetStudent(id);

            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdLector,CI,Nombre,Direccion,Carrera,Edad")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                var resp = await _calls.AddOrUpdateStudents(estudiante);
                return RedirectToAction("Index"); 
            }

            return View(estudiante);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Estudiante data = await _calls.GetStudent(id);

            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var resp = await _calls.DeleteStudent(id);
            return RedirectToAction("Index");
        }

    }
}
