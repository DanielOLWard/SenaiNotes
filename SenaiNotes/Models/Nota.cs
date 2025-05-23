using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class Nota
{
    public int NotasId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? ConteudoNotas { get; set; }

    public int UsuarioId { get; set; }

    public bool? Lixeira { get; set; }

    public string? Imagem { get; set; }

    public bool? Arquivado { get; set; }

    public virtual ICollection<TagNota> TagNota { get; set; } = new List<TagNota>();

    public virtual Usuario Usuario { get; set; }
}
