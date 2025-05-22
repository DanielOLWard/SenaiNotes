namespace SenaiNotes.Dto
{
    public class CadastrarNotaDto
    {
        public string Titulo { get; set; } = null!;

        public string ConteudoNotas { get; set; } = null!;

        public int? UsuarioId { get; set; }

        public bool? Lixeira { get; set; }

        public string? Imagem { get; set; }

        public bool? Arquivado { get; set; }

        public List<string> Tags { get; set; }
    }
}
