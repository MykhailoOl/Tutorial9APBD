using Microsoft.EntityFrameworkCore;
using task9.Context;
using task9.Entities;

namespace task9.Repositories;

public class ClientTripRepository : IClientTripRepository
{
    private readonly ScaffoldContext _context;

    public ClientTripRepository(ScaffoldContext context)
    {
        _context = context;
    }

    public async Task<Client> GetClientByPeselAsync(string pesel)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == pesel);
    }

    public async Task<ClientTrip> GetClientTripAsync(int clientId, int tripId)
    {
        return await _context.ClientTrips.FirstOrDefaultAsync(ct => ct.IdClient == clientId && ct.IdTrip == tripId);
    }

    public async Task AddClientTripAsync(ClientTrip clientTrip)
    {
        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync();
    }
}