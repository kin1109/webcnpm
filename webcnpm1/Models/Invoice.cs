using System;
using System.Collections.Generic;

namespace webcnpm1.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public int? RepairOrderId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual RepairOrder? RepairOrder { get; set; }
}
