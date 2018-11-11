using System.Collections.Generic;

namespace Airport.BL
{
    public interface IAirportService
    {
        List<DTO.Airport> GetAirportListInEU();
    }
}
