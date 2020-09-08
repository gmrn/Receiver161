using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver161
{
    [Table("Binaries")]
    public class Binary
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Rule { get; set; }
        public int Number_bit { get; set; }
    }
}
