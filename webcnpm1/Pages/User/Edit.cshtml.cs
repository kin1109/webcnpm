using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webcnpm1.Models;
using webcnpm1.ViewModels;

namespace webcnpm1.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly WebcnpmDbContext _context;

        public EditModel(WebcnpmDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EditUserViewModel UserVM { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            UserVM = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Ensure CreatedAt is preserved on validation failure
                var userForDate = await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == UserVM.Id);
                if (userForDate != null)
                {
                    UserVM.CreatedAt = userForDate.CreatedAt;
                }
                return Page();
            }

            // Kiểm tra username đã tồn tại chưa (trừ user hiện tại, không phân biệt hoa thường)
            if (await _context.User.AnyAsync(u => u.Username.ToLower() == UserVM.Username.ToLower() && u.Id != UserVM.Id))
            {
                ModelState.AddModelError("UserVM.Username", "Tên đăng nhập này đã tồn tại");
                return Page();
            }

            // Kiểm tra email đã tồn tại chưa (trừ user hiện tại, không phân biệt hoa thường)
            if (await _context.User.AnyAsync(u => u.Email.ToLower() == UserVM.Email.ToLower() && u.Id != UserVM.Id))
            {
                ModelState.AddModelError("UserVM.Email", "Email này đã được sử dụng");
                return Page();
            }

            var userToUpdate = await _context.User.FirstOrDefaultAsync(u => u.Id == UserVM.Id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            // Map the updated properties from the ViewModel to the entity
            userToUpdate.FullName = UserVM.FullName;
            userToUpdate.Username = UserVM.Username;
            userToUpdate.Email = UserVM.Email;
            userToUpdate.Role = UserVM.Role;

            // Nếu có nhập mật khẩu mới thì cập nhật
            if (!string.IsNullOrEmpty(UserVM.NewPassword))
            {
                userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(UserVM.NewPassword);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật người dùng thành công!";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExistsAsync(UserVM.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Lỗi cơ sở dữ liệu: {innerExceptionMessage}");
                UserVM.CreatedAt = userToUpdate.CreatedAt; // Preserve date on error
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
                // Preserve CreatedAt on error
                UserVM.CreatedAt = userToUpdate.CreatedAt;
                return Page();
            }
        }

        private async Task<bool> UserExistsAsync(int id)
        {
            return await _context.User.AnyAsync(e => e.Id == id);
        }
    }
}
