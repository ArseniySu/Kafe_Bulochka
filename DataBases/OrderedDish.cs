using System;
using System.Collections.Generic;

namespace Kafe_Bulochka.DataBases;

public partial class OrderedDish
{
    public int OrdersId { get; set; }

    public int DishesId { get; set; }

    public int Quantity { get; set; }

    public virtual Dish Dishes { get; set; } = null!;

    public virtual Order Orders { get; set; } = null!;
}
