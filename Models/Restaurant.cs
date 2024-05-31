using System;
using System.Collections.Generic;

namespace SipStation;

public partial class Restaurant
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<Drink> Drinks { get; set; } = new List<Drink>();
}