using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS_Mensajeria.Models
{
    public class Mensaje
    {
        public int ID { get; set; }
        public int EM { get; set; }
        public int RE { get; set; }
        public DateTime FECHA { get; set; }
        public string TEXTO { get; set; }
    }
}
