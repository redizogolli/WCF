using System;
using Topshelf;

namespace RestTopShelfService
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serviceUri = "http://localhost:80/EmployeeService";
            var host = HostFactory.New(c =>
            {
                c.Service<WcfServiceWrapper<Employees, IEmployees>>(s =>
                {
                    s.ConstructUsing(x =>
                        new WcfServiceWrapper<Employees, IEmployees>("RestWcfService", serviceUri));
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
                c.RunAsLocalSystem();

                c.SetDescription("Runs RestWcfService.");
                c.SetDisplayName("RestWcfService");
                c.SetServiceName("RestWcfService");
            });

            Console.WriteLine("Hosting ...");
            host.Run();
            Console.WriteLine("Done hosting ...");

            Console.ReadKey();
        }
    }
}
