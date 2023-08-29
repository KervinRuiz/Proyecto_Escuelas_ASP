using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Escuelas_ASP.Models
{
    public class Profesor
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Personas")]
        public string PersonaId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
        public virtual Persona Personas { get; set; }
        public virtual IEnumerable<Materia>? Materias { get; set; }
    }
}
