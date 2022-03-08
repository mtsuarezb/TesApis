using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WATest.Entities;
using WATest.Entities.DTO;
using WATest.Business.Catalogs;


namespace WATest.APIS.Controllers
{
    public class BooksAPIController : ApiController
    {
        [Route("api/BooksAPI/Get")]
        [HttpGet]
        public IEnumerable<LibroDTO> GetAll()
        {
            return new Books().ListAll();
        }

        [Route("api/BooksAPI/GetByTitle")]
        [HttpGet]
        public List<LibroDTO> GetByTitle(string titulo)
        {
            return new Books().ListByNameOrId(0, titulo);
        }

        [Route("api/BooksAPI/GetById")]
        [HttpPost]
        public LibroDTO GetById(int id)
        {
            return new Books().ListByNameOrId(id, "").FirstOrDefault();
        }

        [Route("api/BooksAPI/AddUpdate")]
        [HttpPost]
        public IHttpActionResult AddUpdate(Libro data)
        {
            return Ok(new Books().AddEdit(data));
        }

        [Route("api/BooksAPI/Delete")]
        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            return Ok(new Books().DeleteById(id));
        }
    }
}
