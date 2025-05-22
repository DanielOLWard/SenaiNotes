using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class Tag
{
    public string NomeTag { get; set; } 
    public int TagsId { get; set; }
    public virtual ICollection<TagNota> TagNota { get; set; } = new List<TagNota>();
}
