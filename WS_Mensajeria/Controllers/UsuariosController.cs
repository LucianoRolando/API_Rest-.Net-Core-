using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WS_Mensajeria.Contexts;
using WS_Mensajeria.Models;

namespace WS_Mensajeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDBContexts contexts;
        private readonly IConfiguration Config;

        public UsuariosController(IConfiguration config, AppDBContexts contexts)
        {
            Config = config;
            this.contexts = contexts;
        }


         //Lista los usuarios con los que el usuario logueado puede interactuar
        // GET: api/<UsuariosController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            var claims = User.Claims.First().Value;
            var claimid = Convert.ToInt32(claims);
            IEnumerable<Usuario> ListaUsuarios = null;

            try
            {
                ListaUsuarios = contexts.Usuario.FromSqlInterpolated($"ListarUsuarios {claimid}").ToList();
            }
            catch
            { }

            return ListaUsuarios;
        }

         //Devuelve detalles del usuario logueado
        // GET: api/<UsuariosController>/Editar
        [Authorize]
        [HttpGet("Editar")]
        public Usuario GetUser()
        {
            var claims = User.Claims.First().Value;
            var claimid = Convert.ToInt32(claims);

            Usuario us = null;

            try
            {
                List<Usuario> usa = contexts.Usuario.FromSqlInterpolated($"DatosUsuario {claimid}").ToList();
                us = usa[0];
            }
            catch
            { }

            return us;
        }

         //Agrega nuevas cuentas de usuario
        // POST api/<UsuariosController>
        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                var filasafectadas = contexts.Database.ExecuteSqlInterpolated($"[dbo].[AgregarUsuario] {usuario.Nick},{usuario.Contraseña},{usuario.Nombre},{usuario.Descripcion},{usuario.Enlace}");

                if (filasafectadas > 0)
                {
                    return Ok();
                }
                else
                {
                    return Ok("Nick ya registrado");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

         //Modifica datos de usuario
        // PUT api/<UsuariosController>/5
        [Authorize]
        [HttpPut("Editar")]
        public ActionResult Put([FromBody] Usuario usuario)
        {
            ActionResult response = Unauthorized();

            var claims = User.Claims.First().Value;
            var claimid = Convert.ToInt32(claims);

            GenerarJWT generarJWT = new GenerarJWT(Config);

            usuario.ID = claimid;

            try
            {
                contexts.Database.ExecuteSqlInterpolated($"[dbo].[EditarUsuario] {usuario.ID},{usuario.Nick},{usuario.Contraseña},{usuario.Nombre},{usuario.Descripcion},{usuario.Enlace}");

                var tokenstr = generarJWT.GenerateJWT(usuario);
                response = Ok(new { token = tokenstr });

                return Ok(response);
            }
            catch
            {
                return BadRequest(response);
            }
        }

         //Elimina el usuario que ha iniciado sesion, lo hace con el id del token
        // DELETE api/<UsuariosController>/5
        [Authorize]
        [HttpDelete]
        public ActionResult Delete()
        {
            var claims = User.Claims.First().Value;
            var claimid = Convert.ToInt32(claims);

            try
            {
                contexts.Database.ExecuteSqlInterpolated($"[dbo].[EliminarUsuario] {claimid}");

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
