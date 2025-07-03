using System;
using System.Collections.Generic;

namespace webcnpm1.Models;

public partial class RepairService
{
    public int Id { get; set; }

    public int? RepairOrderId { get; set; }

    public int? ServiceId { get; set; }

    public int? Quantity { get; set; }

    public virtual RepairOrder? RepairOrder { get; set; }

    public virtual Service? Service { get; set; }
}
