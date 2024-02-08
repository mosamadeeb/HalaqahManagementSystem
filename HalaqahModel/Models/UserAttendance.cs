using System;
using System.Collections.Generic;

namespace HalaqahModel.Models;

public partial class UserAttendance : BaseEntity
{
    public int UserId { get; set; }

    public DateTime Timestamp { get; set; }

    public bool IsPresent { get; set; }

    public virtual User User { get; set; } = null!;
}
