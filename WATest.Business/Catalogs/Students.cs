using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WATest.Data;
using WATest.Entities;
using WATest.Entities.DTO;

namespace WATest.Business.Catalogs
{
    public class Students
    {
        private EFDataTest _db;
        
        public Students() => _db = new EFDataTest();

        public List<EstudianteDTO> ListAll()
        {
           return _db.Estudiante.Select(e => 
           new EstudianteDTO 
           { 
              CI = e.CI,
              IdLector = e.IdLector,
              Carrera = e.Carrera,
              Nombre = e.Nombre,
              Direccion = e.Direccion,
              Edad = e.Edad
           }).ToList();
        }

        public List<EstudianteDTO> ListByNameOrId(int id, string name, string ci) {
            Expression<Func<Estudiante, bool>> predicate = e => e.IdLector == id;

            if (id != 0)
            {
                predicate = e => e.IdLector == id;
            }
            else if (name.Length > 0)
            {
                predicate = e => e.Nombre.Contains(name);
            }
            else if (ci.Length > 0)
            {
                predicate = e => e.CI.Contains(ci);
            }

            return _db.Estudiante.Where(predicate).Select(e =>
           new EstudianteDTO
           {
               CI = e.CI,
               IdLector = e.IdLector,
               Carrera = e.Carrera,
               Nombre = e.Nombre,
               Direccion = e.Direccion,
               Edad = e.Edad
           }).ToList();

        }

        public bool AddEdit(Estudiante en) {

            if (en.IdLector == 0)
            {
                var maxId = _db.Estudiante.Select(e => e.IdLector).DefaultIfEmpty().Max();

                en.IdLector = maxId + 1;
                _db.Estudiante.Add(en);
            }
            else {
                var es = _db.Estudiante.Where(e => e.IdLector == en.IdLector).FirstOrDefault();
                _db.Estudiante.Attach(es);
                es.Nombre = en.Nombre;
                es.CI = en.CI;
                es.Direccion = en.Direccion;
                es.Carrera = en.Carrera;
                es.Edad = en.Edad;
            }

            _db.SaveChanges();

            return true;
            
        }

        public bool DeleteById(int id)
        {
            var en = _db.Estudiante.Where(e => e.IdLector == id).FirstOrDefault();
            _db.Estudiante.Remove(en);
            _db.SaveChanges();

            return true;
        }
    }
}
