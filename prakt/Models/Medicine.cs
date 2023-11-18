using System;
using System.Collections.Generic;

namespace prakt.Models;

public partial class Medicine
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Price { get; set; } = null!;
}
