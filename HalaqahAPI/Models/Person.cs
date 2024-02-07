using System;
using System.Collections.Generic;

namespace HalaqahAPI.Models;

public partial class Person : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int MasjidId { get; set; }

    public virtual Masjid Masjid { get; set; } = null!;

    public virtual Student? Student { get; set; }

    public virtual User? User { get; set; }
}
