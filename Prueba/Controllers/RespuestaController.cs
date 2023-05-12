using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data.Repositories;
using TodoApi.Model;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Controlador per Resposte
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaController : ControllerBase
    {
        private readonly RespuestaRepository respuestaRepository;
        public RespuestaController(RespuestaRepository respuestaRepository)
        {
            this.respuestaRepository = respuestaRepository;
        }

        //--------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAllPregunta()
        {
            return Ok(await respuestaRepository.GetAllRespuesta());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRespuestaDetall(int id)
        {
            return Ok(await respuestaRepository.GetRespuestaDetall(id));
        }
        //--------------------------------------------
        [HttpPost]
        public async Task<IActionResult> InsertRespuesta([FromBody] Respuesta obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await respuestaRepository.InsertRespuesta(obj);
            return Created("Creado!", created);
        }
    }
}
