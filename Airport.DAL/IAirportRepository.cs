using System.Collections.Generic;

namespace Airport.DAL
{
    public interface IAirportRepository
    {
        List<DTO.Airport> GetAirportListInEU();

    }
}
