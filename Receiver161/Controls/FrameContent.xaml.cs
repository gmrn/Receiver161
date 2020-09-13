using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public void Сompose(object sender)
        {
            var item = (sender as ListViewItem).DataContext as Message;
            
            //display a title of framecontent
            textBlock.Text = item.Title;

            //wait response from receiver
            //byte[] arr = receiver.get()
            //while show circle load sign
            byte[] data = new PortServer.Reciever().GetByteData(item.Id);

            var list = new PortServer.Decoder().GetValueList(item, data);
            this.Show(list);
        }

        /// <summary>
        /// fill a body's framecontent  
        /// </summary>
        /// <param name="list"></param>
        private void Show(List<Tuple<string, string, string, string>> list)
        {
            stackPanel.Children.Clear();

            foreach (var i in list)
            {
                var contentItem = new ContentItem(){
                    title = i.Item1,
                    view = i.Item2,
                    value = i.Item3,
                    text = i.Item4 };

                contentItem.Compose();

                stackPanel.Children.Add(contentItem);
                //stackPanel.IsEnabled = false;
            }
        }
    }
}
