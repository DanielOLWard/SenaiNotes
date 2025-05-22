using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class Tag
{
    public int TagsId { get; set; }

    public string NomeTag { get; set; } = null!;

    public int? UsuarioId { get; set; }

    public virtual ICollection<TagNota> TagNota { get; set; } = new List<TagNota>();

    public virtual Usuario? Usuario { get; set; }
}
