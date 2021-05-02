using CRUDMicroservice.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDMicroservice.Infrastructure.Repositories.Commands
{
    /// <summary>
    /// Команда на добавление нового объявления.
    /// </summary>
    public class AddAdvertCommand : IRequest<Advert>
    {
        /// <summary>
        /// Название объявления
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Ссылка на объявление
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Цена объявления
        /// </summary>
        public float Price { get; set; }

        public class AddProductCommandHandler : IRequestHandler<AddAdvertCommand, Advert>
        {
            private readonly IAdvertsRepository _advertsRepository;

            public AddProductCommandHandler(IAdvertsRepository advertsRepository)
            {
                _advertsRepository = advertsRepository ?? throw new ArgumentNullException(nameof(advertsRepository));
            }

            public async Task<Advert> Handle(AddAdvertCommand command, CancellationToken cancellationToken)
            {
                Advert product = new Advert()
                {
                    Title = command.Title,
                    Url = command.Url,
                    Price = command.Price
                };

                await _advertsRepository.AddAdvertAsync(product);
                return product;
            }
        }
    }
}
