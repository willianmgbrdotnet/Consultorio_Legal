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
    }
}
