using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WATest.Service
{
    public static class StudentsApisName
    {
        public static string Get() 
        {
            return "api/StudentsAPI/Get";
        }

        public static string GetById(int? id)
        {
            return $"api/StudentsAPI/GetById?id={id}";
        }

        public static string GetByNameOrCI(string nombre, string ci)
        {
            return $"api/StudentsAPI/GetByNameOrCI?nombre={nombre}&ci={ci}";
        }

        public static string AddOrUpdate()
        {
            return "api/StudentsAPI/AddUpdate";
        }

        public static string Delete(int? id) 
        {
            return $"api/StudentsAPI/Delete?id={id}";
        }
    }
}
