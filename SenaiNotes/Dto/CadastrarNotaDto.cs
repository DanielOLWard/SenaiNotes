using SenaiNotes.Models;

namespace SenaiNotes.Dto
{
    public class CadastrarNotaDto
    {
        public string Titulo { get; set; } = null!;

        public string ConteudoNotas { get; set; } = null!;

        public int UsuarioId { get; set; }

        public string? Imagem { get; set; }

        public List<string> Tags { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
