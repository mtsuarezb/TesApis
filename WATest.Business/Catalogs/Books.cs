using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WATest.Data;
using WATest.Entities;
using WATest.Entities.DTO;
using System.Linq.Expressions;

namespace WATest.Business.Catalogs
{
   public  class Books
    {
        private EFDataTest _db;
        public Books() => _db = new EFDataTest();

        public List<LibroDTO> ListAll()
        {
            return _db.Libro.Select(b => new LibroDTO 
            { 
                IdLibro = b.IdLibro,
                Titulo = b.Titulo,
                Area = b.Area,
                Editorial = b.Editorial
            }).ToList();
        }

        public List<LibroDTO> ListByNameOrId(int id, string name)
        {
            Expression<Func<Libro, bool>> predicate;

            if (id != 0)
            {
                predicate = e => e.IdLibro == id;
            }
            else
            {
                predicate = e => e.Titulo.Contains(name);
            }

            return _db.Libro.Where(predicate).Select(b => 
            new LibroDTO
            {
                IdLibro = b.IdLibro,
                Titulo = b.Titulo,
                Area = b.Area,
                Editorial = b.Editorial
            }).ToList();

        }

        public bool AddEdit(Libro en)
        {

            if (en.IdLibro == 0)
            {
                var maxId = _db.Libro.Select(e => e.IdLibro).DefaultIfEmpty().Max();

                en.IdLibro = maxId + 1;
                _db.Libro.Add(en);
            }
            else
            {
                var es = _db.Libro.Where(e => e.IdLibro == en.IdLibro).FirstOrDefault();
                _db.Libro.Attach(es);
                es.Editorial = en.Editorial;
                es.Area = en.Area;
                es.Titulo = en.Titulo;
            }

            _db.SaveChanges();

            return true;

        }

        public bool DeleteById(int id)
        {
            var en = _db.Libro.Where(l => l.IdLibro == id).FirstOrDefault();
            _db.Libro.Remove(en);
            _db.SaveChanges();

            return true;
        }
    }
}
