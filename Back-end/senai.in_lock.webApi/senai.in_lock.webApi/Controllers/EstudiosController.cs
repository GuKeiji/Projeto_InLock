using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.in_lock.webApi.Domains;
using senai.in_lock.webApi.Interfaces;
using senai.in_lock.webApi.Repositories;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiosController : ControllerBase
    {
        private IEstudioRepository _estudioRepository { get; set; }
        public EstudiosController()
        {
            _estudioRepository = new EstudioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<EstudioDomain> listaEstudios = _estudioRepository.ListarTodos();

            return Ok(listaEstudios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EstudioDomain estudioBuscado = _estudioRepository.BuscarPorId(id);

            if (estudioBuscado == null)
            {
                return NotFound("Nenhum estudio foi encontrado!");
            }

            return Ok(estudioBuscado);
        }

        [HttpPost]
        public IActionResult Post(EstudioDomain novoEstudio)
        {

            _estudioRepository.Inserir(novoEstudio);

            return StatusCode(201);
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _estudioRepository.Deletar(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult PutIdBody(EstudioDomain estudioAtualizado)
        {
            if (estudioAtualizado.nomeEstudio == null || estudioAtualizado.idEstudio <= 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome ou o id do estudio não foi informado!"
                    });
            }

            EstudioDomain estudioBuscado = _estudioRepository.BuscarPorId(estudioAtualizado.idEstudio);

            if (estudioBuscado != null)
            {
                try
                {
                    _estudioRepository.AtualizarIdCorpo(estudioAtualizado);

                    return NoContent();
                }
                catch (Exception codErro)
                {
                    return BadRequest(codErro);
                }
            }

            return NotFound(
                    new
                    {
                        mensagem = "Estudio não encontrado!",
                        errorStatus = true
                    }
                );
        }
    }
}

