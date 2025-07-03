using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webcnpm1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webcnpm1.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly WebcnpmDbContext _context;

        public IndexModel(WebcnpmDbContext context)
        {
            _context = context;
        }

        public IList<Models.User> Users { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.User.AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(u =>
                    u.FullName.Contains(SearchString) ||
                    u.Username.Contains(SearchString) ||
                    u.Email.Contains(SearchString)
                );
            }

            Users = await query.OrderByDescending(u => u.CreatedAt).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.RepairOrders)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Kiểm tra xem người dùng có đơn sửa chữa không
            if (user.RepairOrders != null && user.RepairOrders.Any())
            {
                TempData["ErrorMessage"] = "Không thể xóa người dùng này vì họ đã có đơn sửa chữa trong hệ thống.";
                return RedirectToPage();
            }

            try
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa người dùng thành công!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa người dùng. Vui lòng thử lại.";
            }

            return RedirectToPage();
        }
    }
}
