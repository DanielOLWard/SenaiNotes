using SenaiNotes.Models;

namespace SenaiNotes.ViewModel
{
    public class ListarNotaViewModel
    {
        public int NotasId { get; set; }

        public int UsuarioId { get; set; }

        public string Titulo { get; set; } = null!;

        public string? ConteudoNotas { get; set; }

        public bool? Lixeira { get; set; }

        public string? Imagem { get; set; }

        public bool? Arquivado { get; set; }

        public List<TagViewModel> TagsId { get; set; }
    }
}
