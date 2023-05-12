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
    /// Controlador per Preguntes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : ControllerBase
    {
        private readonly PreguntaRepository preguntaRepository;
        public PreguntaController(PreguntaRepository preguntaRepository)
        {
            this.preguntaRepository = preguntaRepository;
        }

        //--------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetPreguntaId()
        {
            return Ok(await preguntaRepository.GetPreguntaId());
        }

        [HttpGet("{dificultat}")]
        public async Task<IActionResult> GetPreguntaDificultat(int dificultat)
        {
            return Ok(await preguntaRepository.GetPreguntaDificultat(dificultat));
        }
        //--------------------------------------------
        [HttpPost]
        public async Task<IActionResult> InsertPregunta([FromBody] Pregunta obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await preguntaRepository.InsertPregunta(obj);
            return Ok(await preguntaRepository.GetPreguntaId());
        }

        //--------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePregunta(int id)
        {
            var deleted = await preguntaRepository.DeletePregunta(new Pregunta { idpregunta = id });
            return Created("Eliminado!", deleted);
        }
    }
}
