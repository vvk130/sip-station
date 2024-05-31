using System;
using System.Collections.Generic;

namespace SipStation;

public partial class ShoppingSession
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public decimal? Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
