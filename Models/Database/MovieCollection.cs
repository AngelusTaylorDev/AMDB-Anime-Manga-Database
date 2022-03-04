namespace AMDB_Anime_Manga_Database.Models.Database
{
    public class MovieCollection
    {
        // Primary key
        public int Id { get; set; }

        // Foreign keys
        public int CollectionId { get; set; }
        public int MovieId { get; set; }

        // Order filter
        public int Order { get; set; }

        // Order filter
        public Collection Collection { get; set; }

        // Navigation Property
        public Movie Movie { get; set; }
    }
}
