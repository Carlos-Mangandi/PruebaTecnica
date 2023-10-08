using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PruebaCRUD.EN;
using PruebaCRUD.BL;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace PruebaCRUD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolController : ControllerBase
    {
        private RolBL rolBL = new RolBL();

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Rol>> Get()
        {
            return await rolBL.ObtenerTodos();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Rol> Get(int id)
        {
            Rol rol = new Rol();
            rol.Id = id;
            return await rolBL.ObtenerPorId(rol);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Rol rol)
        {
            try
            {
                await rolBL.Crear(rol);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Rol rol)
        {
            if (rol.Id == id)
            {
                await rolBL.Modificar(rol);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Rol rol = new Rol();
                rol.Id = id;
                await rolBL.Eliminar(rol);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        [AllowAnonymous]
        public async Task<List<Rol>> Buscar([FromBody] object pRol)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strRol = JsonSerializer.Serialize(pRol);
            Rol rol = JsonSerializer.Deserialize<Rol>(strRol, option);
            return await rolBL.Buscar(rol);
        }
    }
}
