using CRUDMicroservice.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDMicroservice.Infrastructure.Repositories
{
    public interface IAdvertsRepository
    {
        Task<Advert> GetAsync(int id);

        Task<List<Advert>> GetAdvertListAsync();

        Task<Advert> AddAdvertAsync(Advert advert);
    }
}