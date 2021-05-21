using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTO;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer.Objects;
using Task = IntroSE.Kanban.Backend.ServiceLayer.Task;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Service a = new Service();
          //  a.DeleteData();
          //  a.Register("ari@gmail.com","Aa12");
            //  a.Register("ri@gmail.com", "Aa12");
           // a.Register("i@gmail.com", "Aa12");

             // a.Login("ari@gmail.com", "Aa12");
            // a.Login("ri@gmail.com", "Aa12");
            //a.Login("i@gmail.com", "Aa12");


            //  a.AddBoard("ari@gmail.com", "boardy");

             // a.JoinBoard("ri@gmail.com", "ari@gmail.com", "boardy");
           // a.JoinBoard("i@gmail.com", "ari@gmail.com", "boardy");

           //  a.AddTask("i@gmail.com", "ari@gmail.com", "boardy", "title", "description", new DateTime(2025, 1, 1));
          //  a.AdvanceTask("i@gmail.com", "ari@gmail.com", "boardy", 0, 1);
          //  a.AssignTask("i@gmail.com", "ari@gmail.com", "boardy", 1, 1, "ri@gmail.com");
            
          // a.AdvanceTask("ri@gmail.com", "ari@gmail.com", "boardy", 1, 1);

            // a.AddTask("ri@gmail.com", "ari@gmail.com", "boardy", "itle", "description", new DateTime(2025, 1, 1));
            // a.AdvanceTask("ri@gmail.com", "ari@gmail.com", "boardy", 0, 2);

            // a.AddBoard("ri@gmail.com", "board");
            // a.AddTask("ri@gmail.com", "ri@gmail.com", "board", "titl", "description", new DateTime(2025, 1, 1));
            // a.AdvanceTask("ri@gmail.com", "ri@gmail.com", "board", 0, 1);


            // a.JoinBoard("ari@gmail.com", "ri@gmail.com", "board");
            // a.AddTask("ari@gmail.com", "ri@gmail.com", "board", "tit", "description", new DateTime(2025, 1, 1));
            //a.AssignTask("ari@gmail.com", "ri@gmail.com", "board", 0, 2, "ri@gmail.com");
            // a.AdvanceTask("ri@gmail.com", "ri@gmail.com", "board", 0, 2);




           // a.LoadData();
          //  a.Login("ri@gmail.com", "Aa12");
          //  a.Login("ari@gmail.com", "Aa12");
           // a.RemoveBoard("ri@gmail.com", "ri@gmail.com", "board");
           // a.RemoveBoard("ri@gmail.com", "ari@gmail.com", "boardy");
           //a.RemoveBoard("ari@gmail.com", "ari@gmail.com", "boardy");
           
            //Response < IList<Task> > k= a.InProgressTasks("ri@gmail.com");
            
          // foreach (Task l in k.Value)
          // {
            //    Console.WriteLine(l.Title);
          //  }

















        }
    }
}
