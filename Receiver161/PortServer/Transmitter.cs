using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver161.PortServer
{
    class Transmitter
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
            //преобразовать стринг в массив байтов
            port.Write(data);
        }

        private byte[] GetWrapData(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
