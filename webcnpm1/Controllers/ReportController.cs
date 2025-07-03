using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using webcnpm1.Models;

namespace webcnpm1.Controllers
{
    public class ReportController : Controller
    {
        private readonly WebcnpmDbContext _context;

        public ReportController(WebcnpmDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            // Set default date range if not provided (e.g., this month)
            if (!startDate.HasValue)
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            if (!endDate.HasValue)
            {
                endDate = startDate.Value.AddMonths(1).AddDays(-1);
            }

            ViewData["StartDate"] = startDate.Value.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate.Value.ToString("yyyy-MM-dd");

            var reportData = await _context.RepairOrders
                .Include(ro => ro.Customer)
                .Include(ro => ro.AssignedToNavigation)
                .Include(ro => ro.Invoices)
                .Where(ro => ro.CreatedAt >= startDate && ro.CreatedAt <= endDate)
                .OrderByDescending(ro => ro.CreatedAt)
                .ToListAsync();

            return View(reportData);
        }
    }
} 