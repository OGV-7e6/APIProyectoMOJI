using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data.Repositories;
using TodoApi.Model;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Controlador per Usuaris
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariController : ControllerBase
    {
        private readonly UsuariRepository usuariRepository;
        public UsuariController(UsuariRepository usuariRepository)
        {
            this.usuariRepository = usuariRepository;
        }

        //--------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAllUsuaris()
        {
            return Ok(await usuariRepository.GetAllUsuaris());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuariId(int id)
        {
            return Ok(await usuariRepository.GetUsuariId(id));
        }

        [HttpGet("{nick},{contrasenya}")]
        public async Task<IActionResult> GetUsuariDetall(string nick, string contrasenya)
        {
            return Ok(await usuariRepository.GetUsuariDetall(nick, contrasenya));
        }
        //--------------------------------------------
        [HttpPost]
        public async Task<IActionResult> InsertUsuari([FromBody] Usuari obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await usuariRepository.InsertUsuari(obj);
            return Created("Creado!", created);
        }

        //--------------------------------------------
        [HttpPut]
        public async Task<IActionResult> UpdateUsuari([FromBody] Usuari obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await usuariRepository.UpdateUsuari(obj);
            return Created("Actualizado!", updated);
        }
        //----------------------------------------
        [HttpPut("/Puntos")]
        public async Task<IActionResult> UpdatePuntuacio([FromBody] Usuari obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await usuariRepository.UpdatePuntuacio(obj);
            return Created("Actualizado!", updated);
        }

        //--------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuari(int id)
        {
            var deleted = await usuariRepository.DeleteUsuari(new Usuari { id = id });
            return Created("Eliminado!", deleted);
        }
    }
}
