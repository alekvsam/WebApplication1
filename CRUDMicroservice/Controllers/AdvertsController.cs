using CRUDMicroservice.Infrastructure.Services;
using CRUDMicroservice.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CRUDMicroservice.Controllers
{
    public class AdvertsController : ControllerBase
    {
        private readonly IAdvertsService _advertsService;

        public AdvertsController(IAdvertsService advertsService)
        {
            _advertsService = advertsService ?? throw new ArgumentNullException(nameof(advertsService));
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Advert>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Advert>>> GetAllAdvertAsync()
        {
            return await _advertsService.GetAllAdvertAsync();
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<Advert>> Create(Advert book)
        {
            return await _advertsService.AddAdvertAsync(book);
        }
    }
}
