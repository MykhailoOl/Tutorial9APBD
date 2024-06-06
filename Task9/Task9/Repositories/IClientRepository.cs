using task9.Entities;

namespace task9.Repositories;

public interface IClientRepository
{
    Task<Client> FindByIdAsync(int id);
    Task DeleteAsync(Client client);
}