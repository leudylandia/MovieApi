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
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoriaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(200, Type =typeof(List<Categoria>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategorias()
        {
            var lista = await _dbContext.Categorias.OrderBy(c => c.Nombre).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("{id}", Name = "GetCategoria")] // <= Name para reconocer entre metodos cuando creo una categoria lo mando aqui
        [ProducesResponseType(200, Type = typeof(Categoria))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCategoria(int id)
        {
            var obj = await _dbContext.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CrearCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dbContext.AddAsync(categoria);
            await _dbContext.SaveChangesAsync();

            //return Ok( new { Msg = "Guardado con exito.." });
            return CreatedAtRoute("GetCategoria", new { id = categoria.Id }, categoria);

        }

       
    }
}
