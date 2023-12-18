using System;
using System.Collections.Generic;

namespace Kafe_Bulochka.DataBases;

public partial class Order
{
    public int Id { get; set; }

    public int Tables { get; set; }

    public int NumberPersons { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string CookingStatus { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public DateTime DateTimes { get; set; }

    public int Waiter { get; set; }

    public virtual ICollection<OrderedDish> OrderedDishes { get; set; } = new List<OrderedDish>();

    public virtual User WaiterNavigation { get; set; } = null!;
}
