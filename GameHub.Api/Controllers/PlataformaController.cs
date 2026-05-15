using GameHub.Application.Dtos.Plataforma;
using GameHub.Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformaController : ControllerBase
    {
        private readonly IPlataformaService _service;

        public PlataformaController(IPlataformaService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerTodos()
        {
            var registros = await _service.ObtenerTodosAsync();

            return Ok(registros);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var registro = await _service.ObtenerPorIdAsync(id);

            return Ok(registro);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Desarrollador")]
        public async Task<IActionResult> Crear([FromBody] PlataformaCrearDto dto)
        {
            var registro = await _service.CrearAsync(dto);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = registro.Id },
                registro);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(
            int id,
            [FromBody] PlataformaEditarDto dto)
        {
            await _service.ActualizarAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.EliminarAsync(id);

            return NoContent();
        }
    }
}