using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.Windows.Controls.Input;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Receiver161
{
    /// <summary>
    /// Interaction logic for ContentItem.xaml
    /// </summary>
    public partial class ContentItem : UserControl
    {
        internal string title { get; set; }
        internal string view { get; set; }
        internal string value { get; set; }
        internal string text { get; set; }


        public ContentItem()
        {
            InitializeComponent();
        }

        internal void Compose(params string[] param)
        {
            //indent from margin submenu for content BIN type 
            if (char.IsLower(title[0]))
                textBlock.Margin = new Thickness(20, 0, 0, 0);
            
            //title
            textBlock.Text = title;
            //view and value
            this.AddUItem();
        }

        private void AddUItem()
        {
            switch (view)
            {
                case ("radio"):
                    this.AddRadioButtons();
                    break;
                case ("check"):
                    this.AddCheckBox(value, text);
                    break;
                case ("BIN16"):
                    break;
                case ("BIN32"):
                    break;
                case ("ext"):
                    this.AddNumBlock(text);
                    break;
                case ("DATE"):
                    this.AddDatePicker(value);
                    break;
                case ("TIME"):
                    this.AddTimePicker(value);
                    break;
                default:
                    this.AddNumBlock(value);
                    break;
            }
        }

        private void AddNumBlock(string content)
        {

            var temp = new TextBox() { Text = content, TextWrapping = TextWrapping.Wrap};
            var border = new Border() { BorderThickness = new Thickness(5) };
            border.Child = temp;
            this.ui_field.Children.Add(border);

        }
        private void AddCheckBox(string value, string text)
        {
            var temp = new CheckBox() { IsChecked = Convert.ToBoolean(Int32.Parse(value)) , Content=text};
            this.ui_field.Children.Add(temp);
        }
        private void AddRadioButtons()
        {
            var stackPanel1 = new StackPanel();

            for (int i = 0; i < 3; i++)
                stackPanel1.Children.Add(new RadioButton() { Content = i.ToString() });

            this.ui_field.Children.Add(stackPanel1);
        }
        private void AddDatePicker(string _date)
        {
            var date = new DateTime();

            if (!(DateTime.TryParse(_date, out date)))
                date = DateTime.Parse("0001-1-1");

            var temp = new DatePicker() { SelectedDateFormat=DatePickerFormat.Short, SelectedDate=date };
            this.ui_field.Children.Add(temp);
        }
        private void AddTimePicker( string _time)
        {
            var time = new DateTime();

            if (!(DateTime.TryParse(_time, out time)))
                time = DateTime.Parse("00:00:00");

            var temp = new SfTimePicker { Value = time, FormatString = "HH:mm:ss" };
            this.ui_field.Children.Add(temp);
        }
    }
}
