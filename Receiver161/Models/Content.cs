using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver161.Models
{
    [Table("Content")]
    public class Content
    {
        [Key]
        public int Id { get; set; }
        public int Id_message { get; set; }
        public string Title { get; set; }
        public string View { get; set; }
        public string Meterage { get; set; }
    }
}
