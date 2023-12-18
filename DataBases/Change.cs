using System;
using System.Collections.Generic;

namespace Kafe_Bulochka.DataBases;

public partial class Change
{
    public DateTime DateChanges { get; set; }

    public string ChangesTitle { get; set; } = null!;

    public int UsersId { get; set; }

    public virtual User Users { get; set; } = null!;
}
