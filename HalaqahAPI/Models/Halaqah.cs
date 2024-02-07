using System;
using System.Collections.Generic;

namespace HalaqahAPI.Models;

public partial class Halaqah : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int AdminId { get; set; }

    public int TeacherId { get; set; }

    public int SemesterId { get; set; }

    public int Grade { get; set; }

    public int WeekDays { get; set; }

    public virtual User Admin { get; set; } = null!;

    public virtual Semester Semester { get; set; } = null!;

    public virtual User Teacher { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
