using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace To_Do_List
{
    internal class Program
    {
        public class Tarefa
        {
            //Abaixo temos as propriedades auto implementadas, atributos para a classe
            public string Titulo{ get; set; }
            public string Descricao { get; set; }
            public DateTime DataCriacao { get; set; }
            public int Prioridade { get; set; }
            public bool Concluida { get; set; }

            //Método construtor para a classe tarefa
            public Tarefa(string titulo, string descricao, int prioridade)
            {
                Titulo = titulo;
                Descricao = descricao;
                Prioridade = prioridade;
                DataCriacao = DateTime.Now;
                Concluida =  false;
            }
        }

        public class GerenciadorDeTarefas
        {
            private List<Tarefa> listaDeTarefas;
        
        public GerenciadorDeTarefas()
        {
            listaDeTarefas = new List<Tarefa>();
        }
        public void Executar()
        {
            int opcao;

            //enquanto a variavel opcao não for 0 (ou seja, Sair)
            // o código vai fazer o que está no do (será feito ao menos 1 vez

            do
            {
                // começa limpando o console
                Console.Clear();
                Console.WriteLine("=== GERENCIADOR DE TAREFAS ===");
                Console.WriteLine("Selecione uma das opções a seguir:");
                Console.WriteLine("1 - Adicionar Tarefa");
                Console.WriteLine("2 - Listar Tarefas");
                Console.WriteLine("3 - Marcar Tarefa como concluída");
                Console.WriteLine("4 - Filtrar por prioridade");
                Console.WriteLine("5 - Estatísticas");
                Console.WriteLine("0 - Sair");

                //variável que vai receber e armazenar o que o usuário digitar
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                             AdicionarTarefas();
                            break;
                        case 2:
                            listarTarefas();
                            break;
                        case 3:
                            concluirTarefa();
                            break;
                        case 4:
                            FiltrarPorPrioridade();
                            break;
                        case 5:
                            //implementar posteriormente
                            Console.WriteLine("Funcionalidade ainda em desenvolvimento");
                            break;
                        case 0:
                            Console.WriteLine("Volte sempre ! Saindo...");
                            break;
                        default:
                            Console.WriteLine("Opção Inválida. Por favor, digite uma das opções acima.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Digite um número válido por gentileza");
                    opcao = -1;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("Pressione qualquer tecla para sair.");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

            //método responsável por adicionar nova tarefa
            private void AdicionarTarefas()
            {
                Console.WriteLine("=== ADICIONAR TAREFA === \n");
                Console.WriteLine();
                //abaixo adicionaremos o título a tarefa (seja ele qual for)
                Console.Write("Título: ");
                string titulo = Console.ReadLine();

                //a condicional abaixo é para garantir que o título não será um espaço em branco e nem será nulo
                do
                {
                    if (string.IsNullOrWhiteSpace(titulo))
                    {
                        Console.WriteLine("Por favor, escreva um título que seja válido");
                        Console.WriteLine("Pressione qualquer tecla para colocar um novo título");
                        Console.ReadKey(true);
                        Console.Write("Título: ");
                        titulo = Console.ReadLine();
                        
                    }
                } while (string.IsNullOrWhiteSpace(titulo));

                //abaixo escreveremos a descrição da tarefa.
                Console.Write("Descrição: ");
                string descricao = Console.ReadLine();
                //a condicional abaixo é para garantir que o título não será espaço em branco e nem será nulo
                if (string.IsNullOrWhiteSpace(descricao))
                {
                    descricao = "Sem descrição";
                }

                //abaixo leremos a prioridade e ela terá validação
                int prioridade = 0;
                bool prioridadeValida = false;

                while(!prioridadeValida)
                {
                    Console.Write("Prioridade (1-5): ");
                    string entrada = Console.ReadLine();

                    if(int.TryParse(entrada, out prioridade) && prioridade >= 1 && prioridade <= 5)
                    {
                        prioridadeValida = true;
                    } else
                    {
                        Console.WriteLine("Prioridade inválida. Digite um número entre 1 e 5 por favor");
                    }

                }

                Tarefa novaTarefa = new Tarefa(titulo, descricao, prioridade);
                listaDeTarefas.Add(novaTarefa);
                Console.WriteLine("✅ EXCELENTE !!! Sua tarefa foi adicionada com sucesso !");
                Console.WriteLine() ;

                //ABAIXO ESTARÁ O RESUMO DA TAREFA:
                Console.WriteLine("📝 RESUMO DA TAREFA: ");
                Console.WriteLine($" Titulo: {(novaTarefa.Titulo)}");
                Console.WriteLine($" Descrição: {(novaTarefa.Descricao)}");
                Console.WriteLine($" Prioridade: {(novaTarefa.Prioridade)}");
                Console.WriteLine($" Data: {novaTarefa.DataCriacao:dd/MM/yyyy HH:mm}");
                Console.WriteLine($" Status: {(novaTarefa.Concluida ? "Concluída":"Pendente")}");

            }

            private void listarTarefas()
            {
                Console.Clear();
                Console.WriteLine("=== LISTA DE TAREFAS === \n");

                if(listaDeTarefas.Count == 0)
                {
                    Console.WriteLine("Não há nenhuma tarefa por aqui... (Por enquanto)");
                    return;
                }

                for(int i = 0; i < listaDeTarefas.Count; i++)
                {
                    Tarefa tarefa = listaDeTarefas[i];
                    string status = tarefa.Concluida ? "Sim" : "Não";

                    Console.WriteLine($" {i+1} {tarefa.Titulo}");
                    Console.WriteLine($" Descrição: {tarefa.Descricao}");
                    Console.WriteLine($" Prioridade:{tarefa.Prioridade} e Data: {tarefa.DataCriacao:dd/MM/yyyy}");
                    Console.WriteLine();
                }

                Console.WriteLine($" O total é: {listaDeTarefas.Count} tarefa(s) !!!");
            }

            private void concluirTarefa()
            {
                Console.Clear();
                Console.WriteLine("=== MARCAR TAREFAS CONCLUÍDAS ===");

                //assim como nos outros métodos, esse vai verificar se existem tarefas a serem concluídas
                if(listaDeTarefas.Count == 0)
                {
                    Console.WriteLine("Não há nada aqui para ser concluído, inicie uma nova tarefa.");
                    return;
                }

                mostrarTarefasParaConclusao();

                //Solicitar dígito do usuário
                Console.WriteLine("\n Digite o número da tarefa a ser concluída \n");
                string digitoUsuario = Console.ReadLine();

                if(int.TryParse(digitoUsuario,out int  indice))
                {
                    processarConclusao(indice);
                } else
                {
                    Console.WriteLine("Entrada inválida. Digite um número por gentileza.");
                }
            }

            //O nome é auto explicativo, o método abaixo mostra tarefas para serem concluídas
            private void mostrarTarefasParaConclusao()
            {
                Console.WriteLine("Tarefas disponíveis (ainda) \n");

                for(int i = 0;i<listaDeTarefas.Count;i++)
                {
                    Tarefa mostrarTarefa = listaDeTarefas[i];
                    string statusTarefa = mostrarTarefa.Concluida ? "Concluída" : "Em andamento";

                    Console.WriteLine($"{i + 1}. {statusTarefa} {mostrarTarefa.Titulo} (Prioridade: {mostrarTarefa.Prioridade})");
                    Console.WriteLine($" Descrição: {mostrarTarefa.Descricao}");
                    Console.WriteLine($" Data: {mostrarTarefa.DataCriacao:dd/MM/yyyy HH:mm}");
                    Console.WriteLine();
                }
            }

            private void processarConclusao(int indice)
            {
                int posicaoTarefa = indice - 1;

                if(posicaoTarefa >= 0 && posicaoTarefa < listaDeTarefas.Count)
                {
                    Tarefa tarefinha = listaDeTarefas[posicaoTarefa];
                    
                    if(tarefinha.Concluida)
                    {
                        Console.WriteLine($"\n ⚠️ A tarefa {tarefinha.Titulo} já estava concluída !!!");
                    } else
                    {
                        tarefinha.Concluida = true;
                        Console.WriteLine($"A tarefa {tarefinha.Titulo} foi concluída com sucesso !!!");
                    }
                }
                else
                {
                    Console.WriteLine("Número de tarefa inválido, digite um que seja válido.");
                }
            }

            private void FiltrarPorPrioridade()
            {
                Console.Clear();
                Console.WriteLine("=== FILTRAR TAREFAS POR PRIORIDADE ===");

                //Verifica se há tarefas, caso não tenha. Ele retorna
                if(listaDeTarefas.Count <= 0)
                {
                    Console.WriteLine("Não há tarefas para filtrar...");
                    return;
                }


                for(int prioridade = 1; prioridade <= 5; prioridade++)
                {
                    Console.WriteLine($" \n=== Prioridade {prioridade}: ===\n");
                    int contaPrioridade = 0;


                        for(int i = 0; i < listaDeTarefas.Count; i++)
                        {
                           Tarefa tarefazinha = listaDeTarefas[i];
                            
                            if(tarefazinha.Prioridade == prioridade)
                            {
                                 Console.WriteLine($"{tarefazinha.Titulo}");
                                 Console.WriteLine($"Descrição: {tarefazinha.Descricao}");
                                 Console.WriteLine($"Data: {tarefazinha.DataCriacao:dd/MM/yyyy HH:mm}");
                                 Console.WriteLine();                          
                                 contaPrioridade++;
                            }
                        
                        }

                        if(contaPrioridade == 0)
                    {
                        Console.WriteLine("Parece que não há nada aqui... cri cri 🦗");
                    }
                }

            }
        }

        static void Main(string[] args)
        {
            GerenciadorDeTarefas gerenciador = new GerenciadorDeTarefas();
            gerenciador.Executar();
            
        }
    }
}