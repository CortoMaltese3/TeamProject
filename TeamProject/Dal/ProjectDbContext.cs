using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using TeamProject.Managers;
using TeamProject.Models;

namespace TeamProject.Dal
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
            Roles = new RoleManager(this);
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
        public IDatabaseActions<Court> Courts { get; set; }  
        public IDatabaseActions<Branch> Branches { get; set; }
        public IDatabaseActions<BranchFacilities> BranchFacilities { get; set; }
        public IDatabaseActions<User> Users { get; set; }
        public IDatabaseActions<UserRoles> UserRoles { get; set; }
        public IDatabaseActions<Facility> Facilities { get; set; }
        public IDatabaseActions<TimeSlot> TimeSlots { get; set; }
        public IDatabaseActions<Review> Reviews { get; set; }
        public IDatabaseActions<Booking> Bookings { get; set; }
        public IDatabaseActions<Role> Roles { get; set; }
    }
}