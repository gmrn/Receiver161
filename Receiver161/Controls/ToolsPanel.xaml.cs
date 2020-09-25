using System;
using System.Collections.Generic;
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
    /// Interaction logic for ToolsPanel.xaml
    /// </summary>
    public partial class ToolsPanel : UserControl
    {
        private FrameContent frm { get
            { return (this.Parent as Grid).Parent as FrameContent; } }

        public ToolsPanel()
        {
            InitializeComponent();
        }

        private void SendRequest(object sender, RoutedEventArgs e)
        {
            var values = frm.ReadValuesFromFrameContent();
            var bytes = new PortServer.Decoder().WrapValuesToBytes(values, frm.message.Id);

            ((App)Application.Current).Server.transmitter.Send(bytes);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frm.stackPanel.IsEnabled = !(frm.stackPanel.IsEnabled);
        }
    }
}
