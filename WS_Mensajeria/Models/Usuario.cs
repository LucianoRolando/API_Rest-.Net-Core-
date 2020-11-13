using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS_Mensajeria.Models
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        public string Nick { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Enlace { get; set; }
    }
}
