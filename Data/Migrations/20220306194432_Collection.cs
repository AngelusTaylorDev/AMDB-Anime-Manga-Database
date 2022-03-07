using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AMDB_Anime_Manga_Database.Data.Migrations
{
    public partial class Collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    TagLine = table.Column<string>(type: "text", nullable: true),
                    OverView = table.Column<string>(type: "text", nullable: true),
                    Runtime = table.Column<int>(type: "integer", nullable: false),
                    ReleseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    ReviewAverage = table.Column<float>(type: "real", nullable: false),
                    Poster = table.Column<byte[]>(type: "bytea", nullable: true),
                    PosterType = table.Column<string>(type: "text", nullable: true),
                    Backdrop = table.Column<byte[]>(type: "bytea", nullable: true),
                    BackDropType = table.Column<string>(type: "text", nullable: true),
                    TrailerUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TVShow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TVShowId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    TagLine = table.Column<string>(type: "text", nullable: true),
                    OverView = table.Column<string>(type: "text", nullable: true),
                    number_of_episodes = table.Column<int>(type: "integer", nullable: false),
                    number_of_seasons = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true),
                    ReleseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    ReviewAverage = table.Column<float>(type: "real", nullable: false),
                    Poster = table.Column<byte[]>(type: "bytea", nullable: true),
                    PosterType = table.Column<string>(type: "text", nullable: true),
                    Backdrop = table.Column<byte[]>(type: "bytea", nullable: true),
                    BackDropType = table.Column<string>(type: "text", nullable: true),
                    TrailerUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieCast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    CastID = table.Column<int>(type: "integer", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Character = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCast", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieCast_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CollectionId = table.Column<int>(type: "integer", nullable: false),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieCollection_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCollection_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieCrew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    CrewID = table.Column<int>(type: "integer", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Job = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCrew", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieCrew_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TVShowCast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TVShowId = table.Column<int>(type: "integer", nullable: false),
                    CastID = table.Column<int>(type: "integer", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Character = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShowCast", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TVShowCast_TVShow_TVShowId",
                        column: x => x.TVShowId,
                        principalTable: "TVShow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TVShowCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CollectionId = table.Column<int>(type: "integer", nullable: false),
                    TVShowId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShowCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TVShowCollection_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TVShowCollection_TVShow_TVShowId",
                        column: x => x.TVShowId,
                        principalTable: "TVShow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TVShowCrew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TVShowId = table.Column<int>(type: "integer", nullable: false),
                    CrewID = table.Column<int>(type: "integer", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Job = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShowCrew", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TVShowCrew_TVShow_TVShowId",
                        column: x => x.TVShowId,
                        principalTable: "TVShow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCast_MovieId",
                table: "MovieCast",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCollection_CollectionId",
                table: "MovieCollection",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCollection_MovieId",
                table: "MovieCollection",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCrew_MovieId",
                table: "MovieCrew",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_TVShowCast_TVShowId",
                table: "TVShowCast",
                column: "TVShowId");

            migrationBuilder.CreateIndex(
                name: "IX_TVShowCollection_CollectionId",
                table: "TVShowCollection",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TVShowCollection_TVShowId",
                table: "TVShowCollection",
                column: "TVShowId");

            migrationBuilder.CreateIndex(
                name: "IX_TVShowCrew_TVShowId",
                table: "TVShowCrew",
                column: "TVShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCast");

            migrationBuilder.DropTable(
                name: "MovieCollection");

            migrationBuilder.DropTable(
                name: "MovieCrew");

            migrationBuilder.DropTable(
                name: "TVShowCast");

            migrationBuilder.DropTable(
                name: "TVShowCollection");

            migrationBuilder.DropTable(
                name: "TVShowCrew");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "TVShow");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
