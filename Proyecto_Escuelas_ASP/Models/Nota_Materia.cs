using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Escuelas_ASP.Models
{
    public class Nota_Materia
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Estudiantes")]
        public int EstudianteId { get; set; }
        [ForeignKey("CursoMateria")]
        public int CursoMateriaId { get; set; }
        public DateTime FechaCalificacion { get; set; }
        public double NotaObtenida { get; set; }
        public bool Estado { get; set; }
        public virtual Estudiante Estudiantes { get; set; }
        public virtual CursoMateria CursoMateria { get; set; }
    }
}
