using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.in_lock.webApi.Domains;
using senai.in_lock.webApi.Interfaces;
using senai.in_lock.webApi.Repositories;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.in_lock.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JogosController : ControllerBase
    {
        private IJogoRepository _jogoRepository { get; set; }
        public JogosController()
        {
            _jogoRepository = new JogoRepository();
        }

        [Authorize(Roles = "ADMINISTRADOR, COMUM")]
        [HttpGet]
        public IActionResult Get()
        {
            List<JogoDomain> listaJogos = _jogoRepository.ListarTodos();

            return Ok(listaJogos);
        }

        [Authorize(Roles = "ADMINISTRADOR, COMUM")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            JogoDomain jogoBuscado = _jogoRepository.BuscarPorId(id);

            if (jogoBuscado == null)
            {
                return NotFound("Nenhum jogo foi encontrado!");
            }

            return Ok(jogoBuscado);
        }

        [Authorize (Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Post(JogoDomain novoJogo)
        {

            _jogoRepository.Inserir(novoJogo);

            return StatusCode(201);
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _jogoRepository.Deletar(id);
            return NoContent();
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut]
        public IActionResult PutIdBody(JogoDomain jogoAtualizado)
        {
            if (jogoAtualizado.nomeJogo == null || jogoAtualizado.idJogo <= 0 || jogoAtualizado.descricao == null || jogoAtualizado.valor < 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Alguma informação do jogo não foi informada!"
                    });
            }

            JogoDomain jogoBuscado = _jogoRepository.BuscarPorId(jogoAtualizado.idJogo);

            if (jogoBuscado != null)
            {
                try
                {
                    _jogoRepository.AtualizarIdCorpo(jogoAtualizado);

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
                        mensagem = "Jogo não encontrado!",
                        errorStatus = true
                    }
                );
        }
    }
}
