using System;
using System.Diagnostics;
using System.IO.Ports;

namespace ProftaakGame
{
    internal class SerialConnection
    {
        private const int BaudRate = 1337; //TODO: get a real baudRate
        private readonly SerialPort serialPort;

        public SerialConnection(string portName)
        {
            serialPort = new SerialPort(portName, BaudRate);

            serialPort.Open();
        }

        public void WriteMessage(MessageType message)
        {
            string msg = String.Format("<{0}>", message);
            Debug.WriteLine("Sending " + msg);
            serialPort.WriteLine(msg);
        }
    }

    internal enum MessageType
    {
        Initialize,
        Start,
        GameOver,
        Pause
    }
}