using CRUDMicroservice.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDMicroservice.Infrastructure.Repositories.Queries
{
    public class GetAdvertQuery : IRequest<Advert>
    {
        public int Id { get; set; }

        public class GetAdvertQueryHandler : IRequestHandler<GetAdvertQuery, Advert>
        {
            private readonly IAdvertsRepository _advertsRepository;

            public GetAdvertQueryHandler(IAdvertsRepository advertsRepository)
            {
                _advertsRepository = advertsRepository ?? throw new ArgumentNullException(nameof(advertsRepository));
            }

            public async Task<Advert> Handle(GetAdvertQuery query, CancellationToken cancellationToken)
            {
                Advert advert = await _advertsRepository.GetAsync(query.Id);
                return advert;
            }
        }
    }
}
