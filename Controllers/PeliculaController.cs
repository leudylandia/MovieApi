using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PeliculaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetPeliculas")]
        public async Task<IActionResult> GetPeliculas()
        {
            var lista = await _dbContext.Peliculas.Include(c => c.Categoria).OrderBy(p => p.Nombre).ToListAsync();

            return Ok(lista);
        }

        [HttpGet]
        [Route("GetPelicula/{id}")]
        public async Task<IActionResult> GetPelicula(int id)
        {
            var obj = await _dbContext.Peliculas.Include(c => c.Categoria).FirstOrDefaultAsync(p => p.Id == id);

            if(obj == null)
                return NotFound(new { Msj = $"Está pelicula no existe.." });

            return Ok(obj);
        }

        [HttpGet]
        [Route("GetPeliculaByCategoriaId/{categoriaId}")]
        public async Task<IActionResult> GetPeliculaByCategoriaId(int categoriaId)
        {
            var lista = await _dbContext.Peliculas.Include(c => c.Categoria).OrderBy(p => p.Nombre).Where(p => p.CategoriaId == categoriaId).ToListAsync();

            if (lista == null || lista.Count < 1)
                return NotFound(new { Msj = $"No se encontraron peliculas" });

            return Ok(lista);
        }

        [HttpPost]
        [Route("crearPelicula")]
        public async Task<IActionResult> CrearPelicula([FromBody] Pelicula pelicula)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dbContext.AddAsync(pelicula);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Msg = "Guardado con exito.." });
        }
    }
}
