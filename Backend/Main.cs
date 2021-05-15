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
            a.Register("ari@gmail.com", "Aa2eferf");
        }
    }
}
