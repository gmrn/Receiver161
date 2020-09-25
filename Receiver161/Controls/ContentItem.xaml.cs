using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.Windows.Controls.Input;

namespace Receiver161
{
    /// <summary>
    /// Interaction logic for ContentItem.xaml
    /// </summary>
    public partial class ContentItem : UserControl
    {
        internal string value { get; set; }

        public ContentItem(Models.Content content)
        {
            InitializeComponent();
            Compose(content);
        }

        public ContentItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Compose a body's contentItem and
        /// create from a table 'SubContent' list of contents for comboBox if it exists
        /// </summary>
        /// <param name="content"></param>
        internal void Compose(Models.Content content)
        {
            textBlock.Text = content.Title;

            var text = new List<string>();
            foreach (var subContent in ((App)Application.Current).db.GetSubContentsById(content.Id))
            {
                text.Add(subContent.Text);
            }

            this.AddUIElement(content.View, content.Meterage, text);
        }

        private void AddUIElement(string view, string meterage, List<string> text)
        {
            switch (view)
            {
                case ("numblock"):
                    this.AddNumBlock();
                    break;
                case ("check"):
                    this.AddCheckBox(meterage);
                    break;
                case ("date"):
                    this.AddDatePicker();
                    break;
                case ("time"):
                    this.AddTimePicker();
                    break;
                case ("list"):
                    this.AddComboBox(text);
                    break;
                default:
                    break;
            }
        }

        private void AddComboBox(List<string> text)
        {
            var combobox = new ComboBox() ;

            foreach (var item in text)
                combobox.Items.Add(new TextBlock() { Text = item });

            combobox.SelectedIndex = Int32.Parse(value);
            this.ui_field.Children.Add(combobox);
        }

        private void AddNumBlock()
        {

            var temp = new TextBox() { Text = value, TextWrapping = TextWrapping.Wrap};
            temp.TextChanged += NumBox_ValueChanged; 
            var border = new Border() { BorderThickness = new Thickness(5) };
            border.Child = temp;
            this.ui_field.Children.Add(border);

        }

        private void AddCheckBox(string meterage)
        {
            var temp = new CheckBox() { IsChecked = Convert.ToBoolean(Int32.Parse(value)) , Content=meterage};
            this.ui_field.Children.Add(temp);
        }

        private void AddDatePicker()
        {
            var date = new DateTime();

            if (!(DateTime.TryParse(value, out date)))
                date = DateTime.Parse("0001-1-1");

            var temp = new DatePicker() { SelectedDateFormat=DatePickerFormat.Short, SelectedDate=date };
            this.ui_field.Children.Add(temp);
        }

        private void AddTimePicker()
        {
            var time = new DateTime();

            if (!(DateTime.TryParse(value, out time)))
                time = DateTime.Parse("00:00:00");

            var temp = new SfTimePicker { Value = time, FormatString = "HH:mm:ss" };
            this.ui_field.Children.Add(temp);
        }

        private void NumBox_ValueChanged(object sender, TextChangedEventArgs e)
        {
            value = (sender as TextBox).Text;
        }
    }
}
 