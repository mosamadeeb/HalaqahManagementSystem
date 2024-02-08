using System;
using System.Collections.Generic;

namespace HalaqahModel.Models;

public partial class SemesterRecord : BaseEntity
{
    public int StudentId { get; set; }

    public int SemesterId { get; set; }

    public int? HifzExistingRecord { get; set; }

    public int HifzTargetRecord { get; set; }

    public int HifzTargetNumPages { get; set; }

    public int? RevisionExistingRecord { get; set; }

    public int RevisionTargetRecord { get; set; }

    public int RevisionTargetNumPages { get; set; }

    public int? ExamGrade { get; set; }

    public virtual Record? HifzExistingRecordNavigation { get; set; }

    public virtual Record HifzTargetRecordNavigation { get; set; } = null!;

    public virtual Record? RevisionExistingRecordNavigation { get; set; }

    public virtual Record RevisionTargetRecordNavigation { get; set; } = null!;

    public virtual Semester Semester { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
