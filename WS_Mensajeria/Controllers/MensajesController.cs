using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WS_Mensajeria.Contexts;
using WS_Mensajeria.Models;

namespace WS_Mensajeria.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        private readonly AppDBContexts contexts;

        public MensajesController(AppDBContexts contexts)
        {
            this.contexts = contexts;
        }

          //Lista los mensajes enviados y recibidos, ordenados por fecha,
          //entre el usuario logueado(Id token) y el usuario pasado por url(Id)
        // GET api/<MensajesController>/5
        [HttpGet("{id}")]
        public IEnumerable<Mensaje> Get(int id)
        {
            var claims = User.Claims.First().Value;
            var claimid = Convert.ToInt32(claims);

            IEnumerable<Mensaje> mensajes = null;
            try
            {
                mensajes = contexts.Mensajes.FromSqlInterpolated($"ListarMensajes {claimid}, {id}").ToList();
            }
            catch
            { }

            return mensajes;
        }

         //Agrega mensaje enviado por el usuario logueado(id del token) a usuario pasado por url
        // POST api/<MensajesController>/5
        [HttpPost("{id}")]
        public ActionResult Post(int id, [FromBody] Mensaje mensaje)
        {

            var claims = User.Claims.First().Value;
            var claimid = Convert.ToInt32(claims);
            mensaje.EM = claimid;

            mensaje.RE = id;
            
            DateTime fechayhora = DateTime.Now;
            mensaje.FECHA = fechayhora;

            try
            {
                contexts.Database.ExecuteSqlInterpolated($"[dbo].[AgregarMensaje] {mensaje.EM},{mensaje.RE},{mensaje.FECHA},{mensaje.TEXTO}");

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

         //Estos metodos (Put y Delete) no tienen procedimientos para llevarlos a cabo
        // PUT api/<MensajesController>/5
       [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Mensaje mensaje)
        {
            if (mensaje.RE == id)
            {
                contexts.Entry(mensaje).State = EntityState.Modified;
                contexts.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<MensajesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete([FromBody] Mensaje mensaje)
        {
            var mje = contexts.Mensajes.FirstOrDefault(p => p.ID == mensaje.ID);
            if (mje != null)
            {
                contexts.Mensajes.Remove(mje);
                contexts.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

