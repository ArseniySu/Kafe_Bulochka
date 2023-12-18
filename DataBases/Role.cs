using System;
using System.Collections.Generic;

namespace Kafe_Bulochka.DataBases;

public partial class Role
{
    public int Id { get; set; }

    public string Tittle { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
