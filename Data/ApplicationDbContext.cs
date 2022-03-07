using AMDB_Anime_Manga_Database.Models.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AMDB_Anime_Manga_Database.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Creating the DB Set Collection
        public DbSet<Collection> Collection { get; set; }
    }
}
