using System;
using System.Collections.Generic;

namespace HalaqahModel.Models;

public partial class Semester : BaseEntity
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<Halaqah> Halaqahs { get; set; } = new List<Halaqah>();

    public virtual ICollection<SemesterRecord> SemesterRecords { get; set; } = new List<SemesterRecord>();
}
