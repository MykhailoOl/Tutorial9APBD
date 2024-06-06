using task9.DTO;
using task9.Entities;

namespace task9.Repositories;

public interface ITripRepository
{
    Task<List<TripDTO>> GetTripsAsync(int page, int pageSize);
    Task<int> GetTotalTripsCountAsync();
    Task<Trip> GetTripByIdAsync(int idTrip);
}
