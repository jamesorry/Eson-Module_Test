using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.Collections;

namespace Eson_Pega
{
    class RcvSend_Data
    {
        #region Private Parameter
        private byte[] sendData = new byte[128];
        
        private const int TAGID = 0;
        private const int LEN = 1;
        private const int CMD_ID = 2;
        private const int HMI_ID = 3;
        private const int DATA = 4;
        private const int FREQ = 4;
        private const int STEP = 6;
        private const int AUTO_START_FREQ = 4;           // 2 bytes
        private const int AUTO_GOHOME_FREQ = 6;    // 2 bytes
        private const int AUTO_STEPS = 8;                       // 4 bytes
        private const int AUTO_TIME = 12;                      // 4 bytes
        private const int STEP_HOME_ACT = 4;              // 1 byte
        private const int STEP_HOME_FREQ = 5;           // 2 bytes
        private const int STEP_HOME_STEPS = 7;         // 4 bytes

        private const int STATE_DATA = 4;

        //Data Length
        private const int PING_LEN = 0x05;
        private const int SETID_LEN = 0x06;
        private const int READ_MOTOR_PARA_LEN = 0x06;
        private const int WRITE_MOTOR_PARA_LEN = 0x0A;
        private const int SAVE_DATA_LEN = 0x05;
        private const int STATE_LEN = 0x05;
        private const int RESTART_LEN = 0x05;
        private const int IO_STATUS_LEN = 0x06;
        private const int SETDO_LEN = 0x06;
        private const int MOTOR_MOVE_LEN = 0x06;
        private const int MOTOR_EMERG_LEN = 0x07;
        private const int SET_VOLT_LEN = 0x06;
        private const int READ_VOLT_LEN = 0x05;



        #endregion

        #region Public Parameter
        public byte[] rcvbuff = new byte[256];
        public string debug_String = string.Empty;
        public byte present_state = 0x03;
        public bool ping = false;

        //CMD ID
        public const byte PING = 0x00;
        public const byte SETID = 0x01;
        public const byte READ_MOTOR_PARA = 0x02;
        public const byte WRITE_MOTOR_PAPA = 0x03;
        public const byte SAVE_DATA = 0x04;
        public const byte STATE = 0x05;
        public const byte RESTART = 0x06;
        public const byte IO_STATUS = 0x07;
        public const byte SETDO = 0x08;
        public const byte MOTOR_MOVE = 0x09;
        public const byte MOTOR_EMERG = 0x0A;
        public const byte SET_VOLT = 0x0B;
        public const byte READ_VOLT = 0x0C;


        #endregion

        #region Class

        #endregion

        public void GetCmd_string(string msg) 
        {
            debug_String += msg + "\r\n";
        }

        #region 接收指令        
        public void GetCmd()
        {
            if(rcvbuff[0] == 0xF9)                                                                                                                                 //  比對TAG ID
            {
                if(rcvbuff[1] > 0 && rcvbuff[1] < 11)                                                                                                  //   比對CMD LENGTH範圍
                {
                    int cmd_len = rcvbuff[1];
                    byte[] cmd_buff = new byte[cmd_len];
                    Array.Copy(rcvbuff, 0, cmd_buff, 0, cmd_len);                                                                             //   取出CMD


                    if (rcvbuff[cmd_len - 1] == checkCRC(cmd_buff, cmd_len))                                                    //   比對CRC Check
                    {
                        AnalyzeCmd(cmd_buff, cmd_buff.Length);                                                                              //   分析CMD
                        Array.Copy(rcvbuff, cmd_len, rcvbuff, 0, rcvbuff.Length - cmd_len);                                 //   從buffer中移除取出之cmd訊息

                        //debug_String += BitConverter.ToString(cmd_buff, 0, cmd_len);
                        //debug_String += BitConverter.ToString(rcvbuff, 0, rcvbuff.Length);
                    }
                    else
                    {
                        //若CRC Check錯誤
                        debug_String += "CRC Check 錯誤!!\r\n";
                        debug_String += "Receive Message: " + BitConverter.ToString(cmd_buff, 0, cmd_len) + "\r\n";
                        Array.Copy(rcvbuff, cmd_len, rcvbuff, 0, rcvbuff.Length - cmd_len);                                  // 向左移cmd length位
                    }
                }
                else
                {
                    //若CMD LENGTH錯誤
                    debug_String += "CMD 長度錯誤!!\r\n";
                    debug_String += "Receive Message: " + BitConverter.ToString(rcvbuff, 0, 2) + "\r\n";
                    Array.Copy(rcvbuff, 2, rcvbuff, 0, rcvbuff.Length - 2);                                                                 // 向左移2位
                }
            }
            else
            {
                //若TAG ID錯誤
                debug_String += "TAG ID 錯誤!!\r\n";
                debug_String += "Receive Message: " + BitConverter.ToString(rcvbuff, 0, 1) + "\r\n";
                Array.Copy(rcvbuff, 1, rcvbuff, 0, rcvbuff.Length - 1);                                                                     // 向左移1位
            }
        }

        public void AnalyzeCmd(byte[] rcvData, int rcvData_len)
        {
            if (rcvData[rcvData_len - 1] == checkCRC(rcvData, rcvData_len))
            {
                switch (rcvData[CMD_ID])
                {
                    case PING:
                        debug_String += "Receive = Ping Ack\r\n";
                        ping = true;
                        break;

                    case SETID:
                        debug_String += "Receive = Set ID Ack\r\n";
                        break;

                    case READ_MOTOR_PARA:
                        debug_String += "Receive = Read Motor Parameter Ack\r\n";
                        int freq = 0;
                        int acc = 0;
                        string motor_axis = string.Empty;
                        string motor_freq;
                        string motor_acc;

                        //MOTOR AXIS
                        if (rcvData[DATA] == 0)        motor_axis = "Axis: X ";
                        else if(rcvData[DATA] == 1) motor_axis = "Axis: Y ";
                        else if(rcvData[DATA] == 2) motor_axis = "Axis: Z ";

                        //MOTOR FREQ    
                        freq = (rcvData[DATA + 2] << 8 | rcvData[DATA + 1]);
                        motor_freq = "Freq: " + freq.ToString();

                        //MOTOR ACCELERATION
                        acc = (rcvData[DATA + 3] << 8 + rcvData[DATA + 4]);
                        motor_acc = "Acceleration: " +acc.ToString();

                        debug_String += motor_axis + motor_freq + motor_acc + "\r\n";
                        break;

                    case WRITE_MOTOR_PAPA:
                        debug_String += "Receive = Auto Ack\r\n";
                        string write_state = string.Empty;

                        if (rcvData[DATA] == 0)
                            write_state = "Success\r\n";
                        else if (rcvData[DATA] == 1)
                            write_state = "Wrong Data Length\r\n";
                        else if (rcvData[DATA] == 2)
                            write_state= "Wrong Motor Number\r\n";

                        debug_String += "Write Result: " + write_state;

                        break;

                    case SAVE_DATA:
                        debug_String += "Receive = Save Data Ack\r\n";
                        break;

                    case STATE:
                        present_state = rcvData[DATA];
                        debug_String += "Receive = State Ack\r\n";
                        break;

                    case RESTART:
                        debug_String += "Receive = RESTART Ack\r\n";
                        break;

                    case IO_STATUS:
                        ///////////////////////////////////////////////////////
                        debug_String += "Receive = IO STATUS Ack\r\n";
                        break;

                    case SETDO:
                        debug_String += "Receive = SET DO Ack\r\n";
                        break;

                    case MOTOR_MOVE:
                        debug_String += "Receive = MOTOR MOVE Ack\r\n";
                        break;

                    case MOTOR_EMERG:
                        debug_String += "Receive = MOTOR EMERGENCY Ack\r\n";
                        string error_axis = string.Empty;
                        string UL_limit = string.Empty;    //Upper = 0, Lower =1

                        //ERROR AXIS
                        if (rcvData[DATA] == 0)
                            error_axis = "X axis ";
                        else if (rcvData[DATA] == 1)
                            error_axis = "Y axis ";
                        else if (rcvData[DATA] == 2)
                            error_axis = "Z axis  ";

                        //ERROR SENSOR
                        if (rcvData[DATA + 1] == 0)
                            UL_limit = "Upper Limit ";
                        else if (rcvData[DATA + 1] == 1)
                            UL_limit = "Lower Limit ";

                        debug_String += "Error: " + error_axis + UL_limit + "\r\n";
                        break;

                    case SET_VOLT:
                        debug_String += "Receive = SET VOLTAGE Ack\r\n";
                        break;

                    case READ_VOLT:
                        debug_String += "Receive = READ VOLTAGE Ack\r\n";
                        int read_volt = (int)rcvData[DATA];
                        debug_String += "Read Voltage = " + read_volt + "\r\n";
                        break;

                }
                debug_String += "Receive Message: " + BitConverter.ToString(rcvData, 0, rcvData_len) + "\r\n";
            }
        }

        #endregion


        #region 傳送指令
        public void SendData(
                                                 SerialPort Port,
                                                 byte hmiid, 
                                                 byte cmd, 
                                                 byte setid = 0, 
                                                 byte motor_axis = 0, 
                                                 int acc = 0, 
                                                 int io = 0, 
                                                 int motor_mode = 0, 
                                                 int freq = 0,
                                                 int steps = 0,
                                                 int set_volt = 0
                                                )
        {
            Thread.Sleep(10);
            sendData[TAGID] = 0xF1;
            sendData[HMI_ID] = hmiid;
            switch (cmd)
            {
                case PING:
                    sendData[CMD_ID] = PING;
                    sendData[LEN] = PING_LEN;
                    break;

                case SETID:
                    sendData[LEN] = SETID_LEN;
                    sendData[CMD_ID] = SETID;
                    sendData[DATA] = setid;
                    break;

                case READ_MOTOR_PARA:
                    sendData[CMD_ID] = READ_MOTOR_PARA;
                    sendData[LEN] = READ_MOTOR_PARA_LEN;
                    sendData[DATA] = motor_axis;
                    break;

                case WRITE_MOTOR_PAPA:
                    sendData[LEN] = WRITE_MOTOR_PARA_LEN;
                    sendData[CMD_ID] = WRITE_MOTOR_PAPA;
                    //MOTOR AXIS
                    sendData[DATA] = motor_axis;
                    //FREQ
                    sendData[DATA + 1] = (byte)(freq >> 8);
                    sendData[DATA + 2] = (byte)(freq & 0xFF);
                    //ACCELERATION
                    sendData[DATA + 3] = (byte)(acc >> 8);
                    sendData[DATA + 4] = (byte)(acc & 0xFF);
                    break;

                case SAVE_DATA:
                    sendData[CMD_ID] = SAVE_DATA;
                    sendData[LEN] = SAVE_DATA_LEN;
                    break;

                case STATE:
                    sendData[CMD_ID] = STATE;
                    sendData[LEN] = STATE_LEN;
                    break;

                case RESTART:
                    sendData[CMD_ID] = RESTART;
                    sendData[LEN] = RESTART_LEN;
                    break;

                case IO_STATUS:
                    sendData[CMD_ID] = IO_STATUS;
                    sendData[LEN] = IO_STATUS_LEN;
                    sendData[DATA] = (byte)io;
                    break;

                case SETDO:
                    sendData[CMD_ID] = SETDO;
                    sendData[LEN] = SETDO_LEN;
                    //DI = 0 , DO = 1
                    sendData[DATA] = (byte)io;

                    break;

                case MOTOR_MOVE:
                    //steps = Convert.ToUInt32(CountStep(deg, res));
                    sendData[CMD_ID] = MOTOR_MOVE;
                    sendData[LEN] = MOTOR_MOVE_LEN;
                    //MOTOR MODE
                    sendData[DATA] = (byte)motor_mode;
                    //MOTOR STEPS
                    sendData[DATA  + 1] = (byte)((steps >> (3 - 0) * 8) & 0xff);
                    sendData[DATA + 2] = (byte)((steps >> (3 - 1) * 8) & 0xff);
                    sendData[DATA + 3] = (byte)((steps >> (3 - 2) * 8) & 0xff);
                    sendData[DATA + 4] = (byte)((steps >> (3 - 3) * 8) & 0xff);
                    break;

                case MOTOR_EMERG:
                    sendData[CMD_ID] = MOTOR_EMERG;
                    sendData[LEN] = MOTOR_EMERG_LEN;
                    break;

                case SET_VOLT:
                    sendData[CMD_ID] = SET_VOLT;
                    sendData[LEN] = SET_VOLT_LEN;
                    sendData[DATA] = (byte)set_volt;
                    break;

                case READ_VOLT:
                    sendData[CMD_ID] = READ_VOLT;
                    sendData[LEN] = READ_VOLT_LEN;
                    break;
            }
            sendData[sendData[LEN] - 1] = checkCRC(sendData, sendData[LEN]);
            SerialSend(Port, sendData, sendData[LEN]);
            debug_String += "Send: " + BitConverter.ToString(sendData, 0, sendData[LEN]) + "\r\n";
        }

        public void SerialSend(SerialPort Port, byte[] msg, int msg_len)
        {
            if (Port.IsOpen)
            {
                Thread SendData = new Thread(
                    delegate ()
                    {
                        Port.Write(msg, 0, msg_len);
                    });
                SendData.Start();
            }
            else return;
        }

        public void SerialSend_String(SerialPort Port, string msg)
        {
            if (Port.IsOpen)
            {
                Thread SendData = new Thread(
                    delegate ()
                    {
                        Port.WriteLine(msg + "\r\n");
                    });
                SendData.Start();
            }
            else return;
        }
        #endregion


        #region 計算Func.

        /// <summary>
        /// 計算PWM頻率
        /// </summary>
        /// <param name="rpm"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        public int CountFreq(float rpm, int res)
        {
            int freq = (int)(Math.Round(rpm / 60 * res));
            if (freq > 10667 || freq < 1067)
                return -1;
            else
                return freq;
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

        /// <summary>
        /// 計算角度Pulse
        /// </summary>
        /// <param name="deg"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        public int CountStep(int deg, int res)
        {
            //double deg_OK = deg / 0.9;
            //if (deg_OK == Math.Floor(deg_OK))
            //    return (int)(res * 0.9 / 360 * deg_OK); // res/deg = 6400/360 = 16/0.9 每0.9度16個pulse
            //else
            //    return -1;

            int i = (deg - 4) / 9 + 1;
            double deg_OK;
            if (deg > 0 && deg < 5)
                deg_OK = 0.9 * deg;
            else
                deg_OK = 0.9 * (i + deg);

            //debug_String += deg_OK + "\r\n";
            deg_OK = deg_OK / 0.9;
            return (int)(res * 0.9 / 360 * deg_OK); // res/deg = 6400/360 = 16/0.9 每0.9度16個pulse
        }

        #endregion

    }
}
