using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class Lixeira
{
    public int LixeiraId { get; set; }

    public int? NotasId { get; set; }

    public virtual Nota? Notas { get; set; }
}
