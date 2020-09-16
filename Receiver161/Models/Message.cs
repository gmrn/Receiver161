using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver161.Models
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Request_number { get; set; }
        public int Response_number { get; set; }
        public string Request_data { get; set; }
        public string Response_data { get; set; }
    }
}