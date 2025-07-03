using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webcnpm1.Models;
using webcnpm1.ViewModels;

namespace webcnpm1.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly WebcnpmDbContext _context;

        public CreateModel(WebcnpmDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CreateUserViewModel UserVM { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Kiểm tra username đã tồn tại chưa (không phân biệt hoa thường)
            if (await _context.User.AnyAsync(u => u.Username.ToLower() == UserVM.Username.ToLower()))
            {
                ModelState.AddModelError("UserVM.Username", "Tên đăng nhập này đã tồn tại");
                return Page();
            }

            // Kiểm tra email đã tồn tại chưa (không phân biệt hoa thường)
            if (await _context.User.AnyAsync(u => u.Email.ToLower() == UserVM.Email.ToLower()))
            {
                ModelState.AddModelError("UserVM.Email", "Email này đã được sử dụng");
                return Page();
            }

            var user = new Models.User
            {
                Email = UserVM.Email,
                Username = UserVM.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(UserVM.Password),
                FullName = UserVM.FullName,
                Role = UserVM.Role,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm người dùng thành công!";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                // Lấy lỗi chi tiết từ inner exception
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Lỗi cơ sở dữ liệu: {innerExceptionMessage}");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
                return Page();
            }
        }
    }
}
