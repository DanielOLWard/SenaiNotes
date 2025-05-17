namespace SenaiNotes.Dto
{
    public class AtualizarusuarioDto
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;

        public string Telefone { get; set; } = null!;

        public DateTime DataAtualizacao { get; set; }

        public int? TipoUsuarioId { get; set; }
    }
}
