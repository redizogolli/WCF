using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SoapTopShelfService
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serviceUri = "http://localhost:80/SoapEmployeeService";
            var host = HostFactory.New(c =>
            {
                c.Service<WcfServiceWrapper<Employees, IEmployees>>(s =>
                {
                    s.ConstructUsing(x =>
                        new WcfServiceWrapper<Employees, IEmployees>("SoapTopShelfService", serviceUri));
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
                c.RunAsLocalSystem();

                c.SetDescription("Runs SoapTopShelfService.");
                c.SetDisplayName("SoapTopShelfService");
                c.SetServiceName("SoapTopShelfService");
            });

            Console.WriteLine("Hosting ...");
            host.Run();
            Console.WriteLine("Done hosting ...");

            Console.ReadKey();
        }
    }
}
