using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Receiver161
{
    /// <summary>
    /// Interaction logic for FrameContent.xaml
    /// </summary>
    public partial class FrameContent : UserControl
    {
        ApplicationContext db;

        public FrameContent()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Messages.Load();
            db.Contents.Load();
            this.DataContext = db.Messages.Local.ToBindingList();
        }

        public void ShowReadContent(object sender) { }
        public void ShowEditContent(object sender) { }
        public void ShowFullContent(object sender) { }

        public void Show(object sender)
        {
            var item = (sender as ListViewItem).DataContext as Message;

            //display a title of framecontent
            textBlock.Text = item.Title;

            //get a data for a body's framecontent
            //LINQ request to db
            var _contents = from m in db.Messages
                            join c in db.Contents on m.Id equals c.Id_messages
                            join b in db.Binaries on c.Id equals b.Id_contents
                            where m.Id.Equals(item.Id)
                            select new { Id = c.Id, Title1 = c.Title, Type = c.Type, Title2 = b.Title, View = b.View };

            var array = from c in db.Contents
                        where c.Id_messages.Equals(item.Id)
                        select c.Id;
            var intArr = array.ToArray();

            //get data in list format
            var list = new List<Tuple<string, string>>();

            foreach (var i in intArr)
            {
                var _temp_contents = from _c in _contents
                                     where _c.Id.Equals(i)
                                     select _c;

                list.Add(new Tuple<string, string>
                    (_temp_contents.First().Title1, _temp_contents.First().Type));

                foreach (var tuple in _temp_contents)
                {
                    if (tuple.Title2 != null)
                        list.Add(new Tuple<string, string>(tuple.Title2, tuple.View));
                }
            }

            //fill a body's framecontent
            stackPanel.Children.Clear();
            foreach (var i in list)
            {
                var contentItem = new ContentItem();
                contentItem.textBlock.Text = i.Item1;

                switch (i.Item2)
                {
                    case ("radio"):
                        AddRadioButtons(contentItem);
                        break;
                    case ("switch"):
                        AddToggleswitch(contentItem);
                        break;
                    default:
                        break;
                }

                stackPanel.Children.Add(contentItem);
            }
        }

        private void AddToggleswitch(ContentItem contentItem)
        {
            var t_switch = new ToggleButton() { Width = 15, Height = 15 };
            Grid.SetColumn(t_switch, 1);
            contentItem.grid.Children.Add(t_switch);
        }

        private void AddRadioButtons(ContentItem contentItem)
        {
            var stackPanel1 = new StackPanel();

            for (int i = 0; i < 3; i++)
                stackPanel1.Children.Add(new RadioButton() { Content = i.ToString() });

            Grid.SetColumn(stackPanel1, 1);
            contentItem.grid.Children.Add(stackPanel1);
        }
    }
}
