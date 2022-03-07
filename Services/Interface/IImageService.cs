using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AMDB_Anime_Manga_Database.Services.Interface
{
    public interface IImageService
    {
        // Encoding the image IFormfile to a Byte array
        Task<byte[]> EncodeImageAsync(IFormFile poster);

        // Encoding the image url string to a Byte array and store it 
        Task<byte[]> EncodeImageURLAsync(string imageURL);

        // Decode a byte array to a string and display our image file
        string DecodeImage(byte[] poster, string contentType);
    }
}
