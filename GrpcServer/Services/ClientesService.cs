using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class ClientesService : Cliente.ClienteBase
    {
        private readonly ILogger<ClientesService> _logger;

        public ClientesService(ILogger<ClientesService> logger)
        {
            _logger = logger;
        }

        public override Task<ClienteModel> getClienteInfo(ClienteLookupModel request, ServerCallContext context)
        {
            ClienteModel output = new ClienteModel();

            if(request.IdCliente == 1)
            {
                output.PrimeiroNome = "Ricardo";
                output.UltimoNome = "Silva";
                output.Idade = 18;
            }else if(request.IdCliente == 2)
            {
                output.PrimeiroNome = "João";
                output.UltimoNome = "Damasio";
                output.Idade = 21;
            }
            else
            {
                output.PrimeiroNome = "Maria";
                output.UltimoNome = "Eduarda";
                output.Idade = 23;
            }

            return Task.FromResult(output);
        }

        public override async Task getNovosClientes(getNovoClienteRequest request, IServerStreamWriter<ClienteModel> responseStream, ServerCallContext context)
        {
            List<ClienteModel> clientes = new List<ClienteModel>
            {
                new ClienteModel
                {
                    PrimeiroNome = "Emanuel",
                    UltimoNome = "Silva",
                    Idade = 18,
                    Email = "ricardo.jorge@gmail.com"
                },
                new ClienteModel 
                {
                    PrimeiroNome = "Miguel",
                    UltimoNome = "Andrade",
                    Idade = 19,
                    Email = "andrademiguel@gmail.com"
                },
                new ClienteModel
                {
                    PrimeiroNome = "José",
                    UltimoNome = "Duarte",
                    Idade = 21,
                    Email = "joseduarte@hotmail.com"
                },
            };

            foreach(var cliente in clientes)
            {
                await responseStream.WriteAsync(cliente);
            }
        }
    }
}
