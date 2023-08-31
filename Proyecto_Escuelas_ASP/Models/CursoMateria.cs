using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Escuelas_ASP.Models
{
    public class CursoMateria
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Cursos")]
        public int CursoId { get; set; }
        [ForeignKey("Materias")]
        public string MateriaId { get; set; }
        public bool Estado { get; set; }
        public virtual Curso Cursos { get; set; }
        public virtual Materia Materias { get; set; }
        public virtual IEnumerable<Nota_Materia> Nota_Materias { get; set; }
    }
}
