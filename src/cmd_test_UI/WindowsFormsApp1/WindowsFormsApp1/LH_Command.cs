using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class LH_Command
    {
        public bool isConnect = false;
        private Byte[] byteData = new Byte[128];
        private Int32 byteDataLength = 0;
        private Byte[] message = new Byte[128];
        private Int32 messageLength = 0;
        private Thread KeepRecievingThread;
        public bool hasNewRequest = false;
        public bool hasNewRecieve = false;
        private SerialPort My_SerialPort = new SerialPort();

        public void SerialClose()
        {
            if (isConnect)
            {
                KeepRecievingThread.Abort();
                My_SerialPort.Close();
                isConnect = false;
            }
        }

        public void SerialConnect(string PORT, int PORT_BR=115200, int PORT_DB=8, Parity PORT_P=Parity.None, StopBits PORT_SB=StopBits.One)
        {
            Thread thread = new Thread(delegate () {
                try
                {
                    if (My_SerialPort.IsOpen)
                    {
                        My_SerialPort.Close();
                        isConnect = false;
                    }

                    //設定 Serial Port 參數
                    My_SerialPort.PortName = PORT;
                    My_SerialPort.BaudRate = PORT_BR;
                    My_SerialPort.DataBits = PORT_DB;
                    My_SerialPort.Parity = PORT_P;
                    My_SerialPort.StopBits = PORT_SB;

                    if (!My_SerialPort.IsOpen)
                    {
                        //開啟 Serial Port
                        My_SerialPort.Open();
                        isConnect = true;
                    }

                    KeepRecievingThread = new Thread(delegate () {
                        KeepRecieving();
                    });
                    KeepRecievingThread.Start();
                }
                catch 
                //(Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            });
            thread.Start();
        }

        private void KeepRecieving()
        {
            while (true)
            {
                RecieveData();
            }
        }
        private void RecieveData()
        {
            byteDataLength = My_SerialPort.Read(byteData, 0, byteData.Length);
            hasNewRecieve = true;
        }
        public byte checkCRC(byte[] bytes, int length)
        {
            byte cRc = 0x00;
            for (int i = 0; i < (length - 1); i++)
            {
                cRc -= bytes[i];
            }
            return cRc;
        }

        public string GetDataString()
        {
            string dataString = "";
            try
            {
                dataString = Encoding.ASCII.GetString(byteData, 0, byteDataLength);
            }
            catch { }

            return dataString;
        }

        public string GetDataHex()
        {
            return ToHexString(byteData, byteDataLength);
        }

        public string GetMessageString()
        {
            string messageString = "";
            try
            {
                messageString = Encoding.ASCII.GetString(message, 0, messageLength);
            }
            catch { }

            return messageString;
        }

        public string GetMessageHex()
        {
            return ToHexString(message, messageLength);
        }

        public void SendData(byte[] byteData, int length)
        {
            My_SerialPort.Write(byteData, 0, length);
            hasNewRequest = true;
        }

        public void SendDataString(string stringData)
        {
            Byte[] byteData = Encoding.ASCII.GetBytes(stringData);
            SendData(byteData, byteData.Length);
        }

        public void SendDataHex(string hexData)
        {
            Byte[] byteData = GetBytes(hexData);
            SendData(byteData, byteData.Length);
        }

        protected string ToHexString(byte[] bytes, int length)
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder str = new StringBuilder();

                for (int i = 0; i < length; i++)
                {
                    str.Append(bytes[i].ToString("X2"));
                }
                hexString = str.ToString();
            }
            return hexString;
        }

        protected byte[] GetBytes(string HexString)
        {
            int byteLength = HexString.Length / 2;
            byte[] bytes = new byte[byteLength];
            string hex;
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new String(new Char[] { HexString[j], HexString[j + 1] });
                bytes[i] = HexToByte(hex);
                j += 2;
            }
            return bytes;
        }

        private byte HexToByte(string hex)
        {
            if (hex.Length > 2 || hex.Length <= 0)
                throw new ArgumentException("hex must be 1 or 2 characters in length");
            byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return newByte;
        }

    }
}
