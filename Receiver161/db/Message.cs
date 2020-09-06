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
        private int isRequest;

        public int Id { get; set; }
        public string Title { get; set; }
        public int IsRequest { get; set; }
    }
}