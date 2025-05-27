using SenaiNotes.Models;

namespace SenaiNotes.Dto
{
    public class CadastrarNotaDto
    {
        public int UsuarioId { get; set; }
        public string Titulo { get; set; } = null!;
        public string ConteudoNotas { get; set; } = null!;
        public string? Imagem { get; set; }
        public IFormFile? ArquivoNotas { get; set; }  
        public virtual ICollection<string> Tags { get; set; } = new List<string>();
    }
}
