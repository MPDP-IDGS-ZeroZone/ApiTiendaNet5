using Microsoft.AspNetCore.Mvc;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using Microsoft.AspNetCore.Authorization;
using ApiTienda.Services;
using System.Collections;
using System.Collections.Generic;

namespace ApiTienda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : Controller
    {
        private readonly CategoriaService _service;
        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get(int Id = 0, string Nombre = "")
        {
            var Categoria = _service.Get(Id, Nombre);
            
            if (Categoria is null){
                return NotFound();
            }
            return Ok(Categoria);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CategoriaRequest Categoria)
        {
            var newCategoria = _service.Create(Categoria);
            return CreatedAtAction(nameof(Get), new {Id = newCategoria.Idcategoria}, newCategoria);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update(int Id, CategoriaRequest Categoria)
        {
            var CategoriaToUpdate = _service.GetById(Id);

            if(CategoriaToUpdate is not null)
            {
                _service.Update(Id, Categoria);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int Id)
        {
            var CategoriaToDelete = _service.GetById(Id);

            if(CategoriaToDelete is not null)
            {
                _service.Delete(Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}