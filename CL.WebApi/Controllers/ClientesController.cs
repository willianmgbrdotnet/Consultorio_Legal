using CL.Core.Domain;
using CL.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteManager clienteManager;

        public ClientesController(IClienteManager clienteManager)
        {
            this.clienteManager = clienteManager;
        }

        // GET: api/<ClientesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await clienteManager.GetClientesAsync());
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await clienteManager.GetClienteAsync(id));
        }

        // POST api/<ClientesController>
        [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            var clienteInserido = await clienteManager.InsertClienteAsync(cliente);
            //Observar a utilidade do método "CreatedAtAction" e seus recursos.
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        // PUT api/<ClientesController>/5
        [HttpPut]
        public async Task<IActionResult> Put(Cliente cliente)
        {
            var clienteAtualizado = await clienteManager.UpdateClienteAsync(cliente);
            if (clienteAtualizado == null)
            {
                return NotFound();
            }
            return Ok(clienteAtualizado);
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await clienteManager.DeleteClienteAsync(id);
            return NoContent();
        }
    }
}
