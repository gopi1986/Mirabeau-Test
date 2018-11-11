using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace Airport.DAL
{
    public class AirportRepository : IAirportRepository
    {
        public const string AirportJsonFeedUrl =
            "https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json";

        public const string EuropeUnionContinentCode = "EU";

        public List<DTO.Airport> GetAirportListInEU()
        {
            string feed = GetAirportJsonFeed();

            return feed != null
                ? JsonConvert.DeserializeObject<List<DTO.Airport>>(feed).Where(f => f.continent == EuropeUnionContinentCode).ToList()
                : null;
        }

        string GetAirportJsonFeed()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(AirportJsonFeedUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    return responseContent.ReadAsStringAsync().Result;
                }
            }

            return null;
        }        
    }
}
