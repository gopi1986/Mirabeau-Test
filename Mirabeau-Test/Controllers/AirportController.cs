using Airport.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Mirabeau_Test.Models;


namespace Mirabeau_Test.Controllers
{
    public class AirportController : Controller
    {
        readonly IAirportService _airportService;
        
        public AirportController()
        {
            _airportService = new AirportService();
        }

        public ActionResult List()
        {
            var airports = Airports;

            if (airports == null)
            {
                airports = _airportService.GetAirportListInEU();
                HttpContext.Response.Headers.Add("from-feed", "true");
                Airports = airports;
            }

            var countries = airports?.GroupBy(f => f.iso).Select(f => f.Key).OrderBy(f => f).ToList();

            var model = new AirportViewModel {Airports = airports, Countries = countries};

            return View(model);
        }


        public JsonResult FilterByCountry(string country)
        {
            var airports = Airports ?? _airportService.GetAirportListInEU();

            if (airports == null)
                return null;

            var filteredAirport = !string.IsNullOrEmpty(country)
                ? airports.Where(f => f.iso == country).ToList()
                : airports;

            return Json(filteredAirport, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Distance()
        {
            var airports = Airports;

            if (airports == null)
            {
                airports = _airportService.GetAirportListInEU();
                Airports = airports;
            }

            var airportList = airports?.Select(f => f.iata).OrderBy(f => f).ToList();

            var model = new DistanceViewModel() { Airports = airportList };

            return View(model);
        }

        public JsonResult CalculateDistance(string fromIata, string toIata)
        {            
            if (string.IsNullOrEmpty(fromIata) || string.IsNullOrEmpty(toIata))
                return null;

            var airports = Airports ?? _airportService.GetAirportListInEU();

            if (airports == null)
                return null;

            var fromAirport = airports.FirstOrDefault(f => f.iata.ToLower() == fromIata.ToLower());
            var toAirport = airports.FirstOrDefault(f => f.iata.ToLower() == toIata.ToLower());

            if (fromAirport == null || toAirport == null)
                return null;

            var fromCoordinates = new Coordinates(Convert.ToDouble(fromAirport.lat), Convert.ToDouble(fromAirport.lon));
            var toCoordinates = new Coordinates(Convert.ToDouble(toAirport.lat), Convert.ToDouble(toAirport.lon));
            var distance = CoordinateDistanceCalculator.Distance(fromCoordinates, toCoordinates);

            return Json(distance.ToString("#.##"), JsonRequestBehavior.AllowGet);
        }
        
        List<Airport.DTO.Airport> Airports
        {
            get => HttpContext.Cache["Airports"] != null
                ? (List<Airport.DTO.Airport>)HttpContext.Cache["Airports"]
                : null;
            set => HttpContext.Cache.Insert("Airports", value, null, DateTime.Now.AddMinutes(5),
                System.Web.Caching.Cache.NoSlidingExpiration);
        }
    }
}