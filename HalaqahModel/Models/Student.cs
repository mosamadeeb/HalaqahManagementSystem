using System;
using System.Collections.Generic;

namespace HalaqahModel.Models;

public partial class Student : Person
{
    public int Grade { get; set; }

    public string? ParentPhone { get; set; }

    public virtual ICollection<HalaqahRecord>? HalaqahRecords { get; set; } = new List<HalaqahRecord>();

    public virtual ICollection<SemesterRecord>? SemesterRecords { get; set; } = new List<SemesterRecord>();

    public virtual ICollection<StudentAttendance>? StudentAttendances { get; set; } = new List<StudentAttendance>();

    public virtual ICollection<Halaqah>? Halaqahs { get; set; } = new List<Halaqah>();
}
