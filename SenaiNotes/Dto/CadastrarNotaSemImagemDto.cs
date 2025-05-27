namespace SenaiNotes.Dto
{
    public class CadastrarNotaSemImagemDto
    {
        public int UsuarioId { get; set; }
        public string Titulo { get; set; } = null!;
        public string ConteudoNotas { get; set; } = null!;
        public virtual ICollection<string> Tags { get; set; } = new List<string>();
    }
}
