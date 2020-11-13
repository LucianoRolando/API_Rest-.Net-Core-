using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WS_Mensajeria.Models;

namespace WS_Mensajeria.Contexts
{
    public class AppDBContexts : DbContext
    {
        public AppDBContexts(DbContextOptions<AppDBContexts> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Mensaje> Mensajes { get; set; }

        public int IdLog { get; set; }
    }
}
