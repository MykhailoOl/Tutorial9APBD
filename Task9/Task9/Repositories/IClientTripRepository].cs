using task9.Entities;

namespace task9.Repositories;

public interface IClientTripRepository
{
    Task<Client> GetClientByPeselAsync(string pesel);
    Task<ClientTrip> GetClientTripAsync(int clientId, int tripId);
    Task AddClientTripAsync(ClientTrip clientTrip);
}