using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
    }
}
