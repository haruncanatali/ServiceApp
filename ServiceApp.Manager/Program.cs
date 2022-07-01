using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ServiceApp.Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(c =>
            {
                c.Service<ServiceManager>(y =>
                {
                    y.ConstructUsing(sm => new ServiceManager());
                    y.WhenStarted(sm => sm.Start());
                    y.WhenStopped(sm => sm.Stop());
                });

                c.RunAsLocalSystem();

                c.SetServiceName("Veriket Application Test");
                c.SetDescription("Veriket Application Test Service ");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
