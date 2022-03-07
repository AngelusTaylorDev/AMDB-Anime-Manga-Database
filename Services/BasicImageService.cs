using AMDB_Anime_Manga_Database.Services.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AMDB_Anime_Manga_Database.Services
{
    public class BasicImageService : IImageService
    {
        // Injecting the IHttpC lient Factory 
        private readonly IHttpClientFactory _httpClient;

        public BasicImageService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        // Decoding the Image
        public string DecodeImage(byte[] poster, string contentType)
        {
            if (poster == null) return null;
            var posterImage = Convert.ToBase64String(poster);
            return $"data:{contentType};base64,{posterImage}";
        }

        // Encoding the Image from Iformfile
        public async Task<byte[]> EncodeImageAsync(IFormFile poster)
        {
            if (poster == null) return null;

            using var ms = new MemoryStream();
            await poster.CopyToAsync(ms);
            return ms.ToArray();
        }

        // Encoding the Image URL
        public async Task<byte[]> EncodeImageURLAsync(string imageURL)
        {
            var client = _httpClient.CreateClient();
            var response = await client.GetAsync(imageURL);
            using Stream stream = await response.Content.ReadAsStreamAsync();

            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}
