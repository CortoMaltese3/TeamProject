using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamProject.Models;

namespace TeamProject.Database
{
    public class DatabaseDb
    {
        private static List<User> users;
        static DatabaseDb()
        {
            users = new List<User>() { new User() { id = 1, Firstname = "george", Lastname = "xiros", Email = "geo.xiros@gmail.com" } };
        }
        public IEnumerable<User> Users()
        {
            return users;
        }

        public User User(int id)
        {
            return users.Where(u => u.id == id).FirstOrDefault();
        }

        public bool Add(User user)
        {
            user.id = users.Max(u => u.id) + 1;
            users.Add(user);
            return true;
        }

        public bool Update(User user)
        {

            User userToUpdate = User(user.id);

            userToUpdate.Firstname = user.Firstname;
            userToUpdate.Lastname = user.Lastname;
            userToUpdate.Email = user.Email;

            return true;
        }
        public bool RemoveUser(int id)
        {
            users.Remove(User(id));
            return true;
        }

    }
}
