using System.ComponentModel.DataAnnotations;

namespace PaginaColetaEmailVideo.Models
{
    public class EmailModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o Nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira o Email!")]
        public string Email { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataDeRegistro { get; set; } = DateTime.Now;
    }
}
