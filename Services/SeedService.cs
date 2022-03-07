using AMDB_Anime_Manga_Database.Data;
using AMDB_Anime_Manga_Database.Models.Database;
using AMDB_Anime_Manga_Database.Models.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace AMDB_Anime_Manga_Database.Services
{
    public class SeedService
    {
        // Dependency injection
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedService(IOptions<AppSettings> appSettings, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task ManageDataAsync()
        {
            await UpdateDatabaseAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedCollections();
        }

        private async Task UpdateDatabaseAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (_dbContext.Roles.Any()) return;
            var adminRole = _appSettings.AMDBSettings.DefaultCredentials.Role;

            await _roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        private async Task SeedUsersAsync()
        {
            if (_userManager.Users.Any()) return;

            var credentials = _appSettings.AMDBSettings.DefaultCredentials;
            var defaultAdminUser = new IdentityUser()
            {
                Email = credentials.Email,
                UserName = credentials.Email,
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(defaultAdminUser, credentials.Password);
            await _userManager.AddToRoleAsync(defaultAdminUser, credentials.Role);
        }

        private async Task SeedCollections()
        {
            if (_dbContext.Collection.Any()) return;

            _dbContext.Add(new Collection()
            {
                Name = _appSettings.AMDBSettings.DefaultCollection.Name,
                Description = _appSettings.AMDBSettings.DefaultCollection.Description
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
