using CL.Core.Domain;
using CL.Data.Context;
using CL.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        //Injeção de Dependência
        private readonly ClContext context;

        public ClienteRepository(ClContext context)
        {
            this.context = context;
        }
        
        //Métodos Asyncronos devem ter "Async" no final do nome.
        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            //Para liberar a mémoria depois que a lista for apresentada, usa "AsNoTracking()"
            return await context.Clientes.AsNoTracking().ToListAsync();
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            //return await context.Clientes.Where(x => x.Id == id).FirstOrDefaultAsync();
            //return await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            //return await context.Clientes.SingleOrDefaultAsync(x => x.Id == id); //"Retorna erro caso tenha duplicata"
            return await context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> InsertClienteAsync(Cliente cliente)
        {
            await context.Clientes.AddAsync(cliente);
            await context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            var clienteConsultado = await context.Clientes.FindAsync(cliente.Id);
            if(clienteConsultado == null)
            {
                return null;
            }
            /*clienteConsultado.Nome = cliente.Nome;
            clienteConsultado.DataNascimento = cliente.DataNascimento;
            context.Clientes.Update(clienteConsultado);*/
            //A entidade cliente tem apenas 2 propriedades editáveis: nome e dataNascimento.
            //Pense se tivesse 10, 30 ou mais propriedades. Para não termos que escrever todos
            //os campos de uma entidade em sua atualização, existe o seguinte comando:
            context.Entry(clienteConsultado).CurrentValues.SetValues(cliente);

            await context.SaveChangesAsync();
            return clienteConsultado;
        }

        public async Task DeleteClienteAsync(int id)
        {
            var clienteConsultado = await context.Clientes.FindAsync(id);
            context.Clientes.Remove(clienteConsultado);
            await context.SaveChangesAsync();
        }
        //Agora, subimos os métodos para a Interface e Implementamos a classe Manager.
    }
}
