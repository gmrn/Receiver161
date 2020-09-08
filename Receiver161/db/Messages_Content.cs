using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver161
{
    [Table("Messages_Contents")]
    public class Messages_Content
    {
        public int Id_mess { get; set; }
        [Key]
        public int Id_con { get; set; }

    }
}