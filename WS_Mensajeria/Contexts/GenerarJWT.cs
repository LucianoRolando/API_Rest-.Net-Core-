using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WS_Mensajeria.Models;

namespace WS_Mensajeria.Contexts
{
    //Esta clase genera el token, se utiliza al ingresar(Login) y al modificar los datos del usuario
    //ya que en el token esta el nick del usuario logueado
    public class GenerarJWT
    {
        private IConfiguration Config { get; set; }

        public GenerarJWT(IConfiguration config)
        {
            Config = config;            
        }

        //Este metodo genera el JWT, la llave esta en las propiedades del proyecto -> depuracion(debug) -> variables de entorno
        //El token generado vence a los 120 minutos
        public string GenerateJWT(Usuario us)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, Convert.ToString(us.ID)),
                new Claim(JwtRegisteredClaimNames.Sub, us.Nick),
                
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Config["Jwt:Issuer"],
                audience: Config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodetoken;
        }
    }
}
