using ApiImdb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiImdb.Context
{
    public class IMDBContext : DbContext
    {
        public IMDBContext(DbContextOptions<IMDBContext> options) : base(options)
        {

        }

        public DbSet<Administradores> Administradores { get; set; }
        public DbSet<Filmes> Filmes { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Votacoes> Votacoes { get; set; }
    }
}
