using CRUDMicroservice.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDMicroservice.Infrastructure.Repositories.Queries
{
    public class GetAllAdvertQuery : IRequest<IEnumerable<Advert>>
    {
        public class GetAllAdvertQueryHandler : IRequestHandler<GetAllAdvertQuery, IEnumerable<Advert>>
        {
            private readonly IAdvertsRepository _advertsRepository;

            public GetAllAdvertQueryHandler(IAdvertsRepository advertsRepository)
            {
                _advertsRepository = advertsRepository ?? throw new ArgumentNullException(nameof(advertsRepository));
            }

            public async Task<IEnumerable<Advert>> Handle(GetAllAdvertQuery query, CancellationToken cancellationToken)
            {
                IEnumerable<Advert> adverts = await _advertsRepository.GetAdvertListAsync();
                return adverts;
            }
        }
    }
}
