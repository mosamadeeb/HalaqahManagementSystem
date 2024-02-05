using System;
using System.Collections.Generic;

namespace HalaqahAPI.Models;

public partial class Semester
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<Halaqah> Halaqahs { get; set; } = new List<Halaqah>();

    public virtual ICollection<SemesterRecord> SemesterRecords { get; set; } = new List<SemesterRecord>();
}
