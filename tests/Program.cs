using System;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace tests
    
{
    class Program
    {
        static void Main(string[] args)
        {
            Service a = new Service();
            
            Response s = a.Register("arioshry@gmail.com", "Abc1");
            Response<User> c = a.Login("arioshry@gmail.com", "Abc1");
            Response b = a.AddBoard("arioshry@gmail.com", "boardy");
            Response<Task> t = a.AddTask("arioshry@gmail.com", "boardy", "nothing", "nothingness", DateTime.Today);


            if (c.ErrorOccured)
                Console.WriteLine(c.ErrorMessage);
            else
                Console.WriteLine(c.Value.Email);

            if (b.ErrorOccured)
                Console.WriteLine(b.ErrorMessage);

            if (t.ErrorOccured)
                Console.WriteLine(t.ErrorMessage);
            else
                Console.WriteLine(t.Value.Description);
            if (s.ErrorOccured)
                Console.WriteLine(s.ErrorMessage);
            
            Console.ReadLine();


        }
    }
}
