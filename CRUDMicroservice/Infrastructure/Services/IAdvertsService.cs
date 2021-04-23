using CRUDMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMicroservice.Infrastructure.Services
{
    public interface IAdvertsService
    {
        Task<Advert> GetAdvertAsync(int advertId);

        Task<List<Advert>> GetAllAdvertAsync();

        Task<Advert> AddAdvertAsync(Advert advert);

    }
}
