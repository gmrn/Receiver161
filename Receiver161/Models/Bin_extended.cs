using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver161
{
    [Table("Bins_extended")]
    public class Bin_extended
    {
        [Key]
        public int Id { get; set; }
        public int Id_binaries { get; set; }
        public string Text { get; set; }
        public string Data { get; set; }
    }
}
