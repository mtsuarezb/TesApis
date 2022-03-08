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
    public class StudentsAPIController : ApiController
    {
        [Route("api/StudentsAPI/Get")]
        [HttpGet]
        public IEnumerable<EstudianteDTO> GetAll()
        {
            return new Students().ListAll();
        }

        [Route("api/StudentsAPI/GetById")]
        [HttpPost]
        public EstudianteDTO GetById(int id)
        {
            return new Students().ListByNameOrId(id, "", "").FirstOrDefault();
        }

        [Route("api/StudentsAPI/GetByNameOrCI")]
        [HttpPost]
        public List<EstudianteDTO> GetByNameOrCI(string nombre, string ci)
        {
            nombre = nombre == null ? "" : nombre;
            return new Students().ListByNameOrId(0, nombre, ci);
        }

        [Route("api/StudentsAPI/AddUpdate")]
        [HttpPost]
        public IHttpActionResult AddUpdate(Estudiante data)
        {
            return Ok(new Students().AddEdit(data));
        }

        [Route("api/StudentsAPI/Delete")]
        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
           return Ok(new Students().DeleteById(id));
        }
    }
}
