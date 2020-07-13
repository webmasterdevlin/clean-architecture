using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.interfaces
{
    public interface IHeroRepository
    {
        Task<IEnumerable<Hero>> GetAllHeroesAsync();

        Task<Hero> GetHeroByIdAsync(string id);

        Task DeleteHeroAsync(string id);

        Task<Hero> CreateHeroAsync(Hero createdHero);

        Task UpdateHeroAsync(Hero updatedHero);
    }
}