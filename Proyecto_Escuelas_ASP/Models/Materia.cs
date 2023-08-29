using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Escuelas_ASP.Models
{
    public class Materia
    {
        [Key]
        public string Nombre_Materia { get; set; }
        [Required(ErrorMessage = "Descripcion requerida")]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
        [ForeignKey("Profesores")]
        public int ProfesorId { get; set; }
        public virtual Profesor Profesores { get; set; }
        public virtual IEnumerable<Nota_Materia> Nota_Materias { get; set; }
    }
}
