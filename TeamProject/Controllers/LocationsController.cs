using System;
using System.Collections.Generic;
using TeamProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace TeamProject
{
    
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
