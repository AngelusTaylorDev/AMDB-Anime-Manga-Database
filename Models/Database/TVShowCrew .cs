namespace AMDB_Anime_Manga_Database.Models.Database
{
    public class TVShowCrew
    {
        public int Id { get; set; }
        public int TVShowId { get; set; }

        public int CrewID { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public string ImageUrl { get; set; }

        public TVShow TVShow { get; set; }
    }
}
