using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Data.Request;
using TiendaAPI.Services;

namespace TiendaAPI.Controllers
{
    [ApiController]
    [Route("Login")]
    public class AuthController : ControllerBase
{
    private readonly AuthService _auth;

    public AuthController(AuthService auth)
    {
        _auth = auth;
    }

    [HttpPost]
    public IActionResult Login([FromBody] Login model)
    {
        var token = _auth.Authenticate(model.Mail, model.Pasword);

        if (token == null)
        {
            return Unauthorized(); // Credenciales no válidas
        }

        return Ok(token);
    }
}
}