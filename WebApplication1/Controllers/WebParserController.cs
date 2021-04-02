using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Web;

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

        private async Task CheckForUpdates(string url)
        {
            List<dynamic> adverts = new List<dynamic>();
            await GetPageContent(url, adverts);

            using (var db = new AdvertContext())
            {
                foreach (Advert advert in adverts)
                {
                    // We check if the advert exists in the database
                    var existingadvert = db.Adverts.SingleOrDefault(row => row.Url == advert.Url);
                    if (existingadvert != null) continue; 

                    db.Adverts.Add(advert);
                    await db.SaveChangesAsync();
                }
            }

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

            //TODO:Расконкурентить получение данных постранично
            var nextPageUrl = GetNextPageUrl(document);
            if (!String.IsNullOrEmpty(nextPageUrl))
            {
#if DEBUG
                _logger.LogInformation($"переходим на следующу страницу {nextPageUrl}");
#endif
                return await GetPageContent(nextPageUrl, results);
            }

            return results;
        }

        /// <summary>
        /// Определяет текущуюю страницу из параметра внутри DocumentElement.BaseUrl.Query
        /// Получает ссылку на следующую страницу из пагинатора
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Возвращает ссылку на следующую страницу. String.Empty если страница последняя или единственная.</returns>
        public string GetNextPageUrl(AngleSharp.Dom.IDocument document)
        {
            string nextPageUrl = "";

            if (document?.BaseUrl?.Query == null) return nextPageUrl;

            var nextPageButton = document.QuerySelector("span[data-marker='pagination-button/next']");
            //проверяем наличие кнопки "След." на странице
            if (nextPageButton == null) return nextPageUrl;

            //т.к. в самой кнопке "След." url не хранится нужно лезть скрытый пагинатор с ссылками
            //сначала проверяем текущий query в url на наличие параметра текущей страницы, если нет по умолчанию 1
            ushort currentPageId = ushort.TryParse(
                HttpUtility.ParseQueryString(document.DocumentElement.BaseUrl.Query).Get("p"),
                out currentPageId) ? currentPageId : (ushort)1;

            //находим в скрытом пагинаторе ссылку на следующую страницу
            var pageButton = document.QuerySelectorAll("a.pagination-page").Where(e => e.InnerHtml == $"{currentPageId + 1}").FirstOrDefault();
            if (pageButton != null)
            {
                nextPageUrl = document.Origin + pageButton.GetAttribute("href");
            }

            return nextPageUrl;
        }
    }
}
