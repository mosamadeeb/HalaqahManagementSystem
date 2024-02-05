using System;
using System.Collections.Generic;

namespace HalaqahAPI.Models;

public enum AttendanceStatus
{
    Present = 0,
    AbsentExcused = 1,
    AbsentNotExcused = 2,
    LateExcused = 3,
    LateNotExcused = 4
}

public partial class StudentAttendance
{
    public int StudentId { get; set; }

    public DateTime Timestamp { get; set; }

    public AttendanceStatus Status { get; set; }

    public bool HasCompleted { get; set; }

    public bool HasDress { get; set; }

    public virtual Student Student { get; set; } = null!;
}
