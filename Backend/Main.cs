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

            a.Register("ari@gmail.com","Aa12");
            a.Register("ri@gmail.com", "Aa12");
              a.Login("ari@gmail.com", "Aa12");
              a.Login("ri@gmail.com", "Aa12");
             a.AddBoard("ari@gmail.com", "boardy");
            a.Logout("ari@gmail.com");
            a.Login("ri@gmail.com", "Aa12");
            a.AddBoard("ri@gmail.com", "boardy");
            a.JoinBoard("ri@gmail.com", "ari@gmail.com", "boardy");
            Response<IList<String>> b = a.GetBoardNames("ri@gmail.com");
            foreach (String s in b.Value)
            {
                Console.WriteLine(s);
            }
            //  a.JoinBoard("ri@gmail.com", "ari@gmail.com", "boardy"); ;
            // a.AddTask("ari@gmail.com", "ari@gmail.com", "boardy", "title", "description", new DateTime(2025, 1, 1));
            // a.AssignTask("ari@gmail.com", "ari@gmail.com", "boardy", 0, 1, "ri@gmail.com");
            // a.LimitColumn("ri@gmail.com", "ari@gmail.com", "boardy", 0, 5);
            //  a.LoadData();
            //    a.Login("ari@gmail.com", "Aa12");
            //a.AddTask("ari@gmail.com", "ari@gmail.com", "boardy", "itle", "description", new DateTime(2025, 1, 1));
             //a.DeleteData();







        }
    }
}
