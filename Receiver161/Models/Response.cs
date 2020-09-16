using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver161.Models
{
    [Table("Response")]
    public class Response
    {
        [Key]
        public int Id { get; set; }
        public int Id_message { get; set; }
        public string Type { get; set; }
        public int Offset { get; set; }
    }
}
