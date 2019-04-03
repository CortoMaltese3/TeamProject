using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TeamProject.Dal;
using TeamProject.Models;

namespace TeamProject.Managers
{
    public class ReviewManager : TableManager<Review>
    {

        public ReviewManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "InsertQuery",
                    "INSERT INTO Review ([CourtId],[UserId],[Rating],[Comment],[CommentAt]) " +
                    "VALUES (@CourtId,@UserId,@Rating,@Comment,@CommentAt) " +
                    "SELECT * FROM Review WHERE Review.Id = (SELECT SCOPE_IDENTITY())"},
                { "UpdateQuery",
                    "UPDATE Review SET " +
                    "[CourtId]=@CourtId,[UserId]=@UserId,[Rating]=@Rating,[Comment]=@Comment,[CommentAt]=@CommentAt " +
                    "WHERE Id = @Id"}
            };
            _db = projectDbContext;
        }

        public override IEnumerable<Review> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<Review> Reviews = null;

            _db.UsingConnection((dbCon) =>
            {
                Reviews = dbCon.Query<Review, Court, User, Review>(
                "SELECT * FROM Review " +
                "INNER JOIN Court ON Review.CourtId = Court.Id " +
                "LEFT JOIN [User] ON Review.UserId = [User].Id" +
                (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                (Review, court, user) =>
                {

                    Review.Court = court;
                    Review.User = user;
                    return Review;

                },

            splitOn: "id",
                    param: parameters)
                    .Distinct()
                    .ToList();
            });

            return Reviews;
        }

    }
}
