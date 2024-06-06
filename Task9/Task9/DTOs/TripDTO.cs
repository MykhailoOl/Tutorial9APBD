namespace task9.DTO;

public class TripDTO
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public IEnumerable<CountryDTO> Countries { get; set; }
    public IEnumerable<ClientDTO> Clients { get; set; } 
}