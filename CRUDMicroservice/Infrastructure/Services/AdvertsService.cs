using CRUDMicroservice.Infrastructure.Repositories;
using CRUDMicroservice.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMicroservice.Infrastructure.Services
{
    public class AdvertsService : IAdvertsService
    {
        private readonly IAdvertsRepository _advertsRepository;
        private readonly ILogger<AdvertsService> _logger;

        public AdvertsService(IAdvertsRepository advertsRepository, ILogger<AdvertsService> logger)
        {
            _advertsRepository = advertsRepository ?? throw new ArgumentNullException(nameof(advertsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Advert> GetAdvertAsync(int advertId)
        {
            return await _advertsRepository.GetAsync(advertId);
        }

        public async Task<List<Advert>> GetAllAdvertAsync()
        {
            return await _advertsRepository.GetAdvertListAsync();
        }

        public async Task<Advert> AddAdvertAsync(Advert advert)
        {
            return await _advertsRepository.AddAdvertAsync(advert);
        }
    }
}