using AutoMapper;
using GameHub.Api.Request.Videojuego;
using GameHub.Application.Dtos.Videojuego;
using GameHub.Application.Interface.Service;
using GameHub.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideojuegoController : ControllerBase
    {
        private readonly IVideojuegoService _service;
        private readonly IImageStorageService _imageService;
        private readonly IMapper _mapper;

        public VideojuegoController(IVideojuegoService service, IImageStorageService imageService, IMapper mapper)
        {
            _service = service;
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaPaginada<VideojuegoDto>>> ObtenerTodos([FromQuery] int pagina = 1, [FromQuery] int tamano = 10)
        {
            var registros = await _service.ObtenerVideojuegoDtosAsync(pagina, tamano);
            var total = await _service.ContarAsync();

            return Ok(new RespuestaPaginada<VideojuegoDto>(registros, total, pagina, tamano));
        }

        [HttpGet("buscar")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaPaginada<VideojuegoDto>>> Buscar([FromQuery] string valor, [FromQuery] int pagina = 1, [FromQuery] int tamano = 10)
        {
            var registros = await _service.BuscarVideojuegoDtosAsync(valor, pagina, tamano);
            var total = await _service.ContarBusquedaAsync(valor);

            return Ok(new RespuestaPaginada<VideojuegoDto>(registros, total, pagina, tamano));
        }

        [HttpGet("{id:int}", Name = "ObtenerVideojuego")]
        [AllowAnonymous]
        public async Task<ActionResult<VideojuegoDto>> ObtenerPorId(int id)
        {
            var registro = await _service.ObtenerPorIdAsync(id);

            if (registro == null)
                return NotFound();

            return Ok(registro);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Desarrollador")]
        public async Task<ActionResult<VideojuegoDto>> Crear([FromForm] VideojuegoCrearRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(usuarioId))
                return Unauthorized("Usuario no autenticado.");

            // Subida a Cloudinary 
            var imagenUrl = await _imageService.SubirImagenAsync(
                request.UrlImagen.OpenReadStream(),
                request.UrlImagen.FileName,
                request.UrlImagen.ContentType,
                folder: "videojuegos"
            );

            // Mapeo manual del Request al DTO de creación
            var dato = new VideojuegoCrearDto
            {
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Precio = request.Precio,
                FechaLanzamiento = request.FechaLanzamiento,
                GeneroId = request.GeneroId,
                UserId = usuarioId
            };

            var registroCreado = await _service.CrearAsync(dato, imagenUrl);

            return CreatedAtRoute("ObtenerVideojuego", new { id = registroCreado.Id }, registroCreado);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Desarrollador")]
        public async Task<ActionResult<VideojuegoDto>> Editar(int id, [FromForm] VideojuegoEditarRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? nuevaImagenUrl = null;

            if (request.UrlImagen != null)
            {
                nuevaImagenUrl = await _imageService.SubirImagenAsync(
                    request.UrlImagen.OpenReadStream(),
                    request.UrlImagen.FileName,
                    request.UrlImagen.ContentType,
                    folder: "videojuegos"
                );
            }

            var dato = new VideojuegoEditarDto
            {
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Precio = request.Precio,
                FechaLanzamiento = request.FechaLanzamiento,
                GeneroId = request.GeneroId,
                DesarrolladorId = request.DesarrolladorId
            };

            var registroActualizado = await _service.ActualizarAsync(id, dato, nuevaImagenUrl);

            return Ok(registroActualizado);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Desarrollador")]
        public async Task<ActionResult> Eliminar(int id)
        {
            await _service.EliminarAsync(id);

            return NoContent();
        }
    }
}