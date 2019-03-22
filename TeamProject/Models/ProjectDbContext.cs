using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
namespace TeamProject.Models
{
    public class ProjectDbContext
    {
        public static readonly string ACTION_STATUS_OK = "Ok";
        public static readonly string ACTION_STATUS_ERROR = "Error";
        private string _connectionString;
        public string LastActionStatus { get => LastActionError.Equals(string.Empty) ? ACTION_STATUS_OK : ACTION_STATUS_ERROR; }
        public string LastActionError { get; private set; }
        public ProjectDbContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            Courts = new CourtManager(this);
            Branches = new BranchManager(this);
            BranchFacilities = new BranchFacilitiesManager(this);
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
            LastActionError = string.Empty;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    action(sqlConnection);
                }
            }
            catch (DbException e)
            {
                LastActionError = e.Message;
            }
        }
        public CourtManager Courts { get; set; }
        public BranchManager Branches { get; set; }
        public BranchFacilitiesManager BranchFacilities { get; set; }
        public UserManager Users { get; set; }
        public UserRolesManager UserRoles { get; set; }
        public FacilityManager Facilities { get; set; }
        public TimeSlotManager TimeSlots { get; set; }
        public ReviewManager Reviews { get; set; }
        public BookingManager Bookings { get; set; }
        public RoleManager Roles { get; set; }
    }
}