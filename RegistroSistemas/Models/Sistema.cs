using System.ComponentModel.DataAnnotations;

namespace RegistroSistemas.Models
{
    public class Sistema
    {
        [Key]
        public int SistemaId { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "solo se permiten Letras")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string?  Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]

        public string? Complejidad { get; set; }
        
    }
}
