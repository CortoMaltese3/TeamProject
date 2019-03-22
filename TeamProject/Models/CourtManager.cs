using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
namespace TeamProject.Models
{


    public class CourtManager : TableManager<Court>
    {
        public CourtManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "FindById", "Court.id = @id" },
                { "InsertQuery",
                    "INSERT INTO Court ([BranchId],[Name],[ImageCourt],[Description],[MaxPlayers],[Price]) " +
                    "VALUES (@BranchId,@Name,@ImageCourt,@Description,@MaxPlayers,@Price)" +
                    "SELECT * FROM Court WHERE Court.Id = (SELECT SCOPE_IDENTITY())"},
                { "RemoveQuery",
                    "DELETE FROM Court WHERE Id = @Id" },
                { "UpdateQuery",
                    "UPDATE Court SET " +
                    "[BranchId]=@BranchId, [Name]=@Name, [ImageCourt]=@ImageCourt,[Description]=@Description,[MaxPlayers]=@MaxPlayers, [Price]=@Price " +
                    "WHERE Id = @Id"}
            };
            _db = projectDbContext;
        }

        public override IEnumerable<Court> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<Court> courts = null;
            var courtDictionary = new Dictionary<int, Court>();
            _db.UsingConnection((dbCon) =>
            {
                courts = dbCon.Query<Court, Branch, TimeSlot, Court>(
                    "SELECT * FROM Court " +
                    "INNER JOIN Branch ON Court.BranchId = Branch.Id " +
                    "LEFT JOIN Timeslot ON Court.Id = TimeSlot.CourtId " + (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                    (court, branch, timeslot) =>
                    {
                        Court courtEntry;

                        if (!courtDictionary.TryGetValue(court.Id, out courtEntry))
                        {
                            courtEntry = court;
                            courtEntry.TimeSlot = new List<TimeSlot>();
                            courtDictionary.Add(courtEntry.Id, courtEntry);
                        }
                        if (timeslot != null)
                        {
                            courtEntry.TimeSlot.Add(timeslot);
                        }
                        courtEntry.Branch = branch;
                        return courtEntry;
                    },
                    splitOn: "id",
                    param: parameters)
                    .Distinct();
            });

            return courts;
        }
        public IEnumerable<Court> AllCourtsSameBranch(int courtId)
        {
            var branchId = Find(courtId).Branch.Id;
            return BranchCourts(branchId);
        }
        public IEnumerable<Court> BranchCourts(int branchId)
        {
            return Get("branchId=@branchId", new { branchId });
        }

    }

}