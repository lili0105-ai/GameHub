
namespace GameHub.Application.Interface.Service
{
     public interface IImageStorageService
    {
        Task EliminarImagenAsync(string imageUri);

        Task<string> SubirImagenAsync(Stream fileStream, string fileName, string contentType, string folder);
    }
}
