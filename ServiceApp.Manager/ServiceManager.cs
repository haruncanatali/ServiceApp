using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ServiceApp.Manager
{
    public class ServiceManager
    {
        private readonly Timer timer;

        public ServiceManager()
        {
            timer = new Timer(60000) { AutoReset = true };
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Model model = new Model
            {
                Date = DateTime.Now,
                PcName = GetUsername(),
            };

            string[] modelArr = new string[]
            {
                model.Date.ToLongTimeString() + " " + model.PcName+"/"
            };

            File.AppendAllLines(GetProgramDataPath(),modelArr);
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public static string GetProgramDataPath()
        {
            try
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                if ( String.IsNullOrEmpty(appDataPath) )
                    throw new Exception("Unexpected Error (The ProgramData directory cannot be null.)");

                if (!Directory.Exists(appDataPath + "\\VeriketApp"))
                    Directory.CreateDirectory(appDataPath + "\\VeriketApp");

                return appDataPath + "\\VeriketApp\\VeriketAppTest.txt";
            }
            catch ( Exception e )
            {
                throw new Exception("Unexpected Error : " + e.Message);
            }
        }

        public static string GetUsername()
        {
            var computerName = Environment.MachineName;
            if ( String.IsNullOrEmpty(computerName) )
                throw new Exception("Unexpected Error");
            return computerName;
        }
    }
}
