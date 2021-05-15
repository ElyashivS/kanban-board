using System;
using IntroSE.Kanban.Backend.ServiceLayer;
namespace Tests
{
    public class Tests
    {
          public static void Main(string[] args)
        {
            Service a = new Service();
            a.Register("arioshryz@gmail.com", "12345667");
        }
    }
}
