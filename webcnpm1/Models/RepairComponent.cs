using System;
using System.Collections.Generic;

namespace webcnpm1.Models;

public partial class RepairComponent
{
    public int Id { get; set; }

    public int? RepairOrderId { get; set; }

    public int? ComponentId { get; set; }

    public int? Quantity { get; set; }

    public virtual Component? Component { get; set; }

    public virtual RepairOrder? RepairOrder { get; set; }
}
