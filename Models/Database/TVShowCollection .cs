namespace AMDB_Anime_Manga_Database.Models.Database
{
    public class TVShowCollection
    {
        // Primary key
        public int Id { get; set; }

        // Foreign keys
        public int CollectionId { get; set; }
        public int TVShowId { get; set; }

        // Order filter
        public int Order { get; set; }

        // Order filter
        public Collection Collection { get; set; }

        // Navigation Property
        public TVShow TVShow { get; set; }
    }
}
