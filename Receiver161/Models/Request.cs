using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver161
{
    [Table("Requests")]
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public int Id_message { get; set; }
        public string Data { get; set; }
    }
}
