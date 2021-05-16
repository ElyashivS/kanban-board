using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntroSE.Kanban.Backend.ServiceLayer;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Service a = new Service();
           a.Register("ari@gmail.com", "Aa123123");
            a.Login("ari@gmail.com", "Aa123123");
           a.AddBoard("ari@gmail.com","boardy");
            a.Register("ri@gmail.com", "Aa123123");
            a.Login("ri@gmail.com", "Aa123123");
            a.JoinBoard("ri@gmail.com", "ari@gmail.com", "boardy");
            //  a.RemoveBoard("ari@gmail.com", "ari@gmail.com", "boardy");
            //  a.AddTask("ari@gmail.com", "ari@gmail.com", "boardy", "wow", "description", new DateTime(2023, 12, 3));
            a.AddTask("ri@gmail.com", "ari@gmail.com", "oardy", "wow", "description", new DateTime(2023, 12, 3));

        }
    }
}
