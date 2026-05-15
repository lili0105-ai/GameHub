using AutoMapper;
using GameHub.Application.Dtos.Plataforma;
using GameHub.Application.Interface.Repository;
using GameHub.Application.Interface.Service;
using GameHub.Domain.Entities;

namespace GameHub.Application.Service
{
    public class PlataformaService : IPlataformaService
    {
        private readonly IPlataformaRepository _repository;
        private readonly IMapper _mapper;

        public PlataformaService(IPlataformaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlataformaDto>> ObtenerTodosAsync()
        {
            var registros = await _repository.ObtenerTodosAsync();

            return _mapper.Map<IEnumerable<PlataformaDto>>(registros);
        }

        public async Task<PlataformaDto?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

            var registro = await _repository.ObtenerPorIdAsync(id);

            if (registro == null)
                throw new KeyNotFoundException("Registro no encontrado.");

            return _mapper.Map<PlataformaDto>(registro);
        }

        public async Task<PlataformaDto> CrearAsync(PlataformaCrearDto dto)
        {
            var registro = _mapper.Map<Plataforma>(dto);

            await _repository.CrearAsync(registro);

            return _mapper.Map<PlataformaDto>(registro);
        }

        public async Task ActualizarAsync(int id, PlataformaEditarDto dto)
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