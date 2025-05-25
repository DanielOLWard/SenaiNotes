using SenaiNotes.Models;

namespace SenaiNotes.ViewModel
{
    public class TagViewModel
    {
        public int TagsId { get; set; }

        public string NomeTag { get; set; } = null!;

        public int? UsuarioId { get; set; }
    }
}
