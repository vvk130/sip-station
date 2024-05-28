using System;
using System.Collections.Generic;

namespace SipStation;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public string? UserId { get; set; }

    public decimal? Total { get; set; }

    public int? PaymentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual OrderItem? Order { get; set; }
}
