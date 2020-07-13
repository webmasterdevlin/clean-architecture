using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.interfaces;
using Infrastructure.Data.Configurations.MongoDb;
using MongoDB.Driver;
using Web.Dtos;

namespace Web.Services
{
    public class HeroRepository : IHeroRepository
    {
        private readonly IMongoCollection<Hero> _heroes;

        public HeroRepository(ITourOfHeroesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _heroes = database.GetCollection<Hero>(settings.HeroesCollectionName);
        }

        public async Task<IEnumerable<Hero>> GetAllHeroesAsync() =>
            await _heroes.Find(hero => true).ToListAsync();

        public async Task<Hero> GetHeroByIdAsync(string id) =>
            await _heroes.Find(hero => hero.HeroId == id).FirstOrDefaultAsync();

        public async Task DeleteHeroAsync(string id) =>
            await _heroes.FindOneAndDeleteAsync(hero => hero.HeroId == id);

        public async Task<Hero> CreateHeroAsync(Hero createdHero)
        {
            await _heroes.InsertOneAsync(createdHero);
            return createdHero;
        }

        public async Task UpdateHeroAsync(Hero updatedHero) =>
            await _heroes.ReplaceOneAsync(hero => hero.HeroId == updatedHero.HeroId, updatedHero);
    }
}