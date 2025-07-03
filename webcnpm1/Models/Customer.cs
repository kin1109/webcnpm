using System;
using System.Collections.Generic;

namespace webcnpm1.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    public virtual ICollection<RepairOrder> RepairOrders { get; set; } = new List<RepairOrder>();
}
