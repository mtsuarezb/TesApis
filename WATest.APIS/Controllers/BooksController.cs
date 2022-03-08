using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WATest.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using WATest.Service; 

namespace WATest.APIS.Controllers
{
    public class BooksController : Controller
    {
        private CallApis _calls;

        public BooksController() => _calls = new CallApis();


        public async Task<ActionResult> Index()
        {
            List<Libro> data = await _calls.GetAllBooks(false);
            return View(data);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Libro data = await _calls.GetBook(id);
           
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
        public async Task<ActionResult> Create([Bind(Include = "IdLibro,Titulo,Editorial,Area")] Libro libro)
        {
            bool valido = true;
            var data = await _calls.GetAllBooks(libro.Titulo);

            if (data.Count > 0) 
            {
                ViewBag.Mensaje = "Ya existe un libro con ese mismo titulo";
                valido = false;
            }

            if (ModelState.IsValid && valido)
            {

                var resp = await _calls.AddOrUpdateBooks(libro);
                return RedirectToAction("Index");
            }

            return View(libro);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Libro data = await _calls.GetBook(id);

            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdLibro,Titulo,Editorial,Area")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                var resp = await _calls.AddOrUpdateBooks(libro);
                return RedirectToAction("Index");
            }

            return View(libro);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Libro data = await _calls.GetBook(id);

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
            var resp = await _calls.DeleteBook(id);
            return RedirectToAction("Index");
        }

    }
}
