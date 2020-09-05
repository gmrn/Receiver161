using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver161
{
    class Transmitter
    {
        private SerialPort port { get; set; }

        public Transmitter(SerialPort serialPort)
        {
            this.port = serialPort;
        }

        public void Start()
        {
        }
    }
}
