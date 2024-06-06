using Microsoft.AspNetCore.Mvc;
using task9.Repositories;

namespace task9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientById(int id)
        {
            var client = await _clientRepository.FindByIdAsync(id);
            if (client == null)
            {
                return NotFound("NotFound");
            }

            await _clientRepository.DeleteAsync(client);

            return NoContent();
        }
    }
}