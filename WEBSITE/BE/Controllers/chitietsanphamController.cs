using BE.Model;
using BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChitietsanphamController : ControllerBase
    {
        private ChitietsanphamRepositoryADONET ctsanpham;

        public ChitietsanphamController()
        {
            ctsanpham = new ChitietsanphamRepositoryADONET();
        }

        [HttpGet("Searchsanpham")]
        public async Task<ActionResult<IEnumerable<ChitietSanphamWithReviews>>> Searchsanpham([FromQuery] string key)
        {
            try
            {
                var listsanpham = await ctsanpham.GETSANPHAMS(key);

                if (listsanpham == null || !listsanpham.Any())
                {
                    return Ok(new { EC = 1, Message = "Không tìm thấy sản phẩm." });
                }
                return Ok(new { EC = 0, Data = listsanpham });
            }
            catch (Exception ex)
            {
                return Ok(new { EC = 2, Message = "Lỗi server" });
            }
        }

        [HttpGet("Chitietsanpham")]
        public async Task<ActionResult<Chitietsanpham>> Chitietsanpham([FromQuery] string masanpham, int color, int size)
        {
            try
            {
                var sanpham = await ctsanpham.GETCHIIETSANPHAM(masanpham, color, size);
                if (sanpham == null)
                {
                    return Ok(new { EC = 1, Message = "Không tìm chi tiết sản phẩm" });
                }
                return Ok(new { EC = 0, Data = sanpham });
            }
            catch (Exception ex)
            {
                return Ok(new { EC = 2, Message = "Lỗi server" });
            }
        }

        [HttpGet("Getsize")]
        public async Task<ActionResult<IEnumerable<Size>>> Getsize()
        {
            try
            {
                var list = await ctsanpham.GETSIZES();

                if (list == null || !list.Any())
                {
                    return Ok(new { EC = 1, Message = "Không tìm thấy size." });
                }
                return Ok(new { EC = 0, Data = list });
            }
            catch (Exception ex)
            {
                return Ok(new { EC = 2, Message = "Lỗi server" });
            }
        }

        [HttpGet("Getcolor")]
        public async Task<ActionResult<IEnumerable<Color>>> Getcolor()
        {
            try
            {
                var listcolor = await ctsanpham.GETCOLORS();

                if (listcolor == null || !listcolor.Any())
                {
                    return Ok(new { EC = 1, Message = "Không tìm thấy sản phẩm." });
                }
                return Ok(new { EC = 0, Data = listcolor });
            }
            catch (Exception ex)
            {
                return Ok(new { EC = 2, Message = "Lỗi server" });
            }
        }
    }
}
