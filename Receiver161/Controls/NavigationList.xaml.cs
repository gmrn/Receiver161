using System.Data.Entity;
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

            db = ((App)Application.Current).db;
            db.Messages.Load();
            this.DataContext = db.Messages.Local.ToBindingList();
        }

        /// <summary>
        /// Select ListViewItem to call method thst display framecontent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mouse_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListViewItem).DataContext as Models.Message;

            var parent = (this.Parent as Grid).Parent as MainWindow;
            parent.frameContent.Сompose(item);
        }

    }
}
