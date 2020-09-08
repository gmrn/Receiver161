using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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


        private void Mouse_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var messages_contents = db.Messages_Contents.AsQueryable();

            var item = (sender as ListViewItem).DataContext as Message;

            var array = from m in messages_contents
                        where m.Id_mess.Equals(item.Id)
                        select m.Id_con;
            var intArr = array.ToArray();

            frameContent.textBlock.Text = item.Title;
        }
    }
}
