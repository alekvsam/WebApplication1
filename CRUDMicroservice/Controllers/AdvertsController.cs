using CRUDMicroservice.Infrastructure.Repositories.Commands;
using CRUDMicroservice.Infrastructure.Services;
using CRUDMicroservice.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDMicroservice.Controllers
{
    public class AdvertsController : ControllerBase
    {
        private readonly IAdvertsService _advertsService;
        private readonly IMediator _mediator;

        //ASQ: Как ConfigureServices разруливает ситуацию с разными параметрами в конструкторе?
        public AdvertsController(IAdvertsService advertsService, IMediator mediator)
        {
            _advertsService = advertsService ?? throw new ArgumentNullException(nameof(advertsService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("GetAll")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Advert>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Advert>>> GetAllAdvertAsync()
        {
            return Ok(await  _advertsService.GetAllAdvertAsync());
        }


        [Route("GetById")]
        [HttpGet]
        [ProducesResponseType(typeof(Advert), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [ActionName("GetAdvertAsync")]
        public async Task<ActionResult<Advert>> GetAdvertAsync(int id)
        {
            return Ok(await _advertsService.GetAdvertAsync(id));
        }

        /// <summary>
        ///  Создание объявления
        /// </summary>
        /// <param name="client"></param>
        /// <param name="apiVersion"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("AddAdvert")]
        [HttpPost]
        [ProducesResponseType(typeof(Advert), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post([FromBody] AddAdvertCommand client, CancellationToken token)
        {
            Advert entity = await _mediator.Send(client, token);
            return CreatedAtAction(nameof(AdvertsController.GetAdvertAsync), new { id = entity.AdvertId }, entity);
        }
    }
}
