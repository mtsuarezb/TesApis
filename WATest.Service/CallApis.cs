using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WATest.Entities;

namespace WATest.Service
{
    public class CallApis
    {
        private ApisHelpers _helper;

        public CallApis() => _helper = new ApisHelpers();
        public async Task<List<Prestamo>> GetAllLoans() 
        {
            List<Prestamo> data = new List<Prestamo>();
            string apiName = LoansApisNames.Get();
            var resp = await _helper.GetApi(apiName);
            if (resp.IsSuccessStatusCode)
            {
                var dataresp = resp.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<Prestamo>>(dataresp);
            }

            return data;
        }
        public async Task<List<Prestamo>> GetAllLoans(int idLector, int idLibro, DateTime? fdesde = null,
            DateTime? fhasta = null, DateTime? fDevolucion = null)
        {
            List<Prestamo> data = new List<Prestamo>();
            string apiName = LoansApisNames.GetByIds(idLector, idLibro, fdesde, fhasta, fDevolucion);
            var resp = await _helper.PostApi(apiName, "");
            if (resp.IsSuccessStatusCode)
            {
                var dataresp = resp.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<Prestamo>>(dataresp);
            }

            return data;
        }
        public async Task<List<Estudiante>> GetAllStudents(bool bTodos) 
        {
            List<Estudiante> data = new List<Estudiante>();
            string apiName = StudentsApisName.Get();
            var resp = await _helper.GetApi(apiName);
            if (resp.IsSuccessStatusCode)
            {
                var dataresp = resp.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<Estudiante>>(dataresp);
            }

            if (bTodos) {
                data.Add(new Estudiante { IdLector = 0, CI = "", Nombre = " * TODOS" });
            }

            return data;
        }
        public async Task<List<Estudiante>> GetAllStudents(string name, string ci)
        {
            List<Estudiante> data = new List<Estudiante>();
            string apiName = StudentsApisName.GetByNameOrCI(name, ci);
            var resp = await _helper.PostApi(apiName, "");
            if (resp.IsSuccessStatusCode)
            {
                var dataresp = resp.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<Estudiante>>(dataresp);
            }

            return data;
        }
        public async Task<List<Libro>> GetAllBooks(bool bTodos)
        {
            List<Libro> data = new List<Libro>();
            string apiName = BooksApiName.Get();
            var resp = await _helper.GetApi(apiName);
            if (resp.IsSuccessStatusCode)
            {
                var dataresp = resp.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<Libro>>(dataresp);
            }

            if (bTodos)
            {
                data.Add(new Libro { IdLibro = 0, Titulo = " * TODOS" });
            }

            return data;
        }
        public async Task<List<Libro>> GetAllBooks(string titulo)
        {
            List<Libro> data = new List<Libro>();
            string apiName = BooksApiName.GetByTitle(titulo);
            var resp = await _helper.GetApi(apiName);
            if (resp.IsSuccessStatusCode)
            {
                var dataresp = resp.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<Libro>>(dataresp);
            }

            return data;
        }
        public async Task<Prestamo> GetLoan(int? idLector, int? idLibro, DateTime? fechaPrestamo) 
        {
            Prestamo data = new Prestamo();
            string apiName = LoansApisNames.GetByIds(idLector, idLibro, fechaPrestamo);
            var resp = await _helper.PostApi(apiName, "");

            if (resp.IsSuccessStatusCode)
            {
                var dataresp = resp.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<Prestamo>(dataresp);
            }

            return data;
        }
        public async Task<Estudiante> GetStudent(int? id) 
        {
            Estudiante data = new Estudiante();
            string apiName = StudentsApisName.GetById(id);
            var Res = await _helper.PostApi(apiName, "");
            if (Res.IsSuccessStatusCode)
            {
                var dataresp = Res.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<Estudiante>(dataresp);
            }

            return data;
        }
        public async Task<Libro> GetBook(int? id)
        {
            Libro data = new Libro();
            string apiName = BooksApiName.GetById(id);
            var Res = await _helper.PostApi(apiName, "");
            if (Res.IsSuccessStatusCode)
            {
                var dataresp = Res.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<Libro>(dataresp);
            }

            return data;
        }

        public async Task<bool> AddOrUpdateLoans(Prestamo entity) 
        {
            string param = JsonConvert.SerializeObject(entity);
            string apiName = LoansApisNames.AddOrUpdate();
            var Res = await _helper.PostApi(apiName, param);
            if (Res.IsSuccessStatusCode)
            {
                var dataresp = Res.Content.ReadAsStringAsync().Result;
                return true;
            }

            return false;
        }
        public async Task<bool> AddOrUpdateStudents(Estudiante entity)
        {
            string param = JsonConvert.SerializeObject(entity);
            string apiName = StudentsApisName.AddOrUpdate();
            var Res = await _helper.PostApi(apiName, param);
            if (Res.IsSuccessStatusCode)
            {
                var dataresp = Res.Content.ReadAsStringAsync().Result;
                return true;
            }

            return false;
        }
        public async Task<bool> AddOrUpdateBooks(Libro entity)
        {
            var param = JsonConvert.SerializeObject(entity);
            var apiName = BooksApiName.AddOrUpdate();
            var Res = await _helper.PostApi(apiName, param);
            if (Res.IsSuccessStatusCode)
            {
                var dataresp = Res.Content.ReadAsStringAsync().Result;
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteLoans(int idLector, int idLibro, DateTime fechaPrestamo)
        {
            string apiName = LoansApisNames.Delete(idLector, idLibro, fechaPrestamo);
            var Res = await _helper.PostApi(apiName, "");
            if (Res.IsSuccessStatusCode)
            {
                var dataresp = Res.Content.ReadAsStringAsync().Result;
                return true;
            }

            return false;
        }
        public async Task<bool> DeleteStudent(int id)
        {
            string apiName = StudentsApisName.Delete(id);
            var Res = await _helper.PostApi(apiName, "");
            if (Res.IsSuccessStatusCode)
            {
                var dataresp = Res.Content.ReadAsStringAsync().Result;
                return true;
            }

            return false;
        }
        public async Task<bool> DeleteBook(int id)
        {
            string apiName = BooksApiName.Delete(id);
            var Res = await _helper.PostApi(apiName, "");
            if (Res.IsSuccessStatusCode)
            {
                var dataresp = Res.Content.ReadAsStringAsync().Result;
                return true;
            }

            return false;
        }
    } 
}
