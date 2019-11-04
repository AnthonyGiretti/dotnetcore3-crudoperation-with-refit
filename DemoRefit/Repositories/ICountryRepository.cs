using DemoRefit.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoRefit.Repositories
{
    public interface ICountryRepository
    {
        [Get("/api/democrud")]
        Task<IEnumerable<Country>> GetAsync();

        [Get("/api/democrud/{id}")]
        Task<Country> GetAsync(int id);

        [Post("/api/democrud/create")]
        Task CreateAsync([Body] Country country);

        [Put("/api/democrud/update/{id}")]
        Task ReplaceAsync(int id, [Body] Country country);

        [Patch("/api/democrud/update/{id}/description")]
        Task UpdateAsync(int id, [Body]string description);

        [Delete("/api/democrud/delete/{id}")]
        Task DeleteAsync(int id);
    }
}