using System;
using System.Collections.Generic;

namespace HalaqahModel.Models;

public partial class Segment : BaseEntity
{
    public int RecordId { get; set; }

    public int SegmentId { get; set; }

    public string SurahFrom { get; set; } = null!;

    public int AyahFrom { get; set; }

    public string? SurahTo { get; set; }

    public int? AyahTo { get; set; }

    public virtual Record Record { get; set; } = null!;
}
