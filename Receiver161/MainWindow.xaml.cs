using System.Data.Entity;
using System.Windows;

namespace Receiver161
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
            db.Messages.Load();
            this.DataContext = db.Messages.Local.ToBindingList();
        }
    }
}
