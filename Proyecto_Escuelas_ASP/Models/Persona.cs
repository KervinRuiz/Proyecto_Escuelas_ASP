using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Escuelas_ASP.Models
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Evita la generación automática
        [Required(ErrorMessage = "Identificacion requerida")]
        public string Identificacion { get; set; }
        [Required (ErrorMessage = "Nombre Requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = " Apellidos requeridos")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Sexo requerido")]
        public string Sexo { get; set;} // Enum
        [Required(ErrorMessage = "Fecha Nacimiento requerida")]
        public DateTime Fecha_Nacimiento { get; set;}
        [Required(ErrorMessage = "Direccion requerida")]
        public string Direccion { get; set;}
        public DateTime Fecha_Creacion { get; set;}
        public bool Estado { get; set;}
        public virtual IEnumerable<Estudiante>? Estudiantes { get; set; }
        public virtual IEnumerable<Profesor>? Profesores { get; set; }
        public virtual IEnumerable<Nota_Materia>? Nota_Materias { get; set; }
    }
}
