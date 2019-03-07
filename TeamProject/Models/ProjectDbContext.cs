using System;
using System.Configuration;
using System.Data.SqlClient;
namespace TeamProject.Models
{
    public class ProjectDbContext
    {
        private string _connectionString;
        public ProjectDbContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            Court = new CourtManager(this);
            Branch = new BranchManager(this);
            User = new UserManager(this);
            Facility = new FacilityManager(this);
            UserRoles = new UserRolesManager(this);
            TimeSlot = new TimeSlotManager(this);
            Reviews = new ReviewManager(this);
            Bookings = new BookingManager(this);
            Roles= new RoleManager(this);
        }
        public void UsingConnection(Action<SqlConnection> action)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    action(sqlConnection);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public CourtManager Court { get; set; }
        public BranchManager Branch { get; set; }
        public UserManager User { get; set; }
        public UserRolesManager UserRoles { get; set; }
        public FacilityManager Facility { get; set; }
        public TimeSlotManager TimeSlot { get; set; }
        public ReviewManager Reviews { get; set; }
        public BookingManager Bookings { get; set; }
        public RoleManager Roles { get; set; }
    }
}