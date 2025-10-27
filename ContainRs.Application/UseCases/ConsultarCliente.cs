using ContainRs.Application.Repositories;
using ContainRs.Domain.Models;

namespace ContainRs.Application.UseCases;

public class ConsultarCliente
{
    private readonly IClienteRepository _clienteRepository;
    public ConsultarCliente(UnidadeFederativa? estado, IClienteRepository clienteRepository)
    {
        Estado = estado;
        _clienteRepository = clienteRepository;
    }
    public UnidadeFederativa? Estado { get; }
    
    public Task<IEnumerable<Cliente>> ExecutarAsync()
    {
        if (Estado is not null)
        {
            return _clienteRepository.GetAsync(c => c.Estado == Estado);
        }
        return _clienteRepository.GetAsync();
    }
}