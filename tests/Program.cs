﻿using System;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace tests
    
{
    class Program
    {
        static void Main(string[] args)
        {
            Service a = new Service();
            
            Response b = a.Register("arioshry@gmail.com", "Abc1");
            Response<User> c = a.Login("arioshry@gmail.com", "Abc1");
            Response d=a.AddBoard("arioshry@gmail.com", "boardy");
            Response<Task> e = a.AddTask("arioshry@gmail.com", "boardy", "nothing", "nothingness", DateTime.Today);



                 if (b.ErrorOccured)
                Console.WriteLine(b.ErrorMessage);
           
            if (c.ErrorOccured)
                Console.WriteLine(c.ErrorMessage);
            else
                Console.WriteLine(c.Value.Email);
            
            if (d.ErrorOccured)
                Console.WriteLine(d.ErrorMessage);
           
            if (e.ErrorOccured)
                Console.WriteLine(c.ErrorMessage);
            else
                Console.WriteLine(e.Value.Title);


            ;
            
            Console.ReadLine();


        }
    }
}