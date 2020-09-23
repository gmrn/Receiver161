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
        internal Models.Message message { get; private set; }

        public FrameContent()
        {
            InitializeComponent();
        }

        
        public void Сompose(Models.Message item)
        {
            message = item;
            
            //display a title of framecontent
            textBlock.Text = item.Title;

            //var getData = new Task<byte[]>(() =>
            //{
            //    return ((App)Application.Current).Server.receiver.GetByteData(item.Id_response);
            //});
            //getData.Start();
        
            // show load bar
            //getData.Wait();
            //remove load bar

            //var values = ((App)Application.Current).Server.receiver.GetByteDataTest();
            //var list = new PortServer.Decoder().GetListUIElements(item, values);

            var ui_elements = new List<Tuple<string, string, string>>();
            var values = new List<string>();

            var db = ((App)Application.Current).db;

            foreach (var tuple in db.GetContentsForId(item.Id))
            {
                ui_elements.Add(new Tuple<string, string, string>(tuple.Title, tuple.View, tuple.Meterage));
            }

            this.Show(ui_elements, values);
        }

        /// <summary>
        /// Compose and display each elements of a body's framecontent  
        /// </summary>
        /// <param name="list"></param>
        private void Show(List<Tuple<string, string, string, string>> list)
        {
            stackPanel.Children.Clear();
            //if (!(listValues.Count.Equals(listUIElements.Count)))
            //    MessageBox.Show("listValues not equal listUIElements");

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

            for (int i = 0; i < listUIElements.Count; i++)
            {
                var contentItem = new ContentItem()
                {
                    title = listUIElements[i].Item1,
                    view = listUIElements[i].Item2,
                    //value = listValues[i],
                    text = listUIElements[i].Item3
                };

                contentItem.Compose();
                stackPanel.Children.Add(contentItem);
            }

            //stackPanel.IsEnabled = false;
        }

        /// <summary>
        /// Get values from each contentItem at frameContent and return list of them
        /// </summary>
        /// <returns></returns>
        internal List<string> ReadValuesFromFrameContent()
        {
            var values = new List<string>();
            foreach (var item in stackPanel.Children)
            {
                values.Add((item as ContentItem).value);
            }
            return values;
        }
    }
}
