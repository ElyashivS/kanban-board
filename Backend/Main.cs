﻿using System;
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

            // a.Register("ari@gmail.com", "Aa123123");
            // a.Register("ri@gmail.com", "Aa123123");
            // a.Register("i@gmail.com", "Aa123123");
            //a.LoadData();

          //  a.Login("ari@gmail.com", "Aa123123");
             //a.Login("ri@gmail.com", "Aa123123");
            // a.AddBoard("ari@gmail.com", "boardy");
           // a.JoinBoard("ri@gmail.com", "ari@gmail.com", "boardy");

           // a.LimitColumn("ari@gmail.com", "ari@gmail.com", "boardy", 0, 5);
             // a.AddTask("ari@gmail.com", "ari@gmail.com", "boardy", "wow", "description", new DateTime(2023, 12, 3));
            // a.AddTask("ari@gmail.com", "ari@gmail.com", "boardy", "wow", "description", new DateTime(2023, 12, 3));
            a.LoadData();
          a.DeleteData();           

            // a.Register("ri@gmail.com", "Aa123123");
            //a.Login("ri@gmail.com", "Aa123123");
            //a.Register("i@gmail.com", "Aa123123");
            // a.Login("i@gmail.com", "Aa123123");
            // a.LimitColumn("ari@gmail.com", "ari@gmail.com", "boardy", 0, 5);
            //a.LimitColumn("ari@gmail.com", "ari@gmail.com", "boardy", 1, 5);

            // a.JoinBoard("i@gmail.com", "ari@gmail.com", "boardy");

            // a.LimitColumn("ari@gmail.com", "ari@gmail.com", "boardy", 0, 0);

            // a.RemoveBoard("ari@gmail.com", "ari@gmail.com", "boardy");

            // a.UpdateTaskTitle("ari@gmail.com", "ari@gmail.com","boardy",0,1,"new title");
            //  a.AddTask("ari@gmail.com", "ari@gmail.com", "boardy", "wow", "description", new DateTime(2023, 12, 3));
            // a.UpdateTaskDescription("ari@gmail.com", "ari@gmail.com", "boardy", 0, 2, "wtf");
            //  a.RemoveBoard("ari@gmail.com", "ari@gmail.com", "boardy");
            // a.LimitColumn("ari@gmail.com", "ari@gmail.com", "boardy", 0, 5);
            // a.AdvanceTask("ari@gmail.com", "ari@gmail.com", "boardy", 0, 1);
            //a.AdvanceTask("ari@gmail.com", "ari@gmail.com", "boardy",0, 1);



        }
    }
}
