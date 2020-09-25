using System;
using System.Collections.Generic;
using System.IO.Ports;
using Receiver161;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace Receiver161.PortServer
{
    public class Receiver
    {
        private SerialPort port { get; set; }
        private int temp_id { get; set; }
        private byte[] temp_buffer { get; set; }

        public Receiver() { }
        public Receiver(SerialPort serialPort) => this.port = serialPort;

        public void Start()
        {
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            //var inner = Task.Factory.StartNew(() =>
            //{
            //    //create new task for handler
            SerialPort port = (SerialPort)sender;

            byte[] head_buff = new byte[6];

            port.Read(head_buff, 0, head_buff.Length);

            if (head_buff[0] == 0x57 && head_buff[1] == 0xf1)
            {
                //длина сообщения
                int lenght = Convert.ToInt32(head_buff[2]);
                //индетификатор сообщения
                int id = Convert.ToInt32(head_buff[3]);
                //контрольная сумма head_buff[4-5]

                byte[] body_buff = new byte[lenght * 2];
                port.Read(body_buff, 0, body_buff.Length);

                Console.WriteLine(id);
                if (id == temp_id)
                {
                    temp_id = 0;
                    temp_buffer = body_buff;
                }
            }

            //});
        }

        internal byte[] GetByteDataTest154()
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

        internal byte[] GetByteDataTest104()
        {

            byte[] value_arr = { 0x01, 0x01 };

            return value_arr;
        }

        internal byte[] GetByteData(Models.Message item)
        {
            temp_id = item.Response_number;

            while (temp_buffer == null)
            {
                ((App)Application.Current).Server.transmitter.Send(item.Request_data);
                Thread.Sleep(1000);
            }

            byte[] value_arr = temp_buffer;
            temp_buffer = null;

            return value_arr;
        }
    }

}
