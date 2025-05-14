namespace SenaiNotes.Dto
{
    public class CadastrarUsuarioDto
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;

        public string Telefone { get; set; } = null!;

        public DateOnly DataCadastro { get; set; }

        public int? TipoUsuarioId { get; set; }
    }
}
