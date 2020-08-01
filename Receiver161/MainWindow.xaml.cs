using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Receiver161
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly string hPATH = $"{Environment.CurrentDirectory}\\headers.json";

        public MainWindow()
        {
            InitializeComponent();
            var msgs = new List<MessageBody>();

            using (var reader = File.OpenText(hPATH))
            {
                var fileText = reader.ReadToEnd();
                msgs = JsonConvert.DeserializeObject<List<MessageBody>>(fileText);
            }

            for (int i=0; i<5; i++)
            {
                stackPanel.Children.Add(new MessageGrid(msgs[i].header, msgs[i].explanation));
            }
        }
    }
}
