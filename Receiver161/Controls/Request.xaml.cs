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
    /// Interaction logic for request.xaml
    /// </summary>
    public partial class Request : UserControl
    {
        public Request()
        {
            InitializeComponent();
        }

        private void Button_click(object sender, RoutedEventArgs e)
        {
            explanation.Visibility =
                (explanation.IsVisible) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Mouse_Enter(object sender, MouseEventArgs e)
        {
            ((Grid)sender).Background = (Brush)Application.Current.FindResource("SelectedRequestColorBrush");
        }

        private void Mouse_Leave(object sender, MouseEventArgs e)
        {
            ((Grid)sender).Background = (Brush)Application.Current.FindResource("RequestColorBrush");
        }
    }
}
