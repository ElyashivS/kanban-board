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
            TaskDalController TaskController = new TaskDalController();
            ColumnDalController columnDalController = new ColumnDalController();
            BoardDalController BoardDalController = new BoardDalController();
           // bool ans2 = BoardDalController.Insert(new BoardDTO(1, "boardy", "chris"));
           // bool ans1 = columnDalController.Insert(new ColumnDTO(1, "back log", 4));
            TaskDTO a = new TaskDTO(1, "chris", DateTime.Now, DateTime.Now, "wow", "wowow", "back log");
          // bool ans=TaskController.Insert(a);
           bool  ans = TaskController.Delete(a);



           // Console.WriteLine(ans1);
           // Console.WriteLine(ans2);
            Console.WriteLine(ans);


        }
    }
}
