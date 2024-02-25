using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HalaqahModel.Models;

public partial class Person : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int MasjidId { get; set; }

    public virtual Masjid? Masjid { get; set; } = null!;

    public string FullName => $"{Name} {Surname}";
}
