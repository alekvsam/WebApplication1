using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{    
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WebParserController : ControllerBase
    {
        private readonly AdvertContext _context;

        // Constructor
        public WebParserController(AdvertContext context)
        {
            _context = context;
        }

        /// <summary>
        /// По заданным критериям открывает и парсит страницу с объявлениями и заносит результаты в базу
        /// </summary>
        /// <param name="model"></param>
        /// <returns>URL сформированный по критериям</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddAvitoRentAdverts([FromBody] RentURLFilterModel model)
        {
            var adverts = await HTMLParser.ParseAdvertsFromURL(model.ToURLString());
            foreach (AdvertModel advert in adverts)
            {
                var existingadvert = _context.Adverts.SingleOrDefault(row => row.Url == advert.Url);
                if (existingadvert != null) continue;

                _context.Adverts.Add(advert);
                await _context.SaveChangesAsync();
            }

            return Ok(model.ToURLString());
        }
    }
}
