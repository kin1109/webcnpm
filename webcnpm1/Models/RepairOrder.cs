using System;
using System.Collections.Generic;

namespace webcnpm1.Models;

public partial class RepairOrder
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int? DeviceId { get; set; }

    public string? ProblemDescription { get; set; }

    public string? Status { get; set; }

    public int? AssignedTo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? AssignedToNavigation { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Device? Device { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<RepairComponent> RepairComponents { get; set; } = new List<RepairComponent>();

    public virtual ICollection<RepairService> RepairServices { get; set; } = new List<RepairService>();
}
