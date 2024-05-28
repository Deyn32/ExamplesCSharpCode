using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZooFerma.Models.Dao;
using ZooFerma.Models.Dto;
using ZooFerma.Services.Dto;

namespace ZooFerma.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientsController : Controller
    {
        private readonly IListClientsService _clients;

        public ClientsController(IListClientsService clients) 
        {
            _clients = clients;
        }

        [Authorize]
        [HttpGet("list")]
        public async Task<List<ResponseClientDto>> GetClients(string email)
        {
            return await _clients.GetClients(email);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<bool> AddClient([FromBody] ClientRequestDto body)
        {
            return await _clients.AddCient(body);
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<bool> UpdateClient([FromBody] ClientRequestDto body)
        {
            return await _clients.UpdateClient(body);
        }

        [Authorize]
        [HttpDelete("remove")]
        public async Task<bool> RemoveClient(long id)
        {
            return await _clients.RemoveClient(id);
        }
    }
}
