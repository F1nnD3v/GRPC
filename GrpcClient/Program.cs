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
            //Console.WriteLine("Escreva o seu nome: ");
            //string nome = Console.ReadLine();
            //var input = new HelloRequest { Name = nome };
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Cliente.ClienteClient(channel);
            bool sair = false;
            do
            {
                Console.Clear();
                Console.WriteLine("---|Menu Clientes|---");
                Console.WriteLine("1 - Pedir info cliente");
                Console.WriteLine("2 - Sair");
                int opcao = Convert.ToInt32(Console.ReadLine());
                if(opcao == 1)
                {
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

                }else if(opcao == 2)
                {
                    Console.WriteLine("Tem mesmo a certeza que deseja sair?S/N");
                    string opcaoSair = Console.ReadLine().ToLower();
                    if(opcaoSair == "s")
                    {
                        sair = true;
                    }else if(opcaoSair == "n")
                    {
                        sair=false;
                    }
                    else
                    {
                        while(opcaoSair != "s" && opcaoSair != "n")
                        {
                            Console.Clear();
                            Console.WriteLine("Opção inválida!");
                            Console.WriteLine("Tem mesmo a certeza que deseja sair?S/N");
                            opcaoSair = Console.ReadLine().ToLower();
                        }
                    }
                }
            }while (sair == false);
        }
    }
}
