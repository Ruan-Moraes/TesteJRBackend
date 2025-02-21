using System;
using System.Collections.Generic;
using System.Linq;
using apiToDo.DTO;

namespace apiToDo.Models
{
    public class Tarefas
    {
        private static readonly List<TarefaDTO>
            _listaDeTarefas = new(); // Criei uma lista de tarefas. Facilita a manipulação dos itens.

        public Tarefas() // Fiz um mock de tarefas direto no construtor da classe.
        {
            if (!_listaDeTarefas
                    .Any()) // Verifica se a lista de tarefas está vazia, caso sim, adiciona tarefas. Solução temporária.
                // Descobrir esse método neste site: https://www.delftstack.com/pt/howto/csharp/check-if-list-is-empty-in-csharp/
            {
                _listaDeTarefas.Add(new TarefaDTO
                {
                    ID_TAREFA = 1,
                    DS_TAREFA = "Fazer Compras"
                });
                _listaDeTarefas.Add(new TarefaDTO
                {
                    ID_TAREFA = 2,
                    DS_TAREFA = "Fazer Atividade da Faculdade"
                });
                _listaDeTarefas.Add(new TarefaDTO
                {
                    ID_TAREFA = 3,
                    DS_TAREFA = "Subir Projeto de Teste no GitHub"
                });
            }
        }

        // Em relação aos métodos abaixo, eu sei que é uma péssima prática colocar métodos de manipulação de dados direto na entidade.
        // O correto seria criar uma classe de serviço para manipular esses dados...
        // Isso facilitaria a manutenção do código e a organização do projeto, e ainda estaria seguindo um dos princípios do SOLID.

        public List<TarefaDTO> lstTarefas()
        {
            try
            {
                if (_listaDeTarefas.Count > 0) // Verifica se a lista de tarefas está vazia.
                    return _listaDeTarefas; // Retorna a lista de tarefas.

                return null; // Retorna null em caso de lista vazia.
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TarefaDTO buscarTarefaPorId(int ID_TAREFA)
        {
            try
            {
                return
                    _listaDeTarefas.FirstOrDefault(x =>
                        x.ID_TAREFA ==
                        ID_TAREFA); // Retorna a tarefa encontrada ou null em caso de tafera não encontrada.
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TarefaDTO
            InserirTarefa(
                TarefaDTO Request) // Alterei o retorno do método de void para TarefaDTO para melhorar a resposta da API.
        {
            try
            {
                TarefaDTO tarefaNova =
                    new TarefaDTO // Criei um objeto do tipo TarefaDTO para adicionar na lista de tarefas.
                    {
                        ID_TAREFA = Request.ID_TAREFA,
                        DS_TAREFA = Request.DS_TAREFA
                    };


                List<TarefaDTO> listaTarefas = lstTarefas();

                foreach (var tarefa in
                         listaTarefas) // Verifico se a tarefa já existe na lista de tarefas pelo ID_TAREFA.
                {
                    if (tarefa.ID_TAREFA == tarefaNova.ID_TAREFA)
                    {
                        return null;
                    }
                }

                _listaDeTarefas.Add(tarefaNova); // Adicionei a nova tarefa na lista de tarefas.

                return buscarTarefaPorId(tarefaNova.ID_TAREFA); // Retorna a tarefa inserida ou null em caso de erro.
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TarefaDTO AtualizarTarefa(TarefaDTO Request)
        {
            try
            {
                List<TarefaDTO> lstResponse = lstTarefas();

                TarefaDTO Tarefa = lstResponse.FirstOrDefault(x => x.ID_TAREFA == Request.ID_TAREFA);
                Tarefa.DS_TAREFA = Request.DS_TAREFA;

                return Tarefa; // Retorna a tarefa atualizada.
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TarefaDTO DeletarTarefa(int ID_TAREFA)
        {
            try
            {
                List<TarefaDTO> lstResponse = lstTarefas();

                TarefaDTO Tarefa = lstResponse.FirstOrDefault(x => x.ID_TAREFA == ID_TAREFA);
                lstResponse.Remove(Tarefa);

                return Tarefa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}