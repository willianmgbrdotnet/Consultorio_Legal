using CL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Manager.Interfaces
{
    public interface IClienteRepository
    {
        //Esses métodos foram transferidos da ClienteRepository para esta Interface através do recurso pull "metodo" up.
        //Clique no metodo, clique na lâmpada, pull " " up, pull members up to base type..., clique na Interface de destino,
        //e escolha os membros (métodos) que serão copiados para a Interface.
        Task<Cliente> GetClienteAsync(int id);
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task<Cliente> InsertClienteAsync(Cliente cliente);
        Task<Cliente> UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(int id);

    }
}
