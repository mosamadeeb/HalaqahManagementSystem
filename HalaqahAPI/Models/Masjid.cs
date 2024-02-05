using System;
using System.Collections.Generic;

namespace HalaqahAPI.Models;

public partial class Masjid
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
