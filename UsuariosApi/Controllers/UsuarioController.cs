using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Models;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
