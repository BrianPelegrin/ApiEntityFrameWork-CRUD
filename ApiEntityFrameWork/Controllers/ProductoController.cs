using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEntityFrameWork.Models;

namespace ApiEntityFrameWork.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly MarketContext _context;

        public ProductoController(MarketContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                lista = _context.Productos.Include(c => c.oCategoria).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpGet]
        [Route("Obtener/{IdProducto:int}")]
        public IActionResult Obtener(int? IdProducto) 
        {
            if(IdProducto == null) 
            {
                return BadRequest("Producto no encontrado");
            }

            try 
            {
                Producto? producto = _context.Productos.Include(c => c.oCategoria).Where(m=> m.IdProducto == IdProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = producto });
            }catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }
            

            

        }
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Producto producto) 
        {
            try 
            {
                await _context.Productos.AddAsync(producto);
                await _context.SaveChangesAsync();
                return  StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto Guardado" });

            }catch (Exception ex) {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        } 

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Producto? producto) 
        {
            
            if(producto == null)
            {
                return BadRequest();
            }
            var oProducto = await _context.Productos.FindAsync(producto.IdProducto);
            if(oProducto == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Productos.Update(oProducto);
                return StatusCode(StatusCodes.Status200OK);

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", responde = ex.Message });
            }
        }


    }
}
