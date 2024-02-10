using System;
using System.Collections.Generic;

namespace HalaqahModel.Models;

public partial class Student : BaseEntity
{
    public int PersonId { get; set; }

    public int Grade { get; set; }

    public string? ParentPhone { get; set; }

    public virtual ICollection<HalaqahRecord>? HalaqahRecords { get; set; } = new List<HalaqahRecord>();

    public virtual Person? Person { get; set; } = null!;

    public virtual ICollection<SemesterRecord>? SemesterRecords { get; set; } = new List<SemesterRecord>();

    public virtual ICollection<StudentAttendance>? StudentAttendances { get; set; } = new List<StudentAttendance>();

    public virtual ICollection<Halaqah>? Halaqahs { get; set; } = new List<Halaqah>();
    
    public string FullName => $"{Person?.Name} {Person?.Surname}";
}
