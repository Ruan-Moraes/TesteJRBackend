using apiToDo.DTO;
using apiToDo.Models;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("listarTarefas")] // Alterei método da rota para GET e o nome da rota/método para listarTarefas.
        public ActionResult listarTarefas() 
        {
            try
            {
                Tarefas tarefas = new Tarefas(); // Instanciei a classe Tarefas (model).
                
                List<TarefaDTO> listaTarefas = tarefas.listarTarefas(); // Chamei o método listarTarefas da classe Tarefas (model) - ele retorna uma lista de tarefas.
                
                if (listaTarefas == null) // Verifico se a lista de tarefas está vazia, caso sim, mando um status 404 e uma mensagem. 
                {
                    return StatusCode(404, new { msg = "Nenhuma tarefa encontrada." });
                }

                return StatusCode(200, listaTarefas); // Se passou pelo if, retorno o status 200 e a lista de tarefas.
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}"});
            }
        }

        [HttpPost("InserirTarefas")]
        public ActionResult InserirTarefas([FromBody] TarefaDTO Request)
        {
            try
            {

                return StatusCode(200);


            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpGet("DeletarTarefa")]
        public ActionResult DeleteTask([FromQuery] int ID_TAREFA)
        {
            try
            {

                return StatusCode(200);
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }
    }
}
