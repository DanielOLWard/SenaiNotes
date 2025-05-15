using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class TagNota
{
    public int TagNotasId { get; set; }

    public int? NotasId { get; set; }

    public int? TagsId { get; set; }

    public virtual Nota? Notas { get; set; }

    public virtual Tag? Tags { get; set; }

    public virtual ICollection<TagNota> Tag { get; set; } = new List<TagNota>();
}
