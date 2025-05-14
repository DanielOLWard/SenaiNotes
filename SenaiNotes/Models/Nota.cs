using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class Nota
{
    public int NotasId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Subtitulo { get; set; } = null!;

    public string ConteudoNotas { get; set; } = null!;

    public int? UsuarioId { get; set; }

    public virtual ICollection<Lixeira> Lixeiras { get; set; } = new List<Lixeira>();

    public virtual ICollection<TagNota> TagNota { get; set; } = new List<TagNota>();

    public virtual Usuario? Usuario { get; set; }
}
