using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BuisnessLayer;
using log4net;
using log4net.Config;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class UserService
    {
       
        UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // Constructor
        public UserService()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Debug("Service is going up");

            userController = new UserController();
        }
        /// <summary>
        /// This method loads the user data from the persistance.
        /// </summary>
        /// <returns>A response object</returns>
        public Response LoadData()
        {
            try
            {
                userController.LoadData();
                log.Info("Users Data has been loaded");
                return new Response();
            }
            catch(Exception e)
            {
                log.Warn("Failed to load Data");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Removes all user persistent data.
        /// </summary>
        /// <returns></returns>
        public Response DeleteData()
        {
            try
            {
                userController.DeleteData();
                log.Info("Users Data has been deletes successfuly");
                return new Response();
            }
            catch(Exception e)
            {
                log.Warn("Failed to delete d");
                return new Response(e.Message);
            }
        }
        ///<summary>This method registers a new user to the system.</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        ///<returns cref="Response">The response of the action</returns>
        public Response Register(string email, string password)
        {
            try
            {
                userController.Register(email, password);
                log.Info("User with the mail " + email + " has been created");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn("Failed to register");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            try
            {
                BuisnessLayer.User user = userController.Login(email, password);
                log.Info("The user with the mail " + email + " logged in");
                return Response<User>.FromValue(new User(user.email));
            }
            catch (Exception e)
            {
                log.Warn("Failed to log in");
                return Response<User>.FromError(e.Message);
            }
        }
        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            try
            {
                userController.Logout(email);
                log.Info("The user with the mail " + email + " logged out");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn("Failed to logout");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Check if the user can be log in
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <returns>A response object</returns>
        public Response ValidateUserLoggin(string email)
        {
            try {
                userController.ValidateUserLoggin(email);
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn($"user {email} isnt logged in");
                return new Response(e.Message);
            }
         }
        /// <summary>
        /// Brings all users emails
        /// </summary>
        /// <returns>List of all the emails of the users</returns>
        public List<string> BringAllUsersEmail()
        {
            return userController.BringAllUsersEmail();
        }
    }
}
