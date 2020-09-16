using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Receiver161
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ApplicationContext db;
        public PortServer.Server Server { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            db = new ApplicationContext();

            Server = new PortServer.Server();
            
            //launch un new thread the server
            Task.Factory.StartNew(() => Server.Run());

        }
    }


}
