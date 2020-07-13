using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.MongoDb
{
    public class TourOfHeroesDatabaseSettings : ITourOfHeroesDatabaseSettings
    {
        public string HeroesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ITourOfHeroesDatabaseSettings
    {
        string HeroesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}