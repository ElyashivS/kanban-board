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
            BoardDalController BoardController = new BoardDalController();

            BoardController.Insert(new BoardDTO(1,"backlog","fucker"));
            BoardController.Insert(new BoardDTO(2, "backlog", "ucker"));
            BoardController.Insert(new BoardDTO(3, "backlog", "fcker"));
            BoardController.Insert(new BoardDTO(4, "backlog", "fuker"));
            BoardController.Insert(new BoardDTO(5, "backlog", "fucer"));


            List<DTO> a = BoardController.Select();
            foreach (DTO i in a)
            {
                Console.WriteLine(((BoardDTO)i).ID);
            }
        }
    }
}
