using Microsoft.EntityFrameworkCore;
using task9.Context;
using task9.DTO;
using task9.Entities;

namespace task9.Repositories;

public class TripRepository : ITripRepository
    {
        private readonly ScaffoldContext _context;

        public TripRepository(ScaffoldContext context)
        {
            _context = context;
        }

        public async Task<List<TripDTO>> GetTripsAsync(int page, int pageSize)
        {
            return await _context.Trips
                .OrderByDescending(t => t.DateFrom)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TripDTO
                {
                    Name = t.Name,
                    Description = t.Description,
                    DateFrom = t.DateFrom,
                    DateTo = t.DateTo,
                    MaxPeople = t.MaxPeople,
                    Countries = t.IdCountries.Select(ct => new CountryDTO
                    {
                        Name = ct.Name
                    }).ToList(),
                    Clients = t.ClientTrips.Select(ct => new ClientDTO
                    {
                        FirstName = ct.IdClientNavigation.FirstName,
                        LastName = ct.IdClientNavigation.LastName
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<int> GetTotalTripsCountAsync()
        {
            return await _context.Trips.CountAsync();
        }

        public async Task<Trip> GetTripByIdAsync(int idTrip)
        {
            return await _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == idTrip);
        }
    }