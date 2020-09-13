using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Receiver161
{
    /// <summary>
    /// Interaction logic for NavigationList.xaml
    /// </summary>
    public partial class NavigationList : UserControl
    {
        ApplicationContext db;

        public NavigationList()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Messages.Load();
            this.DataContext = db.Messages.Local.ToBindingList();
        }

        private void Mouse_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //((this.Parent as Grid).Parent as MainWindow).frameContent.Show(sender);

            var parent = (this.Parent as Grid).Parent as MainWindow;
            parent.frameContent.Сompose(sender);
        }

    }
}
