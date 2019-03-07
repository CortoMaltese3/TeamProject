using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace TeamProject.Models
{
    public class BookingManager : TableManager<Booking>
    {

        public BookingManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "FindById", "Booking.id = @id" },
                { "InsertQuery",
                    "INSERT INTO Booking ([CourtId],[UserId],[BookedAt],[Duration]) " +
                    "VALUES (@CourtId,@UserId,@BookedAt,@Duration) " +
                    "SELECT * FROM Booking WHERE Booking.Id = (SELECT SCOPE_IDENTITY())"},
                { "RemoveQuery",
                    "DELETE FROM Booking WHERE Id = @Id" },
                { "UpdateQuery",
                    "UPDATE Booking SET " +
                    "[CourtId]=@CourtId,[UserId]=@UserId,[BookedAt]=@BookedAt,[Duration]=@Duration " +
                    "WHERE Id = @Id"}
            };
            _db = projectDbContext;
        }

        public override IEnumerable<Booking> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<Booking> Bookings = null;

            _db.UsingConnection((dbCon) =>
            {
                Bookings = dbCon.Query<Booking, Court, User, Booking>(
                "SELECT * FROM Booking " +
                "INNER JOIN Court ON Booking.CourtId = Court.Id " +
                "INNER JOIN [User] ON Booking.UserId = [User].Id" +
                (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                (Booking, court, user) =>
                {

                    Booking.Court = court;
                    Booking.User = user;
                    return Booking;

                },

            splitOn: "id",
                    param: parameters)
                    .Distinct()
                    .ToList();
            });

            return Bookings;
        }

    }
}
