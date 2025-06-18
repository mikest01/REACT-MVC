using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bodega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
       
        public class ArticulosController : ControllerBase
        {
            private readonly SoyongContext _context;

            public ArticulosController(SoyongContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> GetArticulos()
            {
                var articulos = await _context.Articulos.ToListAsync();
                return Ok(articulos);
            }
        }
    }

