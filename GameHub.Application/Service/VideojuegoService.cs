using AutoMapper;
using GameHub.Application.Dtos.Videojuego;
using GameHub.Application.Interface.Repository;
using GameHub.Application.Interface.Service;
using GameHub.Domain.Entities;

namespace GameHub.Application.Service
{
    public class VideojuegoService : IVideojuegoService
    {
        private readonly IVideojuegoRepository _repository;
        private readonly IImageStorageService _imageStorageService;
        private readonly IMapper _mapper;

        public VideojuegoService(IVideojuegoRepository repository, IImageStorageService imageStorageService, IMapper mapper)
        {
            _repository = repository;
            _imageStorageService = imageStorageService;
            _mapper = mapper;
        }

        public async Task<VideojuegoDto> ActualizarAsync(int id, VideojuegoEditarDto dto, string? nuevaImagenUrl)
        {
            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new InvalidOperationException("Registro no encontrado.");

            var imagenAnterior = registro.UrlImagen;

            _mapper.Map(dto, registro);

            // Verificar si se proporciona una imagen nueva, actualizar la URL de la imagen
            if (!string.IsNullOrEmpty(nuevaImagenUrl))
                registro.UrlImagen = nuevaImagenUrl;

            try
            {
                await _repository.ActualizarAsync(registro);

                if (!string.IsNullOrEmpty(nuevaImagenUrl) && !string.IsNullOrEmpty(imagenAnterior))
                    await _imageStorageService.EliminarImagenAsync(imagenAnterior);

                return _mapper.Map<VideojuegoDto>(registro);
            }
            catch
            {
                if (!string.IsNullOrEmpty(nuevaImagenUrl))
                    await _imageStorageService.EliminarImagenAsync(nuevaImagenUrl);

                throw;
            }
        }

        public async Task<IEnumerable<VideojuegoDto>> BuscarVideojuegoDtosAsync(string valor, int pagina, int tamano)
        {
            var registros = await _repository.BuscarPaginadoAsync(valor, pagina, tamano);
            return _mapper.Map<IEnumerable<VideojuegoDto>>(registros);
        }

        public async Task EliminarAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del videojuego debe ser mayor que cero.", nameof(id));

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("Registro no encontrado.");

            await _repository.EliminarAsync(id);

            if (!string.IsNullOrEmpty(registro.UrlImagen))
            {
                await _imageStorageService.EliminarImagenAsync(registro.UrlImagen);
            }
        }

        public async Task<int> ContarAsync()
        {
            return await _repository.ContarAsync();
        }

        public async Task<int> ContarBusquedaAsync(string valor)
        {
            return await _repository.ContarBusquedaAsync(valor);
        }

        public async Task<VideojuegoDto> CrearAsync(VideojuegoCrearDto dto, string imagenUrl)
        {
            if (string.IsNullOrEmpty(imagenUrl))
                throw new InvalidOperationException("Se requiere una imagen.");

            var registro = _mapper.Map<Videojuego>(dto);
            registro.UrlImagen = imagenUrl;

            try
            {
                await _repository.CrearAsync(registro);
                return _mapper.Map<VideojuegoDto>(registro);
            }
            catch
            {
                if (!string.IsNullOrEmpty(imagenUrl))
                    await _imageStorageService.EliminarImagenAsync(imagenUrl);

                throw;
            }
        }

        public async Task<VideojuegoDto?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del videojuego debe ser mayor que cero.", nameof(id));

            var registro = await _repository.ObtenerPorIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("Registro no encontrado.");

            return _mapper.Map<VideojuegoDto>(registro);
        }

        public async Task<IEnumerable<VideojuegoDto>> ObtenerVideojuegoDtosAsync(int pagina, int tamano)
        {
            var registros = await _repository.ObtenerPaginadoAsync(pagina, tamano);
            return _mapper.Map<IEnumerable<VideojuegoDto>>(registros);
        }
    }
}