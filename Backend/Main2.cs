using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend
{
    class Main2
    {
        static void Main(string[] args)
        {
            UserDalController userController = new UserDalController();
            bool ans = userController.Update("asddas@gmail.com", "password", "98765");
            Console.WriteLine(ans);
        }
    }
}
