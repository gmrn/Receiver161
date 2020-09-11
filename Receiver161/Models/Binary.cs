using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver161
{
    [Table("Binaries")]
    public class Binary
    {
        [Key]
        public int Id { get; set; }
        public int Id_contents { get; set; }
        public string Title { get; set; }
        public string View { get; set; }
        public string Rule { get; set; }
        public int Number_bit { get; set; }
    }
}
