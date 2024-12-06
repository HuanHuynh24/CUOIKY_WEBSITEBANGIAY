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
                return await context.Hoadons
                                    .Where(h => h.NgayTao.Value.Date == today)
                                    .GroupBy(h => today.ToString("yyyy/MM/dd"))
                                    .Select(g => new Thongke
                                    {
                                        Label = g.Key,
                                        Sotien = g.Sum(h => h.TongTien ?? 0)
                                    })
                                    .FirstOrDefaultAsync();
            });
        }

        public async Task<Thongke> GetRevenueForMonth()
        {
            return await GetRevenueAsync(async (context) =>
            {
                var today = DateTime.Today;
                return await context.Hoadons
                                    .Where(h => h.NgayTao.Value.Month == today.Month && h.NgayTao.Value.Year == today.Year)
                                    .GroupBy(h => $"Tháng {today.Month}")
                                    .Select(g => new Thongke
                                    {
                                        Label = g.Key,
                                        Sotien = g.Sum(h => h.TongTien ?? 0)
                                    })
                                    .FirstOrDefaultAsync();
            });
        }

        public async Task<Thongke> GetRevenueForYear()
        {
            return await GetRevenueAsync(async (context) =>
            {
                var today = DateTime.Today;
                return await context.Hoadons
                                    .Where(h => h.NgayTao.Value.Year == today.Year)
                                    .GroupBy(h => $"Năm {today.Year}")
                                    .Select(g => new Thongke
                                    {
                                        Label = g.Key,
                                        Sotien = g.Sum(h => h.TongTien ?? 0)
                                    })
                                    .FirstOrDefaultAsync();
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
    }
}
