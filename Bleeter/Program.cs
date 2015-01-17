using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var app = WebApp.Start<Startup>("http://localhost:9999"))
            {
                Console.WriteLine("Web server running");
                Console.ReadLine();
            }
        }
    }
}
