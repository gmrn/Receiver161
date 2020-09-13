using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver161.PortServer
{
    class Reciever
    {
        private SerialPort port { get; set; }

        public Reciever(){}
        public Reciever(SerialPort serialPort)
        {
            this.port = serialPort;
        }

        public void Start()
        {
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;

            byte[] head_buff = new byte[6];

            port.Read(head_buff, 0, head_buff.Length);

            if (head_buff[0] == 0x57 && head_buff[1] == 0xf1)
            {
                //Console.WriteLine("new message!");
                
                //длина сообщения
                int lenght = Convert.ToInt32(head_buff[2]);
                //индетификатор сообщения
                int id = Convert.ToInt32(head_buff[3]);
                //контрольная сумма head_buff[4-5]

                byte[] body_buff = new byte[lenght * 2];
                port.Read(body_buff, 0, body_buff.Length);

                //new Task(() => decoder(body_buff));
            }
        }

        public byte[] GetByteData(int id)
        {
             byte[] value_arr = {
                0x7f, 0x22,
                0x08, 0x00, 0x04, 0x00,
                0xF1, 0xC9, 0x9A, 0x3B,
                0x14, 0x08, 0x05, 0x00,
                0x00, 0x00, 0x00, 0x88,
                0x6E, 0x89, 0xC4, 0xBE,
                0x00, 0x00, 0xDE, 0x42,
                0x00, 0x00, 0xA2, 0x2D };

            return value_arr;
        }
    }

}
