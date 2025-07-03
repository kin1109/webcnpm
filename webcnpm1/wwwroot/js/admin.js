// Dữ liệu mẫu
let repairs = [
    { id: 1, customer: 'Trần Thị B', device: 'iPhone 13', issue: 'Màn hình bị vỡ', status: 'pending', date: '2025-01-15', phone: '0987654321' },
    { id: 2, customer: 'Lê Văn C', device: 'Samsung Galaxy S21', issue: 'Pin chai', status: 'processing', date: '2025-01-14', phone: '0912345678' }
];

// Hiển thị section được chọn
function showSection(sectionId) {
    document.querySelectorAll('.section').forEach(section => {
        section.classList.remove('active');
    });
    document.querySelectorAll('.nav-item').forEach(item => {
        item.classList.remove('active');
    });

    document.getElementById(sectionId).classList.add('active');
    event.target.classList.add('active');

    // Gọi hàm load dữ liệu tương ứng nếu có
    if (sectionId === 'dashboard') updateDashboard();
}

// Cập nhật số liệu dashboard
function updateDashboard() {
    document.getElementById('totalRepairs').textContent = repairs.length;
    const pending = repairs.filter(r => r.status === 'pending').length;
    const completed = repairs.filter(r => r.status === 'completed').length;
    const revenue = 1850000; // Ví dụ tạm
    document.getElementById('pendingRepairs').textContent = pending;
    document.getElementById('completedRepairs').textContent = completed;
    document.getElementById('totalRevenue').textContent = revenue.toLocaleString('vi-VN') + 'đ';
}

// Khởi tạo mặc định
document.addEventListener('DOMContentLoaded', () => {
    updateDashboard();
});
