using BE.Models;
using BE.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Model
{
    public class ThongkedoanhthuRepositoryADONET
    {
        private readonly IDbContextFactory<db_websitebanhangContext> _contextFactory;


        public ThongkedoanhthuRepositoryADONET(IDbContextFactory<db_websitebanhangContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private async Task<Thongke> GetRevenueAsync(Func<db_websitebanhangContext, Task<Thongke>> queryFunc)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await queryFunc(context);
        }

        public async Task<Thongke> GetRevenueForToday()
        {
            return await GetRevenueAsync(async (context) =>
            {
                var today = DateTime.Today;
                var yesterday = today.AddDays(-1);

                // Doanh thu hôm nay
                var todayRevenue = await context.Hoadons
                                                .Where(h => h.NgayTao.Value.Date == today)
                                                .GroupBy(h => today.ToString("yyyy/MM/dd"))
                                                .Select(g => new Thongke
                                                {
                                                    Label = today.ToString("yyyy/MM/dd"),
                                                    Sotien = g.Sum(h => h.TongTien ?? 0)
                                                })
                                                .FirstOrDefaultAsync() ?? new Thongke { Label = today.ToString("yyyy/MM/dd"), Sotien = 0 };

                // Doanh thu hôm qua
                var yesterdayRevenue = await context.Hoadons
                                                   .Where(h => h.NgayTao.Value.Date == yesterday)
                                                   .GroupBy(h => yesterday.ToString("yyyy/MM/dd"))
                                                   .Select(g => g.Sum(h => h.TongTien ?? 0))
                                                   .FirstOrDefaultAsync();

                if (yesterdayRevenue == null)
                {
                    yesterdayRevenue = 0;
                }

                // Tính mức thay đổi phần trăm
                decimal changePercentage = yesterdayRevenue == 0
                    ? 0 // Nếu hôm qua không có doanh thu, mức thay đổi là 0%
                    : ((todayRevenue.Sotien - yesterdayRevenue) / yesterdayRevenue) * 100;

                todayRevenue.thaydoi = changePercentage;
                // Trả về doanh thu hôm nay và mức thay đổi phần trăm

                return todayRevenue;
            });
        }

        public async Task<Thongke> GetRevenueForMonth()
        {
            return await GetRevenueAsync(async (context) =>
            {
                var today = DateTime.Today;

                // Doanh thu tháng hiện tại
                var currentMonthRevenue = await context.Hoadons
                                                       .Where(h => h.NgayTao.Value.Month == today.Month && h.NgayTao.Value.Year == today.Year)
                                                       .GroupBy(h => $"Tháng {today.Month}/{today.Year}")
                                                       .Select(g => new Thongke
                                                       {
                                                           Label = g.Key,
                                                           Sotien = g.Sum(h => h.TongTien ?? 0)
                                                       })
                                                       .FirstOrDefaultAsync() ?? new Thongke { Label = $"Tháng {today.Month}/{today.Year}", Sotien = 0 };

                // Tính tháng trước
                var previousMonth = today.AddMonths(-1);

                // Doanh thu tháng trước
                var previousMonthRevenue = await context.Hoadons
                                                        .Where(h => h.NgayTao.Value.Month == previousMonth.Month && h.NgayTao.Value.Year == previousMonth.Year)
                                                        .GroupBy(h => $"Tháng {previousMonth.Month}/{previousMonth.Year}")
                                                        .Select(g => g.Sum(h => h.TongTien ?? 0))
                                                        .FirstOrDefaultAsync();

                if (previousMonthRevenue == null)
                {
                    previousMonthRevenue = 0;
                }
                // Nếu tháng trước không có doanh thu, gán bằng 0.

                // Tính mức thay đổi phần trăm
                decimal changePercentage = previousMonthRevenue == 0
                    ? 0 // Nếu tháng trước không có doanh thu, mức thay đổi là 0%.
                    : ((currentMonthRevenue.Sotien - previousMonthRevenue) / previousMonthRevenue) * 100;

                currentMonthRevenue.thaydoi = changePercentage;
                return currentMonthRevenue;
            });
        }



     
        

        public async Task<Thongke> GetRevenueForYear()
        {
            return await GetRevenueAsync(async (context) =>
            {
                var today = DateTime.Today;

                // Doanh thu năm hiện tại
                   var currentYearRevenue = await context.Hoadons
                                                      .Where(h => h.NgayTao.Value.Year == today.Year)
                                                      .GroupBy(h => $"Năm {today.Year}")
                                                      .Select(g => new Thongke
                                                   {
                                                          Label = g.Key,
                                                          Sotien = g.Sum(h => h.TongTien ?? 0)
                                                      })
                                                      .FirstOrDefaultAsync() ?? new Thongke { Label = $"Năm {today.Year}", Sotien = 0 };

                // Tính năm trước
                var previousYear = today.Year - 1;

                // Doanh thu năm trước
                var previousYearRevenue = await context.Hoadons
                                                       .Where(h => h.NgayTao.Value.Year == previousYear)
                                                       .GroupBy(h => $"Năm {previousYear}")
                                                       .Select(g => g.Sum(h => h.TongTien ?? 0))
                                                       .FirstOrDefaultAsync();
                if (previousYearRevenue == null)
                {
                    previousYearRevenue = 0;

                }

                // Tính mức thay đổi phần trăm
                decimal changePercentage = previousYearRevenue == 0
                    ? 0 // Nếu năm trước không có doanh thu, mức thay đổi là 0%.
                    : ((currentYearRevenue.Sotien - previousYearRevenue) / previousYearRevenue) * 100;

                currentYearRevenue.thaydoi = changePercentage;
                return currentYearRevenue;
            });
        }
        public async Task<Thongke> GetRevenueForYear(int year1)
        {   
            return await GetRevenueAsync(async (context) =>
            {
                var currentYearRevenue = await context.Hoadons
                                                      .Where(h => h.NgayTao.HasValue && h.NgayTao.Value.Year == year1)  // Lọc dữ liệu theo năm
                                                      .GroupBy(h => h.NgayTao.Value.Year)  // Nhóm theo năm thực tế
                                                      .Select(g => new Thongke
                                                      {
                                                          Label = $"Năm {year1}",
                                                          Sotien = g.Sum(h => h.TongTien ?? 0)
                                                      })
                                                      .FirstOrDefaultAsync()
                                                      ?? new Thongke { Label = $"Năm {year1}", Sotien = 0 };

                return currentYearRevenue;
            });
        }


        public async Task<IEnumerable<Thongke>> thongkedoanhthu(string type, int year, int month)
        {
            try
            {
                if (type.Equals("tong"))
                {
                    var taskDay = GetRevenueForToday();
                    var taskMonth = GetRevenueForMonth();
                    var taskYear = GetRevenueForYear();

                    var results = await Task.WhenAll(taskDay, taskMonth, taskYear);

                    Console.WriteLine("Day Result: " + results[0]?.Sotien);
                    Console.WriteLine("Month Result: " + results[1]?.Sotien);
                    Console.WriteLine("Year Result: " + results[2]?.Sotien);
                    return results.Where(r => r != null);
                }
                else if (type.Equals("year"))
                {
                    var monthlyRevenueTasks = Enumerable.Range(1, 12)
                        .Select(m => GetRevenueForSpecificMonth(year, m))
                        .ToArray();

                    var monthlyRevenues = await Task.WhenAll(monthlyRevenueTasks);

                    return monthlyRevenues.Select((revenue, index) => new Thongke
                    {
                        Label = $"Tháng {index + 1}",
                        Sotien = revenue
                    });
                }
                else if (type.Equals("month"))
                {
                    int daysInMonth = DateTime.DaysInMonth(year, month);

                    var dailyRevenueTasks = Enumerable.Range(1, daysInMonth)
                        .Select(d => GetRevenueForSpecificDay(year, month, d))
                        .ToArray();

                    var dailyRevenues = await Task.WhenAll(dailyRevenueTasks);

                    return dailyRevenues.Select((revenue, index) => new Thongke
                    {
                        Label = $"Ngày {index + 1}",
                        Sotien = revenue
                    });
                }
                if (type.Equals("all"))
                {
                    int currentYear = 2024;


                    List<Thongke> Lthongke = new List<Thongke>();
                    Thongke tk;
                    for (int item = currentYear - 4; item <= currentYear; ++item)
                    {
                        tk = await GetRevenueForYear(item);  // Gọi async method lấy dữ liệu doanh thu theo năm
                            Lthongke.Add(tk);
                       
                    }    
                       
                    return Lthongke;
                }

  


                return Enumerable.Empty<Thongke>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<Thongke>();
            }
        }

        private async Task<decimal> GetRevenueForSpecificMonth(int year, int month)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Hoadons
                                .Where(h => h.NgayTao.Value.Year == year && h.NgayTao.Value.Month == month)
                                .SumAsync(h => (decimal?)h.TongTien) ?? 0;
        }

        private async Task<decimal> GetRevenueForSpecificDay(int year, int month, int day)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Hoadons
                                .Where(h => h.NgayTao.Value.Year == year && h.NgayTao.Value.Month == month && h.NgayTao.Value.Day == day)
                                .SumAsync(h => (decimal?)h.TongTien) ?? 0;
        }


        public async Task<List<BestSellingProduct>> GetTop10BestSellingProductsAsync()
        {
            // Tạo một DbContext mới thông qua factory
            using (var context = _contextFactory.CreateDbContext())
            {
                var top10BestSellingProducts = await context.Chitiethoadons
                    .Where(c => c.SoLuong.HasValue)  // Lọc những bản ghi có số lượng
                    .Include(c => c.IdChitietSpNavigation)  // Nối bảng Chitietsanpham
                    .ThenInclude(c => c.MaSanphamNavigation)  // Nối bảng Sanpham từ Chitietsanpham
                    .GroupBy(c => c.IdChitietSp)  // Nhóm theo sản phẩm chi tiết
                    .Select(group => new BestSellingProduct
                    {
                        ProductId = group.Key,  // Mã sản phẩm chi tiết
                        TotalQuantitySold = group.Sum(c => c.SoLuong ?? 0),  // Tính tổng số lượng đã bán
                        ProductName = group.FirstOrDefault().IdChitietSpNavigation.MaSanphamNavigation.TenSanpham, // Lấy tên sản phẩm từ bảng Sanpham
                        ProductPrice = group.FirstOrDefault().IdChitietSpNavigation.Gia // Lấy giá từ bảng Chitietsanpham
                    })
                    .OrderByDescending(result => result.TotalQuantitySold)  // Sắp xếp theo số lượng bán giảm dần
                    .Take(10)  // Lấy top 10 sản phẩm bán chạy
                    .ToListAsync();

                return top10BestSellingProducts;
            }



        }

    }
}