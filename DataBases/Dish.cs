using System;
using System.Collections.Generic;

namespace Kafe_Bulochka.DataBases;

public partial class Dish
{
    public int Id { get; set; }

    public string Tittle { get; set; } = null!;

    public int CategoryDishesId { get; set; }

    public decimal Prise { get; set; }

    public virtual CategoryDish CategoryDishes { get; set; } = null!;

    public virtual ICollection<OrderedDish> OrderedDishes { get; set; } = new List<OrderedDish>();
}
