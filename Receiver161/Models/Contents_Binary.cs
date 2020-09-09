using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver161
{
    [Table("Contents_Binaries")]
    public class Contents_Binary
    {
        public int Id_con { get; set; }
        [Key]
        public int Id_bin { get; set; }

    }
}