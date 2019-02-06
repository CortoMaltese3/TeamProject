using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TeamProject
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        // GET: api/Locations
        [HttpGet]
        public IEnumerable<Location> Get()
        {

            return new List<Location>() {
                new Location() { Latitude = 37.9838096, Longitude = 23.727538800000048, Description = "Αθηνα" },
                new Location() { Latitude = 37.990849, Longitude = 23.738339, Description = "Βυρωνας" },
                new Location() { Latitude =  37.990840, Longitude = 23.73833888, Description = "Τυχαιο" },
            };
        }

    }
}
