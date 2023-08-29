using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Escuelas_ASP.Models
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Codigo requerido")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre_Curso { get; set; }
        [Required(ErrorMessage ="Descripcion requerida")]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        [Required(ErrorMessage = "Nivel requerido")]
        public string Nivel { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public virtual IEnumerable<Estudiante>? Estudiantes { get; set; }
        public virtual IEnumerable<CursoMateria>? CursoMaterias { get; set; }
    }
}
