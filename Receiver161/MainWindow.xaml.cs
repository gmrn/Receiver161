using System.Data.Entity;
using System.Windows;

namespace Receiver161
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FrameContent _frameContent;
        NavigationList _navigationList;
        ToolsPanel _toolsPanel;

        public MainWindow()
        {
            InitializeComponent();

            _frameContent = frameContent;
            _navigationList = navigationList;
        }
    }
}
