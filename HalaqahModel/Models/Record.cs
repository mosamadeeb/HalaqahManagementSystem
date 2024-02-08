using System;
using System.Collections.Generic;

namespace HalaqahModel.Models;

public partial class Record : BaseEntity
{
    public int Id { get; set; }

    public virtual ICollection<HalaqahRecord> HalaqahRecordHifzRecords { get; set; } = new List<HalaqahRecord>();

    public virtual ICollection<HalaqahRecord> HalaqahRecordRevisionRecords { get; set; } = new List<HalaqahRecord>();

    public virtual ICollection<Segment> Segments { get; set; } = new List<Segment>();

    public virtual ICollection<SemesterRecord> SemesterRecordHifzExistingRecordNavigations { get; set; } = new List<SemesterRecord>();

    public virtual ICollection<SemesterRecord> SemesterRecordHifzTargetRecordNavigations { get; set; } = new List<SemesterRecord>();

    public virtual ICollection<SemesterRecord> SemesterRecordRevisionExistingRecordNavigations { get; set; } = new List<SemesterRecord>();

    public virtual ICollection<SemesterRecord> SemesterRecordRevisionTargetRecordNavigations { get; set; } = new List<SemesterRecord>();
}
