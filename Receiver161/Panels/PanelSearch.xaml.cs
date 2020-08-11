using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Receiver161
{
    /// <summary>
    /// Interaction logic for PanelSearch.xaml
    /// </summary>
    public partial class PanelSearch : UserControl
    {
        //private readonly string hPATH = $"{Environment.CurrentDirectory}\\headers.json";
        ApplicationContext db;

        public PanelSearch()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Messages.Load();
            this.DataContext = db.Messages.Local.ToBindingList();

            //var msgs = new List<MessageBody>();

            //using (var reader = File.OpenText(hPATH))
            //{
            //    var fileText = reader.ReadToEnd();
            //    msgs = JsonConvert.DeserializeObject<List<MessageBody>>(fileText);
            //}

            //for (int i = 0; i < msgs.Count; i++)
            //{
            //    if (msgs[i].header != null)
            //    {
            //        var request = new Request();
            //        request.header.Text = msgs[i].header;
            //        request.explanation.Text = msgs[i].explanation;
            //        stackPanel.Children.Add(request);
            //    }
            //}
        }
    }
}
