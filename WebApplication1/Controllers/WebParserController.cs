using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class WebParserController : ControllerBase
    {
        private readonly ILogger<WebParserController> _logger;
        
        // Constructor
        public WebParserController(ILogger<WebParserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            CheckForUpdates("https://www.avito.ru/krasnodar/kvartiry/sdam/na_dlitelnyy_srok/2-komnatnye-ASgBAQICAkSSA8gQ8AeQUgFAzAgUkFk?cd=1&pmax=25000");
            return "Success";
        }

        private async void CheckForUpdates(string url)
        {
            List<dynamic> ads = new List<dynamic>();
            await GetPageContent(url, ads);

        }

        private async Task<List<dynamic>> GetPageContent(string url, List<dynamic> results)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            var advertItems = document.QuerySelectorAll("div[data-marker='item']");

            foreach (var item in advertItems)
            {
                Advert advert = new Advert();

                int.TryParse(item.GetAttribute("data-item-id"), out int id);
                advert.Id = id;

                float.TryParse(item.QuerySelector("meta[itemprop='price']").GetAttribute("content"), out float price);
                advert.Price = price;
                
                advert.Url = document.Origin + item.QuerySelector("a[itemprop='url']").GetAttribute("href");

#if DEBUG
                _logger.LogInformation($"the id of ads is {advert.Id}" +
                    $" and price is {advert.Price}" +
                    $" and url is {advert.Url}");
#endif

                results.Add(advert);
            }
            return results;
        }
    }
}
