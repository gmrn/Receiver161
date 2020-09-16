using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Receiver161
{
    /// <summary>
    /// Interaction logic for FrameContent.xaml
    /// </summary>
    public partial class FrameContent : UserControl
    {
        ApplicationContext db;
        bool? IsSaved;
        Message message;

        public FrameContent()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Messages.Load();
            db.Contents.Load();
            this.DataContext = db.Messages.Local.ToBindingList();
        }

        //public void ShowReadContent(object sender) { }
        //public void ShowEditContent(object sender) { }
        //public void ShowFullContent(object sender) { }

        public void Сompose(Message item)
        {
            message = item;
            //display a title of framecontent
            textBlock.Text = item.Title;

            //radialBar.Visibility = Visibility.Visible;

            //var thread = new Thread(
            //    () =>
            //    {
            //        data = ((App)Application.Current).Server.reciever.GetByteData(item.Id_response); // Publish the return value
            //    });
            //thread.Start();
            //thread.Join();

            //new Thread(new ThreadStart(((App)Application.Current).Server.reciever.GetByteData))
            //var getData = new Task<byte[]>(() =>
            //{
            //    return ((App)Application.Current).Server.reciever.GetByteData(item.Id_response);
            //});
            //getData.Start();
        
            // show load bar
            //getData.Wait();
            //remove load bar

            var data = ((App)Application.Current).Server.reciever.GetByteDataTest();

            var list = new PortServer.Decoder().GetListUIElements(item, data);
            this.Show(list);

            //var decoder = new PortServer.Decoder();
            //var listValues = decoder.GetListValues(item, data);
            //var listUIElements = decoder.GetListUIElements(item);
            //this.Show(listUIElements, listValues);
        }

        /// <summary>
        /// Compose and display each elements of a body's framecontent  
        /// </summary>
        /// <param name="list"></param>
        private void Show(List<Tuple<string, string, string, string>> list)
        {
            stackPanel.Children.Clear();

            foreach (var i in list)
            {
                var contentItem = new ContentItem()
                {
                    title = i.Item1,
                    view = i.Item2,
                    value = i.Item3,
                    text = i.Item4
                };

                contentItem.Compose();

                stackPanel.Children.Add(contentItem);
                stackPanel.IsEnabled = false;
            }
        }

        private void Show(List<Tuple<string, string, string>> listUIElements, List<string> listValues)
        {
            stackPanel.Children.Clear();

            if (!(listValues.Count.Equals(listUIElements.Count)))
                MessageBox.Show("listValues not equal listUIElements");

            for (int i = 0; i < listValues.Count; i++)
            {
                var contentItem = new ContentItem()
                {
                    title = listUIElements[i].Item1,
                    view = listUIElements[i].Item2,
                    value = listValues[i],
                    text = listUIElements[i].Item3
                };

                contentItem.Compose();
                stackPanel.Children.Add(contentItem);
            }

            stackPanel.IsEnabled = false;
        }

        private void Farm()
        {
            var listValues = new List<string>();
            foreach (var item in stackPanel.Children)
            {
                var value = (item as ContentItem).ui_field.Children[0] as TextBox;
                listValues.Add(value.Text);
                (item as ContentItem).value = null;
            }
        }
    }
}
