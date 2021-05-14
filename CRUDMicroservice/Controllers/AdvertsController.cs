using CRUDMicroservice.Infrastructure.Repositories.Commands;
using CRUDMicroservice.Infrastructure.Repositories.Queries;
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
      
        public AdvertsController(IMediator mediator)
        {         
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("GetAll")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Advert>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Advert>>> GetAllAdvertAsync(CancellationToken token)
        {
            var adverts = await _mediator.Send(new GetAllAdvertQuery(), token);
            return Ok(adverts);
        }


        [Route("GetById")]
        [HttpGet]
        [ProducesResponseType(typeof(Advert), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [ActionName("GetAdvertAsync")]
        public async Task<ActionResult<Advert>> GetAdvertAsync([FromQuery] GetAdvertQuery query, CancellationToken token)
        {
            var advert = await _mediator.Send(query, token);
            if (advert == null) 
            {
                return NotFound();
            }
            return Ok(advert);
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
        public async Task<IActionResult> Post([FromBody] AddAdvertCommand command, CancellationToken token)
        {
            Advert entity = await _mediator.Send(command, token);
            return CreatedAtAction(nameof(AdvertsController.GetAdvertAsync), new { id = entity.AdvertId }, entity);
        }
    }
}
