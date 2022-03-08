using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WATest.Service
{
    public static class BooksApiName
    {
        public static string Get() 
        {
            return "api/BooksAPI/Get";
        }

        public static string GetByTitle(string titulo)
        {
            return $"api/BooksAPI/GetByTitle?titulo={titulo}";
        }

        public static string GetById(int? id)
        {
            return $"api/BooksAPI/GetById?id={id}";
        }

        public static string AddOrUpdate() 
        {
            return "api/BooksAPI/AddUpdate";
        }

        public static string Delete(int? id) 
        {
            return $"api/BooksAPI/Delete?id={id}";
        }
    }
}
