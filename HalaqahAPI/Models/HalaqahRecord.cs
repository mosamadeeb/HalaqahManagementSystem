using System;
using System.Collections.Generic;

namespace HalaqahAPI.Models;

public partial class HalaqahRecord : BaseEntity
{
    public int StudentId { get; set; }

    public DateTime Date { get; set; }

    public string? Notes { get; set; }

    public int? HifzRecordId { get; set; }

    public int? RevisionRecordId { get; set; }

    public int HifzNumPages { get; set; }

    public int HifzTargetNumPages { get; set; }

    public int RevisionNumPages { get; set; }

    public int RevisionTargetNumPages { get; set; }

    public int TathbeetNumPages { get; set; }

    public int TathbeetTargetNumPages { get; set; }

    public virtual Record? HifzRecord { get; set; }

    public virtual Record? RevisionRecord { get; set; }

    public virtual Student Student { get; set; } = null!;
}
