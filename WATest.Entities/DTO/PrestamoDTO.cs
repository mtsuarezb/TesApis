using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WATest.Entities.DTO
{
    public class PrestamoDTO
    {
        [Key]
        [Column(Order = 0)]
        public int IdLector { get; set; }

        [Key]
        [Column(Order = 1)]
        public int IdLibro { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime FechaPrestamo { get; set; }

        public DateTime? FechaDevolucion { get; set; }

        public bool? Devuelvo { get; set; }

        public EstudianteDTO Estudiante = new EstudianteDTO();

        public LibroDTO Libro = new LibroDTO();
    }
}
