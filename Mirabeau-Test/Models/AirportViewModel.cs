using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirabeau_Test.Models
{
    public class AirportViewModel
    {
        public List<Airport.DTO.Airport> Airports { get; set; }
        public List<string> Countries { get; set; }
    }
}