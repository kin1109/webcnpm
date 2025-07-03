using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using webcnpm1.Models;
using webcnpm1.ViewModels;

namespace webcnpm1.Controllers
{
    public class RepairOrderController : Controller
    {
        private readonly WebcnpmDbContext _context;

        public RepairOrderController(WebcnpmDbContext context)
        {
            _context = context;
        }

        // GET: RepairOrder
        public async Task<IActionResult> Index()
        {
            var repairOrders = _context.RepairOrders.Include(r => r.Customer).Include(r => r.AssignedToNavigation);
            return View(await repairOrders.ToListAsync());
        }

        // GET: RepairOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairOrder = await _context.RepairOrders
                .Include(r => r.Customer)
                .Include(r => r.AssignedToNavigation)
                .Include(r => r.RepairServices)
                    .ThenInclude(rs => rs.Service)
                .Include(r => r.RepairComponents)
                    .ThenInclude(rc => rc.Component)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (repairOrder == null)
            {
                return NotFound();
            }

            return View(repairOrder);
        }

        // GET: RepairOrder/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateRepairOrderViewModel
            {
                CustomerList = new SelectList(await _context.Customers.ToListAsync(), "Id", "FullName"),
                UserList = new SelectList(await _context.User.ToListAsync(), "Id", "FullName"),
                Status = "Đang chờ"
            };
            return View(viewModel);
        }

        // POST: RepairOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRepairOrderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var repairOrder = new RepairOrder
                {
                    ProblemDescription = viewModel.ProblemDescription,
                    Status = viewModel.Status,
                    CustomerId = viewModel.CustomerId,
                    AssignedTo = viewModel.AssignedTo,
                    CreatedAt = System.DateTime.Now
                };

                _context.Add(repairOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdown lists if model state is invalid
            viewModel.CustomerList = new SelectList(await _context.Customers.ToListAsync(), "Id", "FullName", viewModel.CustomerId);
            viewModel.UserList = new SelectList(await _context.User.ToListAsync(), "Id", "FullName", viewModel.AssignedTo);
            return View(viewModel);
        }

        // GET: RepairOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairOrder = await _context.RepairOrders.FindAsync(id);
            if (repairOrder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", repairOrder.CustomerId);
            ViewData["AssignedTo"] = new SelectList(_context.User, "Id", "FullName", repairOrder.AssignedTo);
            return View(repairOrder);
        }

        // POST: RepairOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProblemDescription,Status,CreatedAt,CustomerId,AssignedTo")] RepairOrder repairOrder)
        {
            if (id != repairOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repairOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepairOrderExists(repairOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", repairOrder.CustomerId);
            ViewData["AssignedTo"] = new SelectList(_context.User, "Id", "FullName", repairOrder.AssignedTo);
            return View(repairOrder);
        }

        // GET: RepairOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairOrder = await _context.RepairOrders
                .Include(r => r.Customer)
                .Include(r => r.AssignedToNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repairOrder == null)
            {
                return NotFound();
            }

            return View(repairOrder);
        }

        // POST: RepairOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repairOrder = await _context.RepairOrders.FindAsync(id);
            if (repairOrder != null)
            {
                _context.RepairOrders.Remove(repairOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairOrderExists(int id)
        {
            return _context.RepairOrders.Any(e => e.Id == id);
        }
    }
} 