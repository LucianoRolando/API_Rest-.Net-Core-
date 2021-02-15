using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WS_Mensajeria.Contexts;
using WS_Mensajeria.Models;

namespace WS_Mensajeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration Config;

        private readonly AppDBContexts contexts;

        public LoginController(IConfiguration config, AppDBContexts contexts)
        {
            Config = config;
            this.contexts = contexts;
        }

        [HttpGet]
        public ActionResult Start()
        {

            return Ok("Bienvenidos");

        }

        //Metodos para verificar si la cuenta ingresada es valida y si es asi se crea un token

        [HttpPost]
        public ActionResult Login([FromBody] Usuario cuenta)
        {
            ActionResult response = Unauthorized();

            if (User.Identity.IsAuthenticated)
            {
                response = Ok();
                return response;
            }
            else 
            {
                var user = AuthenticateUser(cuenta);

                if (user != null)
                {
                    GenerarJWT generarJWT = new GenerarJWT(Config);
                    var tokenstr = generarJWT.GenerateJWT(user);
                    response = Ok(new { token = tokenstr });
                }
                return response;
            }
        }

        //En este metodo se realiza la comprobacion de los datos ingresados, no utilice procedimiento
        private Usuario AuthenticateUser(Usuario us)
        {
            Usuario Usuario = null;

            Usuario = contexts.Usuario.FirstOrDefault(p => p.Nick == us.Nick && p.Contraseña == us.Contraseña);

            return Usuario;
        }
    }
}
