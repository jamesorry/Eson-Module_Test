using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Timers;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Runtime.InteropServices;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private SerialPort My_SerialPort;
        private Byte[] byteData = new Byte[128];
        private Byte[] sendData = new Byte[128];
        private Int32 datalen = 0;
        private const int TAGID = 0;
        private const int Len = 1;
        private const int CMD_ID = 2;
        private const int HMI_ID = 3;
        private const int Data = 4;
        private int times = 0;
        public List<string> WriteStringLists = new List<string>();

        private string date = DateTime.Now.ToString("yyyy.MM.dd");
        private string ntime = DateTime.Now.ToString("HH-mm-ss");
        private string FileName = "";
        private string dirPath = @"C:\doc";
        /*private string k1 = "";
        private string k2 = "";
        private string k3 = "";
        private string k4 = "";
        private string k5 = "";
        private string k0 = "";
        private string FileNamelimit = @"C:\doc\limit.csv";
        private string dirPathlimit = @"C:\doc";
        private string connectstatus = "";*/
        private LH_Command LH_cmd = new LH_Command();
        public Form1()
        {
            InitializeComponent();
            button3.Enabled = false;
            button3.Visible = false;
            My_SerialPort = new SerialPort();
            button4.Enabled = false;
            button5.Enabled = false;
            textBox2.Enabled = false;
            button_Save.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //My_SerialPort = new SerialPort();

                //if (LH_cmd.isConnect == true)
                {
                    //My_SerialPort.Close();
                }

                //設定 Serial Port 參數
                //My_SerialPort.PortName = comboBox1.Text;
                //My_SerialPort.BaudRate = 115200;
                //My_SerialPort.DataBits = 8;
                //My_SerialPort.Parity = Parity.None;
                //My_SerialPort.StopBits = StopBits.One;

                if (LH_cmd.isConnect != true)
                {
                    //開啟 Serial Port
                    //My_SerialPort.Open();
                    LH_cmd.SerialConnect(comboBox1.Text);
                    label12.Text = "已連線";
                    label12.BackColor = System.Drawing.Color.LimeGreen;
                    label2.Text = "已連線";
                    label2.BackColor = System.Drawing.Color.LimeGreen;
                    button1.Enabled = false;
                    button1.Visible = false;
                    button3.Enabled = true;
                    button3.Visible = true;
                    timer1.Enabled = true;
                    comboBox1.Enabled = false;
                    button2.Enabled = false;

                    button4.Enabled = true;
                    button5.Enabled = true;
                    textBox2.Enabled = true;
                    button_Save.Enabled = true;
                }
            }
            catch 
            (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.DataSource = SerialPort.GetPortNames();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            My_SerialPort.Close();
            label12.Text = "未連線";
            label12.BackColor = System.Drawing.Color.LightCoral;
            label2.Text = "未連線";
            label2.BackColor = System.Drawing.Color.LightCoral;
            button1.Enabled = true;
            button1.Visible = true;
            button3.Enabled = false;
            button3.Visible = false;
            button2.Enabled = true;
            comboBox1.Enabled = true;
            timer1.Stop();
            timer1.Dispose();
            timer1.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            textBox2.Enabled = false;
            button_Save.Enabled = false;
        }

        private void InitializeTimer()
        {
            // Call this procedure when the application starts.  
            // Set to 1 second.  
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);

            // Enable timer.  
            timer1.Enabled = true;

        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                if (!My_SerialPort.IsOpen)
                {
                    My_SerialPort.Close();
                    label12.Text = "未連線";
                    label12.BackColor = System.Drawing.Color.LightCoral;
                    label2.Text = "未連線";
                    label2.BackColor = System.Drawing.Color.LightCoral;
                    comboBox1.Enabled = false;
                    button1.Enabled = true;
                    button1.Visible = true;
                    button3.Enabled = false;
                    button3.Visible = false;
                    button2.Enabled = true;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    textBox2.Enabled = false;
                    button_Save.Enabled = false;
                    timer1.Stop();
                    timer1.Dispose();
                }
                DataReceivedHandler();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        //private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        private void DataReceivedHandler()
        {
            Thread.Sleep(50);
            //SerialPort My_SerialPort = (SerialPort)sender;
            int readlen = My_SerialPort.BytesToRead;
            if (readlen != 0)
            {
                datalen = My_SerialPort.Read(byteData, 0, byteData.Length);
                if (byteData[0] == 0xF1)
                {
                    if (byteData[datalen - 1] == checkCRC(byteData, datalen)) 
                    {
                        if (byteData[2] == 0x01)
                        {
                            if (byteData[4] == 0x27)
                            {
                                string a = Convert.ToString(byteData[5], 2).PadLeft(8, '0');
                                string a0 = a.Substring(0, 1); string a1 = a.Substring(1, 1); string a2 = a.Substring(2, 1); string a3 = a.Substring(3, 1);
                                string a4 = a.Substring(4, 1); string a5 = a.Substring(5, 1); string a6 = a.Substring(6, 1); string a7 = a.Substring(7, 1);
                                label3.Text = a;
                                string b = Convert.ToString(byteData[6], 2).PadLeft(8, '0');
                                string b0 = b.Substring(0, 1); string b1 = b.Substring(1, 1); string b2 = b.Substring(2, 1); string b3 = b.Substring(3, 1);
                                string b4 = b.Substring(4, 1); string b5 = b.Substring(5, 1); string b6 = b.Substring(6, 1); string b7 = b.Substring(7, 1);
                                label4.Text = b;
                                string c = Convert.ToString(byteData[7], 2).PadLeft(8, '0');
                                string c0 = c.Substring(0, 1); string c1 = c.Substring(1, 1); string c2 = c.Substring(2, 1); string c3 = c.Substring(3, 1);
                                string c4 = c.Substring(4, 1); string c5 = c.Substring(5, 1); string c6 = c.Substring(6, 1); string c7 = c.Substring(7, 1);
                                label5.Text = c;
                                string d = Convert.ToString(byteData[8], 2).PadLeft(8, '0');
                                string d0 = d.Substring(0, 1); string d1 = d.Substring(1, 1); string d2 = d.Substring(2, 1); string d3 = d.Substring(3, 1);
                                string d4 = d.Substring(4, 1); string d5 = d.Substring(5, 1); string d6 = d.Substring(6, 1); string d7 = d.Substring(7, 1);
                                label6.Text = d;
                                string e = Convert.ToString(byteData[9], 2).PadLeft(8, '0');
                                string e0 = e.Substring(0, 1); string e1 = e.Substring(1, 1); string e2 = e.Substring(2, 1); string e3 = e.Substring(3, 1);
                                string e4 = e.Substring(4, 1); string e5 = e.Substring(5, 1); string e6 = e.Substring(6, 1); string e7 = e.Substring(7, 1);
                                label7.Text = e;
                                string[] strArray = { a0, a1, a2, a3, a4, a5, a6, a7, b0, b1, b2, b3, b4, b5, b6, b7, c0, c1, c2, c3, c4, c5, c6, c7, d0, d1, d2, d3, d4, d5, d6, d7, e0, e1, e2, e3, e4, e5, e6, e7 };
                                SaveToTXT(strArray, "40");
                                label2.Text = "已連線";
                                label2.BackColor = System.Drawing.Color.LimeGreen;
                                MessageBox.Show("讀取完畢，請至 C:/doc 確認。");
                                button4.Enabled = true;
                                button5.Enabled = true;
                                textBox2.Enabled = true;
                                button_Save.Enabled = true;
                            }
                            else if (byteData[4] < 0x27 && byteData[4] >= 0x00)
                            {
                                string a = Convert.ToString(byteData[5], 2).PadLeft(8, '0');
                                string a0 = a.Substring(0, 1); string a1 = a.Substring(1, 1); string a2 = a.Substring(2, 1); string a3 = a.Substring(3, 1);
                                string a4 = a.Substring(4, 1); string a5 = a.Substring(5, 1); string a6 = a.Substring(6, 1); string a7 = a.Substring(7, 1);
                                label3.Text = a;
                                string b = Convert.ToString(byteData[6], 2).PadLeft(8, '0');
                                string b0 = b.Substring(0, 1); string b1 = b.Substring(1, 1); string b2 = b.Substring(2, 1); string b3 = b.Substring(3, 1);
                                string b4 = b.Substring(4, 1); string b5 = b.Substring(5, 1); string b6 = b.Substring(6, 1); string b7 = b.Substring(7, 1);
                                label4.Text = b;
                                string c = Convert.ToString(byteData[7], 2).PadLeft(8, '0');
                                string c0 = c.Substring(0, 1); string c1 = c.Substring(1, 1); string c2 = c.Substring(2, 1); string c3 = c.Substring(3, 1);
                                string c4 = c.Substring(4, 1); string c5 = c.Substring(5, 1); string c6 = c.Substring(6, 1); string c7 = c.Substring(7, 1);
                                label5.Text = c;
                                string d = Convert.ToString(byteData[8], 2).PadLeft(8, '0');
                                string d0 = d.Substring(0, 1); string d1 = d.Substring(1, 1); string d2 = d.Substring(2, 1); string d3 = d.Substring(3, 1);
                                string d4 = d.Substring(4, 1); string d5 = d.Substring(5, 1); string d6 = d.Substring(6, 1); string d7 = d.Substring(7, 1);
                                label6.Text = d;
                                string e = Convert.ToString(byteData[9], 2).PadLeft(8, '0');
                                string e0 = e.Substring(0, 1); string e1 = e.Substring(1, 1); string e2 = e.Substring(2, 1); string e3 = e.Substring(3, 1);
                                string e4 = e.Substring(4, 1); string e5 = e.Substring(5, 1); string e6 = e.Substring(6, 1); string e7 = e.Substring(7, 1);
                                label7.Text = e;
                                string[] strArray = { a0, a1, a2, a3, a4, a5, a6, a7, b0, b1, b2, b3, b4, b5, b6, b7, c0, c1, c2, c3, c4, c5, c6, c7, d0, d1, d2, d3, d4, d5, d6, d7, e0, e1, e2, e3, e4, e5, e6, e7 };
                                //string ans = String.Concat(strArray);
                                //MessageBox.Show(ans);
                                string outp = Convert.ToString(byteData[4] + 1);
                                SaveToTXT(strArray, outp);
                                SendData((byte)(byteData[4] + 0x01));
                            }
                        }
                        else if(byteData[2] == 0x02) 
                        {
                            if (byteData[4] == 0x27)
                            {
                                label2.Text = "已連線";
                                label2.BackColor = System.Drawing.Color.LimeGreen;
                                MessageBox.Show("寫入完畢。");
                                button4.Enabled = true;
                                button5.Enabled = true;
                                textBox2.Enabled = true;
                                button_Save.Enabled = true;
                            }
                            else if (byteData[4] < 0x27 && byteData[4] >= 0x00)
                            {
                                SendDataToWrite((byte)(byteData[4] + 0x01));
                            }
                        }
                        else if(byteData[2] == 0x03)
                        {
                            label2.Text = "已連線";
                            label2.BackColor = System.Drawing.Color.LimeGreen;
                            MessageBox.Show("儲存完畢。");
                            button4.Enabled = true;
                            button5.Enabled = true;
                            textBox2.Enabled = true;
                            button_Save.Enabled = true;
                        }
                        
                    }
                }
            }
        }

        protected string ToHexString(byte[] bytes)
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder str = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    str.Append(bytes[i].ToString("X2"));
                }
                hexString = str.ToString();
            }
            return hexString;
        }

        protected byte checkCRC(byte[] bytes, int length)
        {
            byte cRc = 0x00;
            for (int i = 0; i < (length - 1); i++)
            {
                cRc -= bytes[i];
            }
            return cRc;
        }

        protected void SaveToTXT(string[] data_in, string outp)
        {
            if (Directory.Exists(dirPath))
            {
                if (System.IO.File.Exists(FileName))
                {
                    //MessageBox.Show(FileName + " 檔案存在");
                    StreamWriter sw = new StreamWriter(FileName, true);
                    sw.WriteLine("outputpin: " + outp + "," + data_in[0] + "," + data_in[1] + "," + data_in[2] + "," + data_in[3] + "," + data_in[4] + "," + data_in[5] + "," + data_in[6] + "," + data_in[7]
                        + "," + data_in[8] + "," + data_in[9] + "," + data_in[10] + "," + data_in[11] + "," + data_in[12] + "," + data_in[13] + "," + data_in[14] + "," + data_in[15] 
                        + "," + data_in[16] + "," + data_in[17] + "," + data_in[18] + "," + data_in[19] + "," + data_in[20] + "," + data_in[21] + "," + data_in[22] + "," + data_in[23]
                        + "," + data_in[24] + "," + data_in[25] + "," + data_in[26] + "," + data_in[27] + "," + data_in[28] + "," + data_in[29] + "," + data_in[30] + "," + data_in[31]
                        + "," + data_in[32] + "," + data_in[33] + "," + data_in[34] + "," + data_in[35] + "," + data_in[36] + "," + data_in[37] + "," + data_in[38] + "," + data_in[39]);
                    sw.Close();
                }
                else
                {
                    //MessageBox.Show(FileName + " 檔案不存在");
                    StreamWriter sw = new StreamWriter(FileName, true);
                    sw.WriteLine(" " + "," + "Inputpin: 1" + "," + "Inputpin: 2" + "," + "Inputpin: 3" + "," + "Inputpin: 4" + "," + "Inputpin: 5" + "," + "Inputpin: 6" + "," + "Inputpin: 7" + "," + "Inputpin: 8"
                        + "," + "Inputpin: 9" + "," + "Inputpin: 10" + "," + "Inputpin: 11" + "," + "Inputpin: 12" + "," + "Inputpin: 13" + "," + "Inputpin: 14" + "," + "Inputpin: 15" + "," + "Inputpin: 16"
                        + "," + "Inputpin: 17" + "," + "Inputpin: 18" + "," + "Inputpin: 19" + "," + "Inputpin: 20" + "," + "Inputpin: 21" + "," + "Inputpin: 22" + "," + "Inputpin: 23" + "," + "Inputpin: 24"
                        + "," + "Inputpin: 25" + "," + "Inputpin: 26" + "," + "Inputpin: 27" + "," + "Inputpin: 28" + "," + "Inputpin: 29" + "," + "Inputpin: 30" + "," + "Inputpin: 31" + "," + "Inputpin: 32"
                        + "," + "Inputpin: 33" + "," + "Inputpin: 34" + "," + "Inputpin: 35" + "," + "Inputpin: 36" + "," + "Inputpin: 37" + "," + "Inputpin: 38" + "," + "Inputpin: 39" + "," + "Inputpin: 40");

                    sw.WriteLine("outputpin: " + outp + "," + data_in[0] + "," + data_in[1] + "," + data_in[2] + "," + data_in[3] + "," + data_in[4] + "," + data_in[5] + "," + data_in[6] + "," + data_in[7]
                        + "," + data_in[8] + "," + data_in[9] + "," + data_in[10] + "," + data_in[11] + "," + data_in[12] + "," + data_in[13] + "," + data_in[14] + "," + data_in[15]
                        + "," + data_in[16] + "," + data_in[17] + "," + data_in[18] + "," + data_in[19] + "," + data_in[20] + "," + data_in[21] + "," + data_in[22] + "," + data_in[23]
                        + "," + data_in[24] + "," + data_in[25] + "," + data_in[26] + "," + data_in[27] + "," + data_in[28] + "," + data_in[29] + "," + data_in[30] + "," + data_in[31]
                        + "," + data_in[32] + "," + data_in[33] + "," + data_in[34] + "," + data_in[35] + "," + data_in[36] + "," + data_in[37] + "," + data_in[38] + "," + data_in[39]);
                    sw.Close();
                }
            }
            else
            {
                Directory.CreateDirectory(dirPath);
                if (System.IO.File.Exists(FileName))
                {
                    //MessageBox.Show(FileName + " 檔案存在"); 
                    StreamWriter sw = new StreamWriter(FileName, true);
                    sw.WriteLine("outputpin: " + outp + "," + data_in[0] + "," + data_in[1] + "," + data_in[2] + "," + data_in[3] + "," + data_in[4] + "," + data_in[5] + "," + data_in[6] + "," + data_in[7]
                        + "," + data_in[8] + "," + data_in[9] + "," + data_in[10] + "," + data_in[11] + "," + data_in[12] + "," + data_in[13] + "," + data_in[14] + "," + data_in[15]
                        + "," + data_in[16] + "," + data_in[17] + "," + data_in[18] + "," + data_in[19] + "," + data_in[20] + "," + data_in[21] + "," + data_in[22] + "," + data_in[23]
                        + "," + data_in[24] + "," + data_in[25] + "," + data_in[26] + "," + data_in[27] + "," + data_in[28] + "," + data_in[29] + "," + data_in[30] + "," + data_in[31]
                        + "," + data_in[32] + "," + data_in[33] + "," + data_in[34] + "," + data_in[35] + "," + data_in[36] + "," + data_in[37] + "," + data_in[38] + "," + data_in[39]);
                    sw.Close();
                }
                else
                {
                    //MessageBox.Show(FileName + " 檔案不存在");
                    StreamWriter sw = new StreamWriter(FileName, true);
                    sw.WriteLine(" " +  "," + "Inputpin: 1" + "," + "Inputpin: 2" + "," + "Inputpin: 3" + "," + "Inputpin: 4" + "," + "Inputpin: 5" + "," + "Inputpin: 6" + "," + "Inputpin: 7" + "," + "Inputpin: 8"
                        + "," + "Inputpin: 9" + "," + "Inputpin: 10" + "," + "Inputpin: 11" + "," + "Inputpin: 12" + "," + "Inputpin: 13" + "," + "Inputpin: 14" + "," + "Inputpin: 15" + "," + "Inputpin: 16"
                        + "," + "Inputpin: 17" + "," + "Inputpin: 18" + "," + "Inputpin: 19" + "," + "Inputpin: 20" + "," + "Inputpin: 21" + "," + "Inputpin: 22" + "," + "Inputpin: 23" + "," + "Inputpin: 24"
                        + "," + "Inputpin: 25" + "," + "Inputpin: 26" + "," + "Inputpin: 27" + "," + "Inputpin: 28" + "," + "Inputpin: 29" + "," + "Inputpin: 30" + "," + "Inputpin: 31" + "," + "Inputpin: 32"
                        + "," + "Inputpin: 33" + "," + "Inputpin: 34" + "," + "Inputpin: 35" + "," + "Inputpin: 36" + "," + "Inputpin: 37" + "," + "Inputpin: 38" + "," + "Inputpin: 39" + "," + "Inputpin: 40");

                    sw.WriteLine("outputpin: " + outp + "," + data_in[0] + "," + data_in[1] + "," + data_in[2] + "," + data_in[3] + "," + data_in[4] + "," + data_in[5] + "," + data_in[6] + "," + data_in[7]
                        + "," + data_in[8] + "," + data_in[9] + "," + data_in[10] + "," + data_in[11] + "," + data_in[12] + "," + data_in[13] + "," + data_in[14] + "," + data_in[15]
                        + "," + data_in[16] + "," + data_in[17] + "," + data_in[18] + "," + data_in[19] + "," + data_in[20] + "," + data_in[21] + "," + data_in[22] + "," + data_in[23]
                        + "," + data_in[24] + "," + data_in[25] + "," + data_in[26] + "," + data_in[27] + "," + data_in[28] + "," + data_in[29] + "," + data_in[30] + "," + data_in[31]
                        + "," + data_in[32] + "," + data_in[33] + "," + data_in[34] + "," + data_in[35] + "," + data_in[36] + "," + data_in[37] + "," + data_in[38] + "," + data_in[39]);
                    sw.Close();
                }
            }
        }

        protected Boolean floattest(string f)
        {
            try
            {
                float i = float.Parse(f);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            times = times + 1;
            if(textBox2.Text != "")
            {
                FileName = @"C:\doc\" + textBox2.Text + ".csv";
                if (Directory.Exists(dirPath))
                {
                    if (System.IO.File.Exists(FileName))
                    {
                        MessageBox.Show("檔案名稱重複: " + textBox2.Text + ".csv");
                    }
                    else
                    {
                        SendData(0x00);
                        label2.Text = "讀取中...";
                        label2.BackColor = System.Drawing.Color.LightCoral;
                        button4.Enabled = false;
                        button5.Enabled = false;
                        textBox2.Enabled = false;
                        button_Save.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("C槽底下沒有 doc 資料夾" + "請先新增 doc 資料夾");
                }
            }
            else
            {
                MessageBox.Show("名稱不能為空白");
            }
            

        }

        private void SendData(byte data)
        {
            Thread.Sleep(50);
            sendData[TAGID] = 0xF9;
            sendData[Len] = 0x06;
            sendData[CMD_ID] = 0x01;
            sendData[HMI_ID] = 0x00;
            sendData[Data] = data;
            sendData[Data + 1] = checkCRC(sendData, sendData[Len]);
            My_SerialPort.Write(sendData, 0, sendData[Len]);
        }

        private void SendDataToSave()
        {
            Thread.Sleep(50);
            sendData[TAGID] = 0xF9;
            sendData[Len] = 0x05;
            sendData[CMD_ID] = 0x03;
            sendData[HMI_ID] = 0x00;
            sendData[sendData[Len] - 1] = checkCRC(sendData, sendData[Len]);
            My_SerialPort.Write(sendData, 0, sendData[Len]);
        }

        private void SendDataToWrite(byte outpin)
        {
            Thread.Sleep(50);
            sendData[TAGID] = 0xF9;
            sendData[Len] = 0x0B;
            sendData[CMD_ID] = 0x02;
            sendData[HMI_ID] = 0x00;
            sendData[Data] = outpin;
            ConsoleShow(WriteStringLists[outpin + 0x01].ToString());
            int numOfBytes = WriteStringLists[outpin + 0x01].Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int test = 0; test < numOfBytes; ++test)
            {
                bytes[test] = Convert.ToByte(WriteStringLists[outpin + 0x01].Substring(8 * test, 8), 2);
            }
            //byte myByte = bytes[0];
            for(int j=1; j<=5; j++)
            {
                sendData[Data + j] = bytes[j-1];
            }
            sendData[sendData[Len] - 1] = checkCRC(sendData, sendData[Len]);
            My_SerialPort.Write(sendData, 0, sendData[Len]);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //開始寫入
            string resultFile = "";
            string result = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "C:\\doc",
                Filter = "All files(*.*)| *.*|csv files(*.csv) | *.csv|txt files(*.txt) | *.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                resultFile = openFileDialog1.FileName;
                MessageBox.Show(resultFile);
                StreamReader sr = new StreamReader(resultFile);
                int i = 0;
                WriteStringLists.Clear();
                while (!sr.EndOfStream)
                {
                    string a = sr.ReadLine();
                    string[] subs = a.Split(',');
                    result = "";
                    for (int res = 1; res < subs.Length; res++)
                    {
                        result += subs[res];
                    }
                    WriteStringLists.Add(result);
                    i++;
                }
                //MessageBox.Show("總共" + i.ToString() + "筆資料，匯入完成!");
                //MessageBox.Show(+"\n" + WriteStringLists.Count.ToString());
                //MessageBox.Show(WriteStringLists[1]);  //1~40筆資料才是需要的值
                //ConsoleShow(WriteStringLists[1].ToString());
                sr.Close();
                SendDataToWrite(0x00);
                label2.Text = "寫入中...";
                label2.BackColor = System.Drawing.Color.LightCoral;
                button4.Enabled = false;
                button5.Enabled = false;
                textBox2.Enabled = false;
                button_Save.Enabled = false;
                /*
                //測試實驗
                int numOfBytes = WriteStringLists[1].Length / 8;
                byte[] bytes = new byte[numOfBytes];
                for (int test = 0; test < numOfBytes; ++test)
                {
                    bytes[test] = Convert.ToByte(WriteStringLists[1].Substring(8 * test, 8), 2);
                }
                byte myByte = bytes[0];
                MessageBox.Show(WriteStringLists[1]);  //1~40筆資料才是需要的值
                MessageBox.Show(ToHexString(bytes) + ", " + myByte.ToString());
                */
            }
        }
        public void ConsoleShow(string buffer)
        {
            textBox1.AppendText(buffer + "\r\n");
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            SendDataToSave();
            label2.Text = "儲存中...";
            label2.BackColor = System.Drawing.Color.LightCoral;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
