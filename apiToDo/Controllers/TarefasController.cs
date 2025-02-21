using apiToDo.DTO;
using apiToDo.Models;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace apiToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        // [Authorize] - Comentei a linha de autorização, vi que ele estava ocasionando erro na chamada da Rota. 
        // Site que li: https://learn.microsoft.com/pt-br/aspnet/core/security/authorization/simple?view=aspnetcore-9.0
        [HttpGet("lstTarefas")] // Alterei método da rota para GET
        public ActionResult lstTarefas()
        {
            try
            {
                Tarefas Tarefas = new Tarefas(); // Instanciei a classe Tarefas (model). 

                List<TarefaDTO>
                    listaTarefas =
                        Tarefas.lstTarefas(); // Chamei o método listarTarefas da classe Tarefas (model) - ele retorna uma lista de tarefas.

                if (listaTarefas ==
                    null) // Verifico se a lista de tarefas está vazia, caso sim, mando um status 404 e uma mensagem. 
                {
                    return StatusCode(404, new { msg = "Nenhuma tarefa encontrada." });
                }

                return StatusCode(200, listaTarefas); // Se passou pelo if, retorno o status 200 e a lista de tarefas.
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new
                    {
                        msg = $"Ocorreu um erro em sua API {ex.Message}"
                    }); // Se ocorrer um erro, retorno o status 500 e a mensagem de erro.
            }
        }

        [HttpGet("buscarTarefaPorId")] // Adicionei essa rota porque já tinha criado o método buscarPorId na classe Tarefas (model). Aí já aproveitei para criar a rota.
        public ActionResult buscarTarefaPorId([FromQuery] int ID_TAREFA)
        {
            try
            {
                Tarefas Tarefas = new Tarefas();

                TarefaDTO
                    tarefaBuscada =
                        Tarefas.buscarTarefaPorId(
                            ID_TAREFA); // Chamei o método buscarPorId da classe Tarefas (model) - ele retorna uma tarefa pelo ID.

                if (tarefaBuscada == null)
                {
                    return StatusCode(404, new { msg = "Nenhuma tarefa encontrada." });
                }

                return StatusCode(200, tarefaBuscada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpPost("InserirTarefas")]
        public ActionResult InserirTarefas([FromBody] TarefaDTO Request)
        {
            try
            {
                Tarefas Tarefas = new Tarefas();

                List<TarefaDTO> TarefasAtualizadas = Tarefas.InserirTarefa(Request);

                if (TarefasAtualizadas == null)
                {
                    return StatusCode(400,
                        new { msg = "Erro ao adicionar tarefa. Verifique se o id da tarefa não é repetido.", });
                }

                return
                    StatusCode(201,
                        TarefasAtualizadas); // Retorno o status 201 (criado com sucesso) e a lista de tarefas atualizada.
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpPut("AtualizarTarefa")] // Criei a rota AtualizarTarefa
        public ActionResult AtualizarTarefa([FromBody] TarefaDTO Request)
        {
            try
            {
                Tarefas Tarefas = new Tarefas();

                List<TarefaDTO> listaTarefasAtualizadas = Tarefas.AtualizarTarefa(Request);

                if (listaTarefasAtualizadas == null)
                {
                    return StatusCode(404,
                        new { msg = "Erro ao atualizar tarefa. Verifique se o id da tarefa existe." });
                }

                return StatusCode(200, listaTarefasAtualizadas);
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpDelete(
            "DeletarTarefa")] // Alterei o método de GET para DELETE.
        public ActionResult DeleteTask([FromQuery] int ID_TAREFA)
        {
            try
            {
                List<TarefaDTO>
                    listaTarefasAtualizadas =
                        new Tarefas()
                            .DeletarTarefa(
                                ID_TAREFA); // Chamei o método DeletarTarefa da classe Tarefas (model) - ele deleta uma tarefa pelo ID.

                if (listaTarefasAtualizadas == null)
                    return StatusCode(404, new { msg = "Nenhuma tarefa encontrada para ser removida." });

                return StatusCode(200, listaTarefasAtualizadas);
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }
    }
}