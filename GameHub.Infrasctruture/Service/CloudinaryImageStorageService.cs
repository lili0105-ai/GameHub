using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GameHub.Application.Interface.Service;

namespace GameHub.Infrastructure.Service
{
    public class CloudinaryImageStorageService : IImageStorageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryImageStorageService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task EliminarImagenAsync(string imageUri)
        {
            if (string.IsNullOrEmpty(imageUri)) return;

            // Extraer el public_id de la URL
            var uri = new Uri(imageUri);
            var segments = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);

            var publicId = string.Join("/", segments.Skip(segments.Length - 2));
            publicId = Path.Combine(
                Path.GetDirectoryName(publicId) ?? string.Empty,
                Path.GetFileNameWithoutExtension(publicId)
            ).Replace("\\", "/");

            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image
            };

            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.Result != "ok" && result.Result != "not found")
                throw new InvalidOperationException($"No se pudo eliminar la imagen: {result.Result}");
        }

        public async Task<string> SubirImagenAsync(Stream fileStream, string fileName, string contentType, string folder)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, fileStream),
                Folder = folder,
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
                throw new InvalidOperationException(result.Error.Message);

            return result.SecureUrl.ToString();
        }
    }
}