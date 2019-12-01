using DemoRefit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRefit.Repositories
{
    public interface ICountryRepositoryClient
    {
        Task<IEnumerable<Country>> GetAsync();
    }
}