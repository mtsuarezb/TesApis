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
    public class LoansAPISController : ApiController
    {
        [Route("api/LoansAPI/Get")]
        [HttpGet]
        public IEnumerable<PrestamoDTO> GetAll()
        {
            var data = new LoansBooks().ListAll();

            return data;
        }

        [Route("api/LoansAPI/GetByIds")]
        [HttpPost]
        public PrestamoDTO GetByIds(int idLector, int idLibro, DateTime fechaPrestamo)
        {
            var data = new LoansBooks().ListByNameOrId(idLector, idLibro, fechaPrestamo);
            var pDTO =  new PrestamoDTO
                {
                    IdLector = data.IdLector,
                    IdLibro = data.IdLibro,
                    FechaPrestamo = data.FechaPrestamo,
                    FechaDevolucion = data.FechaDevolucion,
                    Devuelvo = data.Devuelvo,
                    Estudiante = new EstudianteDTO { IdLector = data.Estudiante.IdLector, Nombre = data.Estudiante.Nombre  },
                    Libro = new LibroDTO { IdLibro = data.Libro.IdLibro, Titulo = data.Libro.Titulo }
                };

            return pDTO;
        }

        [Route("api/LoansAPI/GetByIds")]
        [HttpPost]
        public List<PrestamoDTO> GetByIds(int idLector, int idLibro, DateTime? fdesde = null, 
            DateTime? fhasta = null, DateTime? fDevolucion = null)
        {
            var data = new LoansBooks().ListByNameOrId(idLector, idLibro, fdesde , fhasta, fDevolucion)
                .Select(p => 
                new PrestamoDTO
            {
                IdLector = p.IdLector,
                IdLibro = p.IdLibro,
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucion = p.FechaDevolucion,
                Devuelvo = p.Devuelvo,
                Estudiante = new EstudianteDTO { IdLector = p.Estudiante.IdLector, Nombre = p.Estudiante.Nombre },
                Libro = new LibroDTO { IdLibro = p.Libro.IdLibro, Titulo = p.Libro.Titulo }
            }).ToList();

            return data;
        }


        [Route("api/LoansAPI/AddUpdate")]
        [HttpPost]
        public IHttpActionResult AddUpdate(Prestamo data)
        {
            return Ok(new LoansBooks().AddEdit(data));
        }

        [Route("api/LoansAPI/Delete")]
        [HttpPost]
        public IHttpActionResult Delete(int idLector, int idLibro, DateTime fechaPrestamo)
        {
            var obj = new LoansBooks();
            var entity = obj.ListByNameOrId(idLector, idLibro, fechaPrestamo);

            return Ok(obj.DeleteById(entity));
        }
    }
}
