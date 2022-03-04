using AMDB_Anime_Manga_Database.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMDB_Anime_Manga_Database.Models.Database
{
    public class TVShow
    {
        public int Id { get; set; }
        public int TVShowId { get; set; }
        public string Title { get; set; }
        public string TagLine { get; set; }
        public string OverView { get; set; }
        public int number_of_episodes { get; set; }
        public int number_of_seasons { get; set; }
        public string type { get; set; }

        // Date
        [DataType(DataType.Date)]
        [Display(Name = "Relese Date")]
        public DateTime ReleseDate { get; set; }

        // Enums
        public ESRBRating Rating { get; set; }

        public float ReviewAverage { get; set; }

        // Image - Poster
        public byte[] Poster { get; set; }
        public string PosterType { get; set; }

        // Image - BackDrop
        public byte[] Backdrop { get; set; }
        public string BackDropType { get; set; }

        // Trailer
        public string TrailerUrl { get; set; }

        // IForm File for Poster Image
        [NotMapped]
        [Display(Name = "Poster Image")]
        public IFormFile PosterFile { get; set; }

        // IForm File for Poster Image
        [NotMapped]
        [Display(Name = "Backdrop Image")]
        public IFormFile BackdropFile { get; set; }

        // Navigational Properties
        public ICollection<TVShowCollection> Collections { get; set; } = new HashSet<TVShowCollection>();
        public ICollection<TVShowCast> Cast { get; set; } = new HashSet<TVShowCast>();
        public ICollection<TVShowCrew> Crew { get; set; } = new HashSet<TVShowCrew>();
    }
}
