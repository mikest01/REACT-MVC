using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bodega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly SoyongContext _context;

        public MovimientosController(SoyongContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarMovimiento(Movimientos movimiento)
        {
            try
            {
                var articulo = await _context.Articulos.FindAsync(movimiento.ArticuloId);
                if (articulo == null) return NotFound("Artículo no encontrado");

                if (movimiento.TipoMovimiento == "Salida")
                {
                    if (articulo.CantidadTotal < movimiento.Cantidad)
                        return BadRequest("No hay suficiente cantidad para salir");
                    articulo.CantidadTotal -= movimiento.Cantidad;
                }
                else if (movimiento.TipoMovimiento == "Entrada")
                {
                    articulo.CantidadTotal += movimiento.Cantidad;
                }
                else return BadRequest("Tipo de movimiento inválido");

                movimiento.FechaHora = DateTime.Now;
                _context.Movimientos.Add(movimiento);
                await _context.SaveChangesAsync();

                return Ok(movimiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }
    }
}
