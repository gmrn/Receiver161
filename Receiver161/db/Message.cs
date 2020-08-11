namespace Receiver161
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Data.Entity;
    using System.Linq;

    public class Message
    {
        private int id;
        private string title;
        private string text;
        private string notes;
        private int isRequest;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        public int IsRequest { get; set; }

        //public string Title
        //{
        //    get { return title; }
        //    set
        //    {
        //        title = value;
        //        OnPropertyChanged("Title");
        //    }
        //}
        //public string Company
        //{
        //    get { return company; }
        //    set
        //    {
        //        company = value;
        //        OnPropertyChanged("Company");
        //    }
        //}
        //public int Price
        //{
        //    get { return price; }
        //    set
        //    {
        //        price = value;
        //        OnPropertyChanged("Price");
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged([CallerMemberName]string prop = "")
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(prop));
        //}
    }
}