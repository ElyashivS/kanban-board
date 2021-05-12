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


            bool a1 = boardDalController.Insert(new BoardDTO(777, "lcuk", "me"));
            bool a2 = userDalController.Insert(new UserDTO("Hirtut@gmail.com", "qwe123"));
            Console.WriteLine(a1);
            Console.WriteLine(a2);
            bool b1 = boardDalController.Update(777, "Creator", "BIBABU");
            bool b2 = userDalController.Update("Hirtut@gmail.com", "password", "asd987");
            Console.WriteLine(b1);
            Console.WriteLine(b2);
            bool c1 = taskController.Insert(new TaskDTO(123, "Hirtut@gmail.com", new DateTime(2025, 1, 1), new DateTime(2026, 1, 1), "ToDo", "Now", "backlog"));
            bool c2 = columnDalController.Insert(new ColumnDTO(777, "backlog", 70));
            Console.WriteLine(c1);
            Console.WriteLine(c2);
            bool d1 = taskController.Update(123, "backlog", "Title", "NotToDo");
            bool d2 = columnDalController.Update("backlog", "ColumnLimiter", "44");
            Console.WriteLine(d1);
            Console.WriteLine(d2);
        }
    }
}
