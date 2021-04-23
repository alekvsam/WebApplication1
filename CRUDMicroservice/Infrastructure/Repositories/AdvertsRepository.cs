using CRUDMicroservice.Data;
using CRUDMicroservice.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMicroservice.Infrastructure.Repositories
{
    public class AdvertsRepository : IAdvertsRepository
    {
        private readonly AdvertsContext _context;

        public AdvertsRepository(IOptions<CosmosDbSettings> settings)
        {
            _context = new AdvertsContext(settings);
        }

        public async Task<List<Advert>> GetAdvertListAsync()
        {
            return await _context.Adverts.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Advert> GetAsync(int id)
        {
            var filter = Builders<Advert>.Filter.Eq("Id", id);
            return await _context.Adverts
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task<Advert> AddAdvertAsync(Advert advert)
        {
            await _context.Adverts.InsertOneAsync(advert);
            return advert;
        }
    }
}
