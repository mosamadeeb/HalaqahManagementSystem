using System;
using System.Collections.Generic;

namespace HalaqahAPI.Models;

public partial class UserAttendance
{
    public int UserId { get; set; }

    public DateTime Timestamp { get; set; }

    public bool IsPresent { get; set; }

    public virtual User User { get; set; } = null!;
}
