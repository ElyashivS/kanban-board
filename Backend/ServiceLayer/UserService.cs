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
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);  // Put this in every class

        public UserService()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Debug("Service is going up");

            userController = new UserController();
        }



        public Response Register(string email, string password)
        {
            try
            {
                userController.Register(email, password);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }

        }

        public Response<User> Login(string email, string password)
        {
            try
            {
                BuisnessLayer.User user = userController.Login(email, password);
                return Response<User>.FromValue(new User(user.email));
            }
            catch (Exception e)
            {
                return Response<User>.FromError(e.Message);
            }
        }


        public Response Logout(string email)
        {

            try
            {
                
                userController.Logout(email);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response ValidateUserLoggin(string email)
        {
            try {
                userController.ValidateUserLoggin(email);
                return new Response();
            }

            catch (Exception e)
            {
                return new Response(e.Message);
            }



            }
    }
}
