using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaCRUD.BL;
using PruebaCRUD.EN;
using PruebaCRUD.WebAPI.Auth;
using System.Text.Json;

namespace PruebaCRUD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioBL usuarioBL = new UsuarioBL();

        /* private readonly IJwtAuthenticationService authService;
        public UsuarioController(IJwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        */
        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await usuarioBL.ObtenerTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<Usuario> Get(int id)
        {
            Usuario usuario = new Usuario();
            usuario.Id = id;
            return await usuarioBL.ObtenerPorIdAsync(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                await usuarioBL.CrearAsync(usuario);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            if (usuario.Id == id)
            {
                await usuarioBL.ModificarAsync(usuario);
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
                Usuario usuario = new Usuario();
                usuario.Id = id;
                await usuarioBL.EliminarAsync(usuario);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
