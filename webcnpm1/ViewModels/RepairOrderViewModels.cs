using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace webcnpm1.ViewModels
{
    public class CreateRepairOrderViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn khách hàng.")]
        [Display(Name = "Khách hàng")]
        public int CustomerId { get; set; }

        [Display(Name = "Nhân viên phụ trách")]
        public int? AssignedTo { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả sự cố.")]
        [Display(Name = "Mô tả sự cố")]
        public string ProblemDescription { get; set; } = string.Empty;

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Đang chờ";

        // Properties for dropdown lists
        public SelectList? CustomerList { get; set; }
        public SelectList? UserList { get; set; }
    }
} 