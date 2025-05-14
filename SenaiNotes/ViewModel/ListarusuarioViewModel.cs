namespace SenaiNotes.ViewModel
{
    public class ListarusuarioViewModel
    {
        public int UsuarioId { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telefone { get; set; } = null!;

        public DateOnly DataCadastro { get; set; }

        public int? TipoUsuarioId { get; set; }
    }
}
