using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WATest.Service
{
    public static class LoansApisNames
    {
        public static string Get()
        {
            return "api/LoansAPI/Get";
        }

        public static string GetByIds(int? idLector, int? idLibro, DateTime? fechaPrestamo) 
        {
          return  $"api/LoansAPI/GetByIds?idLector={idLector}&idLibro={idLibro}&fechaPrestamo={fechaPrestamo}";
        }

        public static string GetByIds(int? idLector, int? idLibro, DateTime? fdesde = null,
            DateTime? fhasta = null, DateTime? fDevolucion = null)
        {
            string api = $"api/LoansAPI/GetByIds?idLector={idLector}&idLibro={idLibro}";

            if (fdesde != null) {
                api = api + $"?fdesde={fdesde}";
            }

            if (fhasta != null)
            {
                api = api + $"?fhasta={fhasta}";
            }

            if (fDevolucion != null)
            {
                api = api + $"?fDevolucion={fDevolucion}";
            }

            return api;
        }

        public static string AddOrUpdate()
        {
            return "api/LoansAPI/AddUpdate";
        }

        public static string Delete(int? idLector, int? idLibro, DateTime? fechaPrestamo)
        {
            return $"api/LoansAPI/Delete?idLector={idLector}&idLibro={idLibro}&fechaPrestamo={fechaPrestamo}";
        }
    }
}
