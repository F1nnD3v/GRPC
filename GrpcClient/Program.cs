using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Cliente.ClienteClient(channel);
            bool sair = false;
            do
            {
                Console.Clear();
                Console.WriteLine("---|Menu Clientes|---");
                Console.WriteLine("1 - Pedir info do cliente");
                Console.WriteLine("2 - Ver todos os clientes");
                Console.WriteLine("3 - Sair");
                int opcao = Convert.ToInt32(Console.ReadLine());
                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Escreva o id do cliente que deseja ver as informações: ");
                        int idPedido = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        var idRequested = new ClienteLookupModel { IdCliente = idPedido };
                        var cliente = await client.getClienteInfoAsync(idRequested);
                        Console.WriteLine("Primeiro nome: " + cliente.PrimeiroNome);
                        Console.WriteLine("Último nome: " + cliente.UltimoNome);
                        Console.WriteLine("Idade: " + cliente.Idade);
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Aqui está uma lista de todos os clientes:\n ");
                        using(var call = client.getNovosClientes(new getNovoClienteRequest()))
                        {
                            while(await call.ResponseStream.MoveNext())
                            {
                                var clienteAtual = call.ResponseStream.Current;
                                Console.WriteLine("Primeiro nome: " + clienteAtual.PrimeiroNome);
                                Console.WriteLine("Último nome: " + clienteAtual.UltimoNome);
                                Console.WriteLine("Email: " + clienteAtual.Email);
                                Console.WriteLine("Idade: " + clienteAtual.Idade);
                                Console.WriteLine("");
                            }
                        }
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Tem mesmo a certeza que deseja sair?S/N");
                        string opcaoSair = Console.ReadLine().ToLower();
                        if (opcaoSair == "s")
                        {
                            sair = true;
                        }
                        else if (opcaoSair == "n")
                        {
                            sair = false;
                        }
                        else
                        {
                            while (opcaoSair != "s" && opcaoSair != "n")
                            {
                                Console.Clear();
                                Console.WriteLine("Opção inválida!");
                                Console.WriteLine("Tem mesmo a certeza que deseja sair?S/N");
                                opcaoSair = Console.ReadLine().ToLower();
                            }
                        }
                        break;
                    default:

                        break;
                }
            }while (sair == false);
        }
    }
}
