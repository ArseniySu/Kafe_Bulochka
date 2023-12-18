using System;
using System.Collections.Generic;

namespace Kafe_Bulochka.DataBases;

public partial class CategoryDish
{
    public int Id { get; set; }

    public string Tittle { get; set; } = null!;

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
