using System;
using System.Collections.Generic;

namespace SipStation;

public partial class CartItem
{
    public int Id { get; set; }

    public int? SessionId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Product? Product { get; set; }
}
