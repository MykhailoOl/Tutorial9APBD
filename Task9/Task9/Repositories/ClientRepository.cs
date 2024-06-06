using Microsoft.EntityFrameworkCore;
using task9.Context;
using task9.Entities;

namespace task9.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ScaffoldContext _dbContext;

    public ClientRepository(ScaffoldContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Client> FindByIdAsync(int id)
    {
        return await _dbContext.Clients.FindAsync(id);
    }
    public async Task DeleteAsync(Client client)
    {
        _dbContext.Clients.Remove(client);
        await _dbContext.SaveChangesAsync();
    }
    
}