using BE.Interface;
using BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE.Repository
{
    public class ChitiethoadonRepositoryH:IChitiethoadonH
    {
        private readonly db_websitebanhangContext _context;

        public ChitiethoadonRepositoryH(db_websitebanhangContext context)
        {
            _context = context;
        }


        public async Task<bool> kTracthd(string idUser, string idsp)
        {
            return await _context.Hoadons
                                 .Where(hd => hd.Taikhoan == idUser &&
                                              hd.Chitiethoadons.Any(cthd => cthd.IdChitietSp == idsp) &&
                                              hd.IdTrangthai == 0)
                                 .AnyAsync();
        }

        public async Task<String> addProduct( String Iduser, String idctsp)
        {
            try
            {
                String maHD = await checkHoaDon(Iduser);

                var exitHoadon = await _context.Hoadons
                                                 .Where(hd => hd.MaHoadon == maHD)
                                                 .ToListAsync();
                if (exitHoadon.Count ==0)
                {
                    Hoadon newhoadon = new Hoadon
                    {
                        MaHoadon = maHD,
                        Taikhoan = Iduser,
                        IdTrangthai = 0

                    };
                    _context.Hoadons.Add(newhoadon);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine($"Error: {ex.InnerException?.Message}");
                        throw;
                    }


                }
                Chitiethoadon newchitiethoadon = new Chitiethoadon
                {
                    MaHoadon = maHD,
                    IdChitietHd = $"{GenerateID()}{Iduser.Substring(3)}{idctsp.Substring(4)}",
                    IdChitietSp = idctsp,
                    SoLuong = 1
                };
                _context.Chitiethoadons.Add(newchitiethoadon);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error: {ex.InnerException?.Message}");
                    throw;
                }

                return "1";
            } catch(Exception ex)
            {
                return ex.Message;
            }
            
        }
        string GenerateID()
        {
            string day = DateTime.Now.Day.ToString("D2"); 
            string month = DateTime.Now.Month.ToString("D2");
            string year = DateTime.Now.Year.ToString().Substring(2);

            return $"{day}{month}{year}";
        }
        private async Task<string> checkHoaDon(string idUser)
        {
            var userInvoices = await _context.Hoadons
                                             .Where(hd => hd.Taikhoan == idUser)
                                             .ToListAsync();  

            if (userInvoices.Count == 0)
            {
                var idhd = idUser.Length > 3 ? idUser.Substring(3) + "001" : "00000001";
                return idhd.PadLeft(10,'0');
            }

            var openInvoice = userInvoices.FirstOrDefault(hd => hd.IdTrangthai == 0); 

            if (openInvoice != null)
            {
                return openInvoice.MaHoadon.ToString(); 
            }
            else
            {
                var maxInvoice = userInvoices.Max(hd => hd.MaHoadon);
                return (int.Parse(maxInvoice.ToString()) + 1).ToString().PadLeft(10, '0');
            }
        }

        public async Task<IEnumerable<object>> getGiohang(string idUser)
        {
            var result = await _context.Hoadons
                                .Where(hd => hd.IdTrangthai == 0 && hd.Taikhoan == idUser)
                                .SelectMany(hd => hd.Chitiethoadons)
                                .Select(cthd => new
                                {
                                    idCthd = cthd.IdChitietHd,
                                    idHD = cthd.MaHoadonNavigation.MaHoadon,
                                    soluong = cthd.SoLuong,
                                    hinhanh = cthd.IdChitietSpNavigation.HinhAnh,
                                    gia = cthd.IdChitietSpNavigation.Gia,
                                    tensanpham = cthd.IdChitietSpNavigation.MaSanphamNavigation.TenSanpham,
                                    mau = cthd.IdChitietSpNavigation.MaMauNavigation.TenMau,
                                    soluongton = cthd.IdChitietSpNavigation.SoLuongTon,
                                    makhuyenmai = cthd.IdChitietSpNavigation.MaSanphamNavigation.MaKhuyenmaiNavigation.PhanTram
                                })
                                .ToListAsync();
            return result;
        }
    }
}
