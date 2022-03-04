namespace AMDB_Anime_Manga_Database.Models.Database
{
    public class TVShowCast
    {
        // Primary key
        public int Id { get; set; }

        // Foreign key
        public int TVShowId { get; set; }

        // Descriptive 
        // Storing the cast ID
        public int CastID { get; set; }

        // Storing the cast Department
        public string Department { get; set; }

        // Storing the cast Member Name
        public string Name { get; set; }

        // Storing the Character Played
        public string Character { get; set; }

        // Storing the cast Member profile Image
        public string ImageUrl { get; set; }

        // Navigation Property
        // Storing the cast Member movie acted in.
        public TVShow TVShow { get; set; }
    }
}
