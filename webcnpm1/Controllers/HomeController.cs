using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webcnpm1.Models;
using webcnpm1.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace webcnpm1.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebcnpmDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, WebcnpmDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pendingStatuses = new List<string> { "received", "in_progress", "waiting_for_component" };
            
            var viewModel = new DashboardViewModel
            {
                PendingOrders = await _context.RepairOrders.CountAsync(o => o.Status != null && pendingStatuses.Contains(o.Status)),
                CompletedOrders = await _context.RepairOrders.CountAsync(o => o.Status == "completed"),
                TotalCustomers = await _context.Customers.CountAsync(),
                TotalRevenue = await _context.Invoices
                                            .Where(i => i.PaymentStatus == "paid")
                                            .SumAsync(i => i.TotalAmount ?? 0),
                RecentActivities = await _context.RepairOrders
                                            .Include(r => r.Customer)
                                            .OrderByDescending(r => r.CreatedAt)
                                            .Take(5)
                                            .ToListAsync(),
                OrdersByStatus = await _context.RepairOrders
                                            .Where(o => o.Status != null)
                                            .GroupBy(o => o.Status!)
                                            .ToDictionaryAsync(g => g.Key, g => g.Count())
            };

            var invoicesThisYear = await _context.Invoices
                .Where(i => i.PaymentStatus == "paid" && i.CreatedAt.HasValue && i.CreatedAt.Value.Year == DateTime.Now.Year)
                .ToListAsync();

            viewModel.RevenueByMonth = invoicesThisYear
                .GroupBy(i => i.CreatedAt!.Value.Month)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key.ToString("d2"), g => g.Sum(i => i.TotalAmount ?? 0));

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
