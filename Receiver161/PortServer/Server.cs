using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Receiver161.PortServer
{
    class Server
    {
        SerialPort serialPort { get; set; }
        Transmitter transmitter { get; set; }


        public void Run()
        {
            serialPort = ChoosePorts();
            setConfig();
            var reciever = new Reciever(serialPort);
            transmitter = new Transmitter(serialPort);

            try
            {
                serialPort.Open();

                while (true)
                {
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(reciever.DataReceivedHandler);
                }

                //var threadReciever = new Thread(new ThreadStart(new Reciever(serialPort).Start));
                //var threadTransmitter = new Thread(new ThreadStart(new Transmitter(serialPort).Start));
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: невозможно открыть порт:");
                return;
            }



        }



        private SerialPort ChoosePorts()
        {
            string[] ports = SerialPort.GetPortNames();

            //Console.WriteLine("Выберите порт:");

            //for (int i = 0; i < ports.Length; i++)
            //{
            //    Console.WriteLine("[" + i.ToString() + "] " + ports[i].ToString());
            //}

            //string n = Console.ReadLine();
            //int num = int.Parse(n);
            var port = new SerialPort();

            if (ports.Length == 1)
                port.PortName = ports[0];

            //port.PortName = ports[num];
            return port;
        }

        private void setConfig()
        {
            //serialPort.PortName = ports[num];
            //serialPort.ReadTimeout = 1000;
            //serialPort.WriteTimeout = 1000;
            serialPort.BaudRate = 38400;
            serialPort.DataBits = 8;
            serialPort.Parity = System.IO.Ports.Parity.Odd;
            serialPort.StopBits = System.IO.Ports.StopBits.One;
        }
    }
}
