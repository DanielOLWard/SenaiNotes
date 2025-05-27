using SenaiNotes.Models;

namespace SenaiNotes.Dto
{
    public class TagDto
    {
        public string NomeTag { get; set; } = null!;

        public int? UsuarioId { get; set; }

        public IFormFile ArquivoTag {  get; set; }
    }
}
