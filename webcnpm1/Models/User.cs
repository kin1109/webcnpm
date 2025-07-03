using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webcnpm1.Models;

public partial class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải từ 3-50 ký tự")]
    [Display(Name = "Tên đăng nhập")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Tên đăng nhập chỉ được chứa chữ cái, số và dấu gạch dưới")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên")]
    [Display(Name = "Mật khẩu")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Họ tên không được để trống")]
    [Display(Name = "Họ và tên")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Họ tên phải từ 2-100 ký tự")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Vai trò không được để trống")]
    [Display(Name = "Vai trò")]
    public string Role { get; set; } = null!;

    [Display(Name = "Ngày tạo")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<RepairOrder> RepairOrders { get; set; } = new List<RepairOrder>();
}
