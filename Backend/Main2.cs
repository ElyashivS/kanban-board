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
            TaskDalController taskController = new TaskDalController();
            ColumnDalController columnDalController = new ColumnDalController();
            BoardDalController boardDalController = new BoardDalController();
            UserDalController userDalController = new UserDalController();
            //// ColumnDTO a = new ColumnDTO(777, "backlog", 70);
            //  UserDTO a = new UserDTO("Hirtut@gmail.com", "qwe123");
            //  bool c2 = userDalController.Insert(a);
            //  UserDTO b = userDalController.SpecificSelect("Hirtut@gmail.com");

            // bool c1 = userDalController.Delete(a);
            // bool a=taskController.Insert(new TaskDTO(1,"backlog",1,"chris",DateTime.Now,DateTime.Now,"its a title","its a description"));
            // bool b = taskController.Insert(new TaskDTO(1, "backlog", 2, "chris", DateTime.Now, DateTime.Now, "its not a title", "its not a description"));
            //TaskDTO c = taskController.SpecificSelect(1,"backlog",1);
            // Console.WriteLine(b.Password);

            //bool c = columnDalController.Insert(new ColumnDTO(1, "in progress", 10));

            bool a1 = boardDalController.InsertToAsigneeList(1, "Hirtut@gmail.com");
            //  bool a2 = userDalController.Insert(new UserDTO("Hirtut@gmail.com", "qwe123"));
              Console.WriteLine(a1);
            //  Console.WriteLine(a2);
            //  bool b1 = boardDalController.Update(777, "Creator", "BIBABU");
            // bool b2 = userDalController.Update("Hirtut@gmail.com", "password", "asd987");
            //  Console.WriteLine(b1);
            //  Console.WriteLine(b2);

            // bool c2 = columnDalController.Insert(new ColumnDTO(777, "backlog", 70));
           // Console.WriteLine(a);
            //Console.WriteLine(b);
          // Console.WriteLine(c2);
          // bool d1 = taskController.Update(123, "backlog", "Title", "NotToDo");
           // bool d2 = columnDalController.Update("backlog", "ColumnLimiter", "44");
           // Console.WriteLine(d1);
            //Console.WriteLine(d2);
        }
    }
}
