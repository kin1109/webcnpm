using System;
using System.Collections.Generic;

namespace webcnpm1.Models;

public partial class Component
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? QuantityInStock { get; set; }

    public virtual ICollection<RepairComponent> RepairComponents { get; set; } = new List<RepairComponent>();
}
