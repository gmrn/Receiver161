using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver161.PortServer
{
    public class Transmitter
    {
        private SerialPort port { get; set; }

        public Transmitter(SerialPort serialPort)
        {
            this.port = serialPort;
        }

        public void Send(byte[] data)
        {
            port.Write(data, 0, data.Length);
        }

        public void Send(string data)
        {
            var str_buffer = data.Split(' ');
            var buffer = new byte[str_buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Byte.Parse(str_buffer[i]);
            }

            port.Write(buffer, 0, buffer.Length);
        }

        private byte[] GetWrapData(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
