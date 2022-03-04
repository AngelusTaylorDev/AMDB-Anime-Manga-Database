using System.Collections.Generic;

namespace AMDB_Anime_Manga_Database.Models.Database
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigational Properties
        public ICollection<MovieCollection> Collections { get; set; } = new HashSet<MovieCollection>();
        public ICollection<TVShowCollection> ShowCollections { get; set; } = new HashSet<TVShowCollection>();
    }
}
