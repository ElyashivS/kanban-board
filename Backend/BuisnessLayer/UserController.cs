using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class UserController
    {
        Dictionary<string, User> Users;
        UserDalController UsersTable = new UserDalController();
        
        // Constructor
        public UserController()
        {
            Users = new Dictionary<string, User>();
        }
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="email">The email of the new user</param>
        /// <param name="password">The password of the new user</param>
        public void Register(string email, string password)
        {
            email = email.Trim().ToLower();
            if (Users.ContainsKey(email))
                throw new ArgumentException("email is already in the system");
            
            Users.Add(email, new User(email, password));
            UserDTO toInsert = new UserDTO(email, password);
            UsersTable.Insert(toInsert);
        }
        /// <summary>
        /// Login the user
        /// </summary>
        /// <param name="email">The email</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public User Login(string email, string password)
        {
            email = email.Trim().ToLower();
            if (!Users.ContainsKey(email))
                throw new Exception("email does not exsist");
            Users[email].Login(email, password);

            return Users[email];
        }
        /// <summary>
        /// Log out the user
        /// </summary>
        /// <param name="email">The email</param>
        public void Logout(string email)
        {
            email = email.Trim().ToLower();
            if (!Users.ContainsKey(email))
            {
                throw new Exception("email does not exsist");
            }
            Users[email].Logout();
        }
        /// <summary>
        /// Check if the user can be log in
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <returns></returns>
        public bool ValidateUserLoggin(string email)
        {
            email = email.Trim().ToLower();
            if (!Users.ContainsKey(email))
            {
                throw new Exception("email does not exsist");
            }
            else if (!Users[email].ValidateUserLoggin())
                throw new Exception("user is logged off");

            return true;
        }
        /// <summary>
        /// Removes all user persistent data.
        /// </summary>
        public void DeleteData()
        {
            try
            {
                UsersTable.DeleteUserTable();
            }
            catch
            {
                throw new Exception("could not delete all the users");
            }
        }
        /// <summary>
        /// This method loads the data from the persistance.
        /// </summary>
        public void LoadData()
        {
            List<DTO> toload=UsersTable.Select();
            foreach (UserDTO user in toload)
            {
                
                RegisterForLoad(user.Email, user.Password);
            }
        }
        /// <summary>
        /// Make a list of all the users
        /// </summary>
        /// <returns>The list of the users</returns>
        public List<User> UsersToList()
        {
            return Users.Values.ToList();
        }
        /// <summary>
        /// Brings all users email
        /// </summary>
        /// <returns>The emails of the users</returns>
        public List<string> BringAllUsersEmail()
        {
            List<string> toreturn = new List<string>();
            List<User> a = UsersToList();
            foreach (User b in a)
            {
                toreturn.Add(b.email);
            }
            return toreturn;
        }
        /// <summary>
        /// Load all the registered emails
        /// </summary>
        /// <param name="email">The emails</param>
        /// <param name="password">The passwords</param>
        private void RegisterForLoad(string email, string password)
        {
            email = email.Trim().ToLower();
            if (Users.ContainsKey(email))
                throw new ArgumentException("email is already in the system");

            Users.Add(email, new User(email, password));
        }
    }
}
