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
            Courts = new CourtManager(this);
            Branches = new BranchManager(this);
            Users = new UserManager(this);
            Facilities = new FacilityManager(this);
            UserRoles = new UserRolesManager(this);
            TimeSlots = new TimeSlotManager(this);
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
        public CourtManager Courts { get; set; }
        public BranchManager Branches { get; set; }
        public UserManager Users { get; set; }
        public UserRolesManager UserRoles { get; set; }
        public FacilityManager Facilities { get; set; }
        public TimeSlotManager TimeSlots { get; set; }
        public ReviewManager Reviews { get; set; }
        public BookingManager Bookings { get; set; }
        public RoleManager Roles { get; set; }
    }
}