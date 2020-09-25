using System.Collections.Generic;
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

        public FrameContent() => InitializeComponent();

        public void Сompose(Models.Message message)
        {
            this.message = message;

            frameTitle.Text = message.Title;

            //var getData = new Task<byte[]>(() =>
            //{
            //    return ((App)Application.Current).Server.receiver.GetByteData(message.Id_response);
            //});
            //getData.Start();

            // show load bar
            //getData.Wait();
            //remove load bar

            var data = ((App)Application.Current).Server.receiver.GetByteDataTest104();
            var values = new PortServer.Decoder().GetListValues(message.Id, data);

            this.Show(values);
            //this.Show(new List<string>());
        }

        /// <summary>
        /// Display each elements of a body's framecontent
        /// </summary>
        /// <param name="values"></param>
        private void Show(List<string> values)
        {
            //HOW DO VALUES TRANSFER 
            stackPanel.Children.Clear();

            var value = values.GetEnumerator();

            foreach (var content
                in ((App)Application.Current).db.GetContentsForId(message.Id))
            {
                if (value.MoveNext())
                {
                    var contentitem = new ContentItem() { value = value.Current };
                    contentitem.Compose(content);
                    stackPanel.Children.Add(contentitem);
                }
                    //stackPanel.Children.Add(new ContentItem(content) { value = value.Current });
                //stackPanel.Children.Add(new ContentItem(content));
            }

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
