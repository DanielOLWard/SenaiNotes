using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class TipoUsuario
{
    public int TipoUsuarioId { get; set; }

    public string? Descricao { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
