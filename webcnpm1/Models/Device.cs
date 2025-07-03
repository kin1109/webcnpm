using System;
using System.Collections.Generic;

namespace webcnpm1.Models;

public partial class Device
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? Type { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<RepairOrder> RepairOrders { get; set; } = new List<RepairOrder>();
}
