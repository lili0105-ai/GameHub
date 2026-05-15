using AutoMapper;
using GameHub.Application.Dtos.Genero;
using GameHub.Application.Interface.Repository;
using GameHub.Application.Interface.Service;
using GameHub.Domain.Entities;

namespace GameHub.Application.Service
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _repository;
        private readonly IMapper _mapper;

        public GeneroService(IGeneroRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GeneroDto>> ObtenerTodosAsync()
        {
            var registros = await _repository.ObtenerTodosAsync();

            return _mapper.Map<IEnumerable<GeneroDto>>(registros);
        }

        public async Task<IEnumerable<GeneroDto>> ObtenerPaginadoAsync(int pagina, int tamano)
        {
            if (pagina <= 0)
                throw new ArgumentException("La página debe ser mayor que cero.", nameof(pagina));

            if (tamano <= 0)
                throw new ArgumentException("El tamaño debe ser mayor que cero.", nameof(tamano));

            var registros = await _repository.ObtenerPaginadoAsync(pagina, tamano);

            return _mapper.Map<IEnumerable<GeneroDto>>(registros);
        }

        public async Task<IEnumerable<GeneroDto>> BuscarPaginadoAsync(string valor, int pagina, int tamano)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("El valor de búsqueda es requerido.", nameof(valor));

            var registros = await _repository.BuscarPaginadoAsync(valor, pagina, tamano);

            return _mapper.Map<IEnumerable<GeneroDto>>(registros);
        }

        public async Task<int> ContarAsync()
        {
            return await _repository.ContarAsync();
        }

        public async Task<int> ContarBusquedaAsync(string valor)
        {
            return await _repository.ContarBusquedaAsync(valor);
        }

        public async Task<GeneroDto> CrearAsync(GeneroCrearDto dto)
        {
            var registro = _mapper.Map<Genero>(dto);

            await _repository.CrearAsync(registro);

            return _mapper.Map<GeneroDto>(registro);
        }

        public async Task<GeneroDto?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

            var registro = await _repository.ObtenerPorIdAsync(id);

            if (registro == null)
                throw new KeyNotFoundException("Registro no encontrado.");

            return _mapper.Map<GeneroDto>(registro);
        }

        public async Task ActualizarAsync(int id, GeneroEditarDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

            var registro = await _repository.ObtenerPorIdAsync(id);

            if (registro == null)
                throw new KeyNotFoundException("Registro no encontrado.");

            _mapper.Map(dto, registro);

            await _repository.ActualizarAsync(registro);
        }

        public async Task EliminarAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

            var registro = await _repository.ObtenerPorIdAsync(id);

            if (registro == null)
                throw new KeyNotFoundException("Registro no encontrado.");

            await _repository.EliminarAsync(registro);
        }
    }
}