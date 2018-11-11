using System.Collections.Generic;
using Airport.DAL;

namespace Airport.BL
{
    public class AirportService : IAirportService
    {
        readonly IAirportRepository _airportRepository;
        public AirportService()
        {
            _airportRepository = new AirportRepository();
        }
        public List<DTO.Airport> GetAirportListInEU()
        {
            var airports = _airportRepository.GetAirportListInEU();
            return airports;
        }
    }
}
