using System;
using System.Collections.Generic;

namespace SipStation;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Sku { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? ImageUrl { get; set; }

    public int? Stock { get; set; }

    public string? Brand { get; set; }

    public string? Maincategory { get; set; }

    public string? Color { get; set; }

    public decimal? Discount { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
