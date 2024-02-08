using System;
using System.Collections.Generic;

namespace HalaqahModel.Models;

public partial class User : BaseEntity
{
    public int PersonId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public bool IsTeacher { get; set; }

    public virtual ICollection<Halaqah> HalaqahAdmins { get; set; } = new List<Halaqah>();

    public virtual ICollection<Halaqah> HalaqahTeachers { get; set; } = new List<Halaqah>();

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<UserAttendance> UserAttendances { get; set; } = new List<UserAttendance>();
}
