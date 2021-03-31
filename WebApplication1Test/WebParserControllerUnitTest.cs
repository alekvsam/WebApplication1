using AngleSharp;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace WebApplication1Test
{
    public class WebParserControllerUnitTest
    {
        private readonly ILogger<WebApplication1.Controllers.WebParserController> _logger;

        [Fact]
        public async void GetNextPageUrl_EmptyUrl()
        {            
            string url = string.Empty;

            //переделать на mock
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            WebApplication1.Controllers.WebParserController controller = new WebApplication1.Controllers.WebParserController(_logger);
            string result = controller.GetNextPageUrl(document);

            Assert.Equal("", result);
        }

        [Fact]
        public async void GetNextPageUrl_FullUrl()
        {
            string url = "https://www.avito.ru/krasnodar/kvartiry/sdam/na_dlitelnyy_srok/2-komnatnye-ASgBAQICAkSSA8gQ8AeQUgFAzAgUkFk?p=1&cd=1&pmax=25000";

            //переделать на mock
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            WebApplication1.Controllers.WebParserController controller = new WebApplication1.Controllers.WebParserController(_logger);
            string result = controller.GetNextPageUrl(document);

            Assert.NotEqual("", result);
        }

        [Fact]
        public async void GetNextPageUrl_NonexistentUrl()
        {
            string url = "https://www.avito.ru/krasnodar/kvartiry/sdam/na_dlitelnyy_srok/2-komnatnye-ASgBAQICAkSSA8gQ8AeQUgFAzAgUkFk?p=22&cd=1&pmax=25000";

            //переделать на mock
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            WebApplication1.Controllers.WebParserController controller = new WebApplication1.Controllers.WebParserController(_logger);
            string result = controller.GetNextPageUrl(document);

            Assert.Equal("", result);
        }

        [Fact]
        public async void GetNextPageUrl_UrlWithoutQueryPart()
        {
            string url = "https://www.avito.ru/krasnodar/kvartiry/sdam/na_dlitelnyy_srok/2-komnatnye-ASgBAQICAkSSA8gQ8AeQUgFAzAgUkFk";

            //переделать на mock
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            WebApplication1.Controllers.WebParserController controller = new WebApplication1.Controllers.WebParserController(_logger);
            string result = controller.GetNextPageUrl(document);

            Assert.Equal("", result);
        }

        [Fact]
        public async void GetNextPageUrl_UrlWithoutPageParameterInQuery()
        {
            string url = "https://www.avito.ru/krasnodar/kvartiry/sdam/na_dlitelnyy_srok/2-komnatnye-ASgBAQICAkSSA8gQ8AeQUgFAzAgUkFk?cd=1&pmax=25000";

            //переделать на mock
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            WebApplication1.Controllers.WebParserController controller = new WebApplication1.Controllers.WebParserController(_logger);
            string result = controller.GetNextPageUrl(document);

            Assert.NotEqual("", result);
        }

        [Fact]
        public async void GetNextPageUrl_UrlWithFirstPageParameterInQuery()
        {
            string url = "https://www.avito.ru/krasnodar/kvartiry/sdam/na_dlitelnyy_srok/2-komnatnye-ASgBAQICAkSSA8gQ8AeQUgFAzAgUkFk?p=1&cd=1&pmax=25000";

            //переделать на mock
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            WebApplication1.Controllers.WebParserController controller = new WebApplication1.Controllers.WebParserController(_logger);
            string result = controller.GetNextPageUrl(document);

            Assert.NotEqual("", result);
        }

        [Fact]
        public async void GetNextPageUrl_UrlWithLastPageParameterInQuery()
        {
            string url = "https://www.avito.ru/krasnodar/kvartiry/sdam/na_dlitelnyy_srok/2-komnatnye-ASgBAQICAkSSA8gQ8AeQUgFAzAgUkFk?p=13&cd=1&pmax=25000";

            //переделать на mock
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            WebApplication1.Controllers.WebParserController controller = new WebApplication1.Controllers.WebParserController(_logger);
            string result = controller.GetNextPageUrl(document);

            Assert.Equal("", result);
        }

    }
}
