namespace SenaiNotes.ViewModel
{
    public class ListarNotaViewModel
    {
        public int NotasId { get; set; }

        public string Titulo { get; set; } = null!;

        public string Subtitulo { get; set; } = null!;

        public string ConteudoNotas { get; set; } = null!;

        public int? UsuarioId { get; set; }
    }
}
