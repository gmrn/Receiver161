using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver161.Models
{
    [Table("SubContent")]
    public class SubContent
    {
        [Key]
        public int Id { get; set; }
        public int Id_content { get; set; }
        public string Text { get; set; }
    }
}
