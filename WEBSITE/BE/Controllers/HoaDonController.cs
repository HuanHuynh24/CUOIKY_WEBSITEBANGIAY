using BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly db_websitebanhangContext _context; // DbContext bạn scaffold
        public class OrderDto
        {
            public string MaHoadon { get; set; }
            public string Trangthai { get; set; }
        }
        public HoaDonController(db_websitebanhangContext context)
        {
            _context = context;
        }

        // GET: api/HoaDon
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Hoadons
                .Include(h => h.TaikhoanNavigation) // Join với bảng Users
                .Include(h => h.IdTrangthaiNavigation) // Join với bảng Trangthai
                .Select(h => new
                {
                    MaHoaDon = h.MaHoadon,
                    TenKhachHang = h.TaikhoanNavigation.Ten,
                    DiaChiHD = h.DiaChi,
                    TongTienHD = h.TongTien,
                    TrangThai = h.IdTrangthaiNavigation.TenTrangthai,
                    NgayTaoHD = h.NgayTao,
                    NgayGiaoHD = h.NgayGiao
                    //UpdateStatus = "Update" // Static button text
                })
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/HoaDon/{maHoadon}
        [HttpGet("{maHoadon}")]
        public async Task<IActionResult> GetOrderDetails(string maHoadon)
        {
            var orderDetails = await _context.Chitiethoadons
                .Where(cthd => cthd.MaHoadon == maHoadon) // Lọc theo mã hóa đơn
                .Include(cthd => cthd.IdChitietSpNavigation) // Join với bảng Chitietsanpham
                    .ThenInclude(ctsp => ctsp.MaSanphamNavigation) // Join với bảng Sanpham
                        .ThenInclude(sp => sp.MaNhanNavigation) // Join với bảng Nhanhieu
                .Include(cthd => cthd.IdChitietSpNavigation.MaSanphamNavigation.MaKhuyenmaiNavigation) // Join với Khuyenmai
                .Include(cthd => cthd.IdChitietSpNavigation.MaMauNavigation) // Join với Color
                .Include(cthd => cthd.IdChitietSpNavigation.MaSizeNavigation) // Join với Size
                .Select(cthd => new
                {
                    TenSP = cthd.IdChitietSpNavigation.MaSanphamNavigation.TenSanpham, // Tên sản phẩm
                    ThuongHieu = cthd.IdChitietSpNavigation.MaSanphamNavigation.MaNhanNavigation.TenNhan, // Nhà sản xuất
                    GiaSP = cthd.IdChitietSpNavigation.Gia, // Giá đơn vị
                    SoLuongSP = cthd.SoLuong, // Số lượng
                    Mau = cthd.IdChitietSpNavigation.MaMauNavigation.TenMau, // Tên màu
                    KichThuoc = cthd.IdChitietSpNavigation.MaSize,
                    KhuyenMai = cthd.IdChitietSpNavigation.MaSanphamNavigation.MaKhuyenmaiNavigation.PhanTram, // Phần trăm khuyến mãi
                    TongGia = (cthd.IdChitietSpNavigation.Gia * cthd.SoLuong) -
                                ((cthd.IdChitietSpNavigation.Gia * cthd.SoLuong) *
                                 ((decimal)(cthd.IdChitietSpNavigation.MaSanphamNavigation.MaKhuyenmaiNavigation.PhanTram ?? 0) / 100)), // Tổng tiền
                    NgayDat = cthd.MaHoadonNavigation.NgayTao // Ngày đặt hàng
                })
                .ToListAsync();

            if (!orderDetails.Any())
            {
                return NotFound(new { message = "Không tìm thấy chi tiết hóa đơn." });
            }

            return Ok(orderDetails);
        }

        // PUT: api/Orders/UpdateStatus
        /*[HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateOrderStatus(string maHoadon, int newStatusId)
        {
            if (string.IsNullOrEmpty(maHoadon))
            {
                return BadRequest(new { message = "Mã hóa đơn không được để trống." });
            }

            // Tìm hóa đơn theo mã hóa đơn
            var order = await _context.Hoadons.FirstOrDefaultAsync(h => h.MaHoadon == maHoadon);
            if (order == null)
            {
                return NotFound(new { message = "Hóa đơn không tồn tại." });
            }

            // Kiểm tra trạng thái mới có hợp lệ không
            var status = await _context.Trangthais.FirstOrDefaultAsync(t => t.Id == newStatusId);
            if (status == null)
            {
                return BadRequest(new { message = "Trạng thái mới không hợp lệ." });
            }

            // Cập nhật trạng thái
            order.IdTrangthai = newStatusId;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Cập nhật trạng thái thành công.", order });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi trong quá trình cập nhật.", error = ex.Message });
            }
        }*/


        /*[HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateOrderStatus(string maHoadon, int newStatusId)
        {
            if (string.IsNullOrEmpty(maHoadon))
            {
                return BadRequest(new { message = "Mã hóa đơn không được để trống." });
            }

            var order = await _context.Hoadons.Include(h => h.IdTrangthaiNavigation)
                                              .FirstOrDefaultAsync(h => h.MaHoadon == maHoadon);
            if (order == null)
            {
                return NotFound(new { message = "Hóa đơn không tồn tại." });
            }

            var status = await _context.Trangthais.FirstOrDefaultAsync(t => t.Id == newStatusId);
            if (status == null)
            {
                return BadRequest(new { message = "Trạng thái mới không hợp lệ." });
            }

            order.IdTrangthai = newStatusId;

            try
            {
                await _context.SaveChangesAsync();
                var result = new OrderDto
                {
                    MaHoadon = order.MaHoadon,
                    Trangthai = status.TenTrangthai
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi trong quá trình cập nhật.", error = ex.Message });
            }
        }*/

        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusModel model)
        {
            var hoaDon = await _context.Hoadons.FindAsync(model.MaHoaDon);
            if (hoaDon == null)
            {
                return NotFound(new { message = "Không tìm thấy hóa đơn." });
            }

            // Cập nhật thông tin
            hoaDon.NgayGiao = DateTime.Parse(model.MaHoaDon);
            hoaDon.IdTrangthai = model.TrangThai;

            _context.Hoadons.Update(hoaDon);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cập nhật trạng thái thành công." });
        }
    }
}
