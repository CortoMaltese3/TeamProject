using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class BranchManager
    {
        private ApplicationDbContext _db;

        public BranchManager()//ApplicationDbContext db)
        {
            _db = new ApplicationDbContext();
        }

        /// <summary>
        /// Returns List of branches near to a given latitude, longtitude
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longtitude"></param>
        /// <returns></returns>
        public List<Branch> GetNearestBranches(double latitude, double longtitude, double distanceInMeters = 5000)
        {
            // TODO
            // Call GetBranchesDistance Procedure from Sql Server and Return List With branches
            // Procedure Parameters:
            //   @Latitude float,
            //   @Longtitude float,
            //   @distance float
            // Returned Columns => ID, Distance
            throw new NotImplementedException();
        }
    }
}