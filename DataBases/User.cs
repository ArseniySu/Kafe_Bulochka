using System;
using System.Collections.Generic;

namespace Kafe_Bulochka.DataBases;

public partial class User
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Sname { get; set; } = null!;

    public string? Mname { get; set; }

    public string Logins { get; set; } = null!;

    public string Passwords { get; set; } = null!;

    public int RoleId { get; set; }

    public byte[]? Photo { get; set; }

    public byte[]? Copys { get; set; }
    public string? Jobstatus { get; set; }

    public virtual ICollection<Change> Changes { get; set; } = new List<Change>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}
