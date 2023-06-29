using CL.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Na API, foram instalados os nuggets: EF CORE, EF CORE.DESIGN e EF CORE.TOOLS.
namespace CL.Data.Context
{
    //Aqui foi instalado o nugget EF CORE.
    public class ClContext : DbContext
    {
        //Tabela do Banco de Dados gerada através do ORM.   
        public DbSet<Cliente> Clientes { get; set; }

        public ClContext(DbContextOptions options) : base(options)
        {

        }
    }
}
