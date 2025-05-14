using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public DateOnly DataCadastro { get; set; }

    public DateOnly DataAtualizacao { get; set; }

    public int? TipoUsuarioId { get; set; }

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();

    public virtual TipoUsuario? TipoUsuario { get; set; }
}
