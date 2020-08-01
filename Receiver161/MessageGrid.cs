using System.Windows;
using System.Windows.Controls;

namespace Receiver161
{
    class MessageGrid : Grid
    {
        private string header { get; set; }
        private string explanation { get; set; } = "hasn't description";

        public MessageGrid(params string[] str)
        {
            this.header = str[0];

            if (str[1] != null)
                this.explanation = str[1];

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            var myThickness = new Thickness(10);
            this.Margin= myThickness;
            
            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            this.RowDefinitions.Add(rowDef1);
            this.RowDefinitions.Add(rowDef2);

            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            this.ColumnDefinitions.Add(colDef1);
            this.ColumnDefinitions.Add(colDef2);
            colDef2.Width = new GridLength(0.1, GridUnitType.Star);

            TextBox txt = new TextBox();
            txt.IsReadOnly = true;
            txt.Text = header;
            Grid.SetRow(txt, 0);
            Grid.SetColumn(txt, 0);

            Button btn = new Button();
            btn.Content = "i";
            Grid.SetRow(btn, 0);
            Grid.SetColumn(btn, 1);

            this.Children.Add(txt);
            this.Children.Add(btn);

            btn.MouseEnter += Button_enter;
            btn.MouseLeave += Button_leave;
        }

        private void Button_enter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBox text = new TextBox();
            text.Text = explanation;
            Grid.SetRow(text, 1);
            Grid.SetColumnSpan(text, 2);
            ((MessageGrid)((Button)sender).Parent).Children.Add(text);
        }

        private void Button_leave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((MessageGrid)((Button)sender).Parent).Children.RemoveAt(2);
        }
    }
}
