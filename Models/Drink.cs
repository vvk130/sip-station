using System;
using System.Collections.Generic;

namespace SipStation;

public partial class Drink
{
    public int Id { get; set; }

    public string Sku { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ImgUrl { get; set; } = null!;

    public int? RestaurantId { get; set; }

    public decimal? Price { get; set; }

    public virtual Restaurant? Restaurant { get; set; }
}
