using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WATest.Data;
using WATest.Entities;
using WATest.Entities.DTO;
using System.Linq.Expressions;
using WATest.Business.Resources;

namespace WATest.Business.Catalogs
{
    public class LoansBooks
    {
        private EFDataTest _db;
       public LoansBooks() => _db = new EFDataTest();

        public List<PrestamoDTO> ListAll()
        {
            var data = _db.Prestamo.ToList();
            List<PrestamoDTO> pDTO = new List<PrestamoDTO>();

            data.ForEach(p =>
            {
                var pres = new PrestamoDTO();
                pres.IdLector = p.IdLector;
                pres.IdLibro = p.IdLibro;
                pres.FechaPrestamo = p.FechaPrestamo;
                pres.FechaDevolucion = p.FechaDevolucion;
                pres.Devuelvo = p.Devuelvo;
                pres.Estudiante.IdLector = p.Estudiante.IdLector;
                pres.Estudiante.Nombre = p.Estudiante.Nombre;
                pres.Libro.IdLibro = p.Libro.IdLibro;
                pres.Libro.Titulo = p.Libro.Titulo;

                pDTO.Add(pres);
            });

            return pDTO;
        }

        public Prestamo ListByNameOrId(int idLector, int idLibro, DateTime? fromLoans = null)
        {
            var predicate = PredicateBuilder.True<Prestamo>();

            predicate = e => e.IdLector == idLector;

            if (idLector != 0)
            {
                predicate = e => e.IdLector == idLector;
            }

            if (idLibro != 0)
            {
                predicate = predicate.And(e => e.IdLibro == idLibro);
            }

            if (fromLoans != null)
            {
                predicate = predicate.And(e => e.FechaPrestamo == fromLoans);
            }

            return _db.Prestamo.Where(predicate).FirstOrDefault();

        }


        public List<Prestamo> ListByNameOrId(int idLector, int idLibro, DateTime? fromLoans = null, 
            DateTime? toLoans = null, DateTime? devolutionDate = null, int bReturned = -1 )
        {
            var predicate = PredicateBuilder.True<Prestamo>();

            predicate = e => (e.IdLector == idLector || idLector == 0) ;

            if (idLector != 0)
            {
                predicate = e => e.IdLector == idLector;
            }
            
            if (idLibro != 0)
            {
                predicate = predicate.And(e => e.IdLibro == idLibro);
            }
            
            if (fromLoans != null && toLoans != null)
            {
                predicate = predicate.And(e => e.FechaPrestamo >= fromLoans && e.FechaPrestamo <= toLoans);
            }
            
            if (devolutionDate != null)
            {
                predicate = predicate.And(e => e.FechaDevolucion == devolutionDate);
            }
            
            if (bReturned == 1 || bReturned == 0) 
            {
                var _breturned = bReturned == 1 ? true : false;
                predicate = predicate.And(e => e.Devuelvo == _breturned);
            }

            return _db.Prestamo.Where(predicate).ToList();

        }

        public bool AddEdit(Prestamo en)
        {

            if (!_db.Prestamo.Where(p => p.IdLibro == en.IdLibro && p.IdLector == en.IdLector && p.FechaPrestamo == en.FechaPrestamo).Any())
            {
                en.Devuelvo = en.Devuelvo == null ? false : en.Devuelvo;
                _db.Prestamo.Add(en);
            }
            else
            {
                var es = _db.Prestamo.Where(p => p.IdLibro == en.IdLibro && p.IdLector == en.IdLector && p.FechaPrestamo == en.FechaPrestamo).FirstOrDefault();
                _db.Prestamo.Attach(es);
                es.Devuelvo = en.FechaDevolucion != null && (en.Devuelvo == null || en.Devuelvo == false) ? true : en.Devuelvo;
                es.FechaDevolucion = en.FechaDevolucion;
            }

            _db.SaveChanges();

            return true;

        }

        public bool DeleteById(Prestamo en)
        {
            _db.Prestamo.Remove(en);
            _db.SaveChanges();

            return true;
        }
    }
}
