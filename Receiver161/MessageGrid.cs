using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Receiver161
{
    public class MessageGrid : Grid
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
            //margins
            this.Margin= new Thickness(10);
            //bodrers
            var border = new Border();
            this.Background = Brushes.LightGray;

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

            //header
            TextBox txt = new TextBox();
            txt.IsReadOnly = true;
            txt.Background = Brushes.Transparent;
            txt.BorderThickness = new Thickness(0);
            txt.Text = header;

            Grid.SetRow(txt, 0);
            Grid.SetColumn(txt, 0);

            //button info
            Button btn = new Button();
            btn.Background = Brushes.Transparent;
            btn.BorderThickness = new Thickness(0);
            btn.Content = "i";

            Grid.SetRow(btn, 0);
            Grid.SetColumn(btn, 1);
            btn.Click += Button_click;

            //explanation
            TextBox txt1 = new TextBox();
            txt1.IsReadOnly = true;
            txt1.Background = Brushes.Transparent;
            txt1.BorderThickness = new Thickness(0);
            txt1.Text = explanation;

            Grid.SetRow(txt1, 1);
            Grid.SetColumnSpan(txt1, 2);
            txt1.Visibility = Visibility.Collapsed;

            this.Children.Add(txt);
            this.Children.Add(btn);
            this.Children.Add(txt1);

            this.MouseEnter += Button_enter;
            this.MouseLeave += Button_leave;
        }

        private void Button_leave(object sender, MouseEventArgs e)
        {
            ((MessageGrid)sender).Background = Brushes.LightGray;
        }

        private void Button_enter(object sender, MouseEventArgs e)
        {
            ((MessageGrid)sender).Background = Brushes.DarkOrange;
        }

        private void Button_click(object sender, RoutedEventArgs e)
        {
            var explanation = ((MessageGrid)((Button)sender).Parent).Children[2];

            explanation.Visibility =
                (explanation.IsVisible) ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
