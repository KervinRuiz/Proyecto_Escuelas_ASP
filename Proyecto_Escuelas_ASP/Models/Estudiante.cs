using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Escuelas_ASP.Models
{
    public class Estudiante
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Personas")]
        public string PersonaId { get; set; }
        public int CursoId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public virtual Persona Personas { get; set; }
        public virtual Curso Cursos { get; set; }
        
    }
}
