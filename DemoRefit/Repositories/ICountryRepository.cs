using DemoRefit.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Task<Country> PostAsync([Body] Country country);

        [Put("/api/democrud/update/{id}")]
        Task<Country> PutAsync(int id, [Body] Country country);

        [Patch("/api/democrud/update/{id}/description")]
        Task<Country> PatchAsync(int id, [Body]string description);

        [Delete("/api/democrud/delete/{id}")]
        Task<Country> DeleteAsync(int id);
    }
}