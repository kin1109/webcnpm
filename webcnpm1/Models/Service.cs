using System;
using System.Collections.Generic;

namespace webcnpm1.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<RepairService> RepairServices { get; set; } = new List<RepairService>();
}
