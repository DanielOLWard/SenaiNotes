using SenaiNotes.Models;

namespace SenaiNotes.Dto
{
    public class TagDto
    {
        public string NomeTag { get; set; } = null!;
        public int Id { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
