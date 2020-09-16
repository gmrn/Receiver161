using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver161.Models
{
    [Table("Binary")]
    public class Binary
    {
        [Key]
        public int Id { get; set; }
        public int Id_request { get; set; }
        public int Id_response { get; set; }
        public string Rule { get; set; }
        public int Length { get; set; }
    }
}
