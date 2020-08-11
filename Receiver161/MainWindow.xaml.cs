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


        public MainWindow()
        {
            InitializeComponent();
            //Cover.Width = Grid.Width * 0.9 - PanelMessage.Width;
        }

        private void PanelSearch_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PanelSearch.IsEnabled = true;
            PanelSearch.TabIndex = 0;
            PanelMessage.TabIndex = 1;
            PanelMessage.IsEnabled = false;
        }



        private void Cover_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((Cover)sender).HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetZIndex(PanelSearch, 1);
        }
    }
}
