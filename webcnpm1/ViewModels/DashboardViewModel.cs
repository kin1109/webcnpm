using System.Collections.Generic;
using webcnpm1.Models;

namespace webcnpm1.ViewModels
{
    public class DashboardViewModel
    {
        public int PendingOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int TotalCustomers { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<RepairOrder> RecentActivities { get; set; } = new List<RepairOrder>();
        public Dictionary<string, int> OrdersByStatus { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, decimal> RevenueByMonth { get; set; } = new Dictionary<string, decimal>();
    }
} 