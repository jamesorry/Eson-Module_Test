using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Eson_Pega
{
    public partial class main : Form
    {
        #region Private param.
        private byte UploadCRC = 0x00;
        #endregion

        #region Public param.
        public int DataUploadRowNum = 0;
        #endregion

        #region Class
        SerialPort Port = new SerialPort();
        RcvSend_Data rcvsend = new RcvSend_Data();
        #endregion

        #region Thread
        Thread RefreshingUIThread;
        #endregion
        public main()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            RefreshingUIThread = new Thread(new ThreadStart(RefreshUI));
            RefreshingUIThread.Start();
            InitMain();
        }
        private void InitMain()
        {
            comboBox_BinaryType.SelectedIndex = 0;
        }


        #region Form Func
        private void main_Load(object sender, EventArgs e)
        {
            GetComPort();
        }

        #endregion


        #region Button Func
        private void connect_button_Click(object sender, EventArgs e)
        {
            if (Port.IsOpen)
            {
                try
                {
                    Port.Close();
                    connect_button.BackColor = Control.DefaultBackColor;
                    connect_button.Text = "連線";
                    Port.DataReceived -= new SerialDataReceivedEventHandler(Port_DataReceived);
                    rcvsend.present_state = 0x03;
                    return;
                }
                catch (Exception disconn)
                {
                    MessageBox.Show(disconn.ToString());
                }
            }
            if (!string.IsNullOrEmpty(COM_comboBox.Text) && !Port.IsOpen)
            {
                try
                {
                    //ComPort設定
                    Port.PortName = COM_comboBox.Text;
                    Port.BaudRate = 115200;
                    Port.StopBits = StopBits.One;
                    Port.Parity = Parity.None;
                    Port.ReadTimeout = 300;
                    Port.Handshake = Handshake.None;

                    Port.Open();
                    Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);

                    //Ping
                    //Thread.Sleep(1000);
                    int i = 100;
                    //rcvsend.SendData(Port, 0x00, RcvSend_Data.PING);
                    while (rcvsend.ping == false)
                    {
                        if (i > 0)
                        {
                            i--;
                            Thread.Sleep(1);
                        }
                        else
                        {
                            rcvsend.ping = true;
                            //MessageBox.Show("未正確連接HMI");
                            break;
                        }
                    }

                    if (rcvsend.ping == true)
                    {
                        //debug_textBox.AppendText(rcvsend.debug_String);
                        //rcvsend.debug_String = string.Empty;
                        connect_button.BackColor = Color.LightGreen;
                        connect_button.Text = "斷線";
                        rcvsend.ping = false;
                    }

                    else
                    {
                        Port.Close();
                        rcvsend.ping = false;
                        Port.DataReceived -= new SerialDataReceivedEventHandler(Port_DataReceived);
                    }

                }
                catch (Exception conn)
                {
                    MessageBox.Show(conn.ToString());
                }
            }
        }

        private void Refresh_button_Click(object sender, EventArgs e)
        {
            GetComPort();
        }

        #endregion


        #region Serial Func
        protected override void WndProc(ref Message m)
        {
            const int WM_DEVICECHANGE = 0x219; //設備改變
            const int DBT_DEVICEARRIVAL = 0x8000; //檢測到新設備
            const int DBT_DEVICEREMOVECOMPLETE = 0x8004; //移除設備
            //const int DBT_DEVTYP_PORT = 0x00000003;
            base.WndProc(ref m);//調用父類方法，以確保其他功能正常
            switch (m.Msg)
            {
                case WM_DEVICECHANGE://設備改變事件
                    switch ((int)m.WParam)
                    {
                        case DBT_DEVICEARRIVAL:
                            //int dbccSize = Marshal.ReadInt32(m.LParam, 0);
                            //int devType = Marshal.ReadInt32(m.LParam, 4);                            
                            break;

                        case DBT_DEVICEREMOVECOMPLETE:
                            if (!Port.IsOpen)
                            {
                                Port.Close();
                                Port.DataReceived -= new SerialDataReceivedEventHandler(Port_DataReceived);
                                connect_button.BackColor = Control.DefaultBackColor;
                                connect_button.Text = "連線";
                            }
                            break;
                    }
                    //刷新串口設備
                    GetComPort();
                    break;
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(10);  //（毫秒）等待一定時間，確保資料的完整性 int len      
            if (Port.IsOpen)
            {
                int len = Port.BytesToRead;
                if (len != 0)
                {
                    string buff = Port.ReadLine();
                    rcvsend.GetCmd_string(buff);
                    /*
                    byte[] buff = new byte[len];
                    Port.Read(buff, 0, len);
                    buff.CopyTo(rcvsend.rcvbuff, 0);
                    rcvsend.GetCmd();
                    */
                }
            }
        }

        private void GetComPort()
        {
            string[] names = SerialPort.GetPortNames();
            COM_comboBox.Items.Clear();
            COM_comboBox.Text = string.Empty;
            foreach (string s in names)
            {
                COM_comboBox.Items.Add(s);
            }

            if (names.Length > 0)
            {
                COM_comboBox.SelectedIndex = 0;
            }
        }

        #endregion


        #region Other Function
        private void RefreshUI()
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(rcvsend.debug_String))
                {
                    debug_textBox.AppendText(rcvsend.debug_String);
                    rcvsend.debug_String = string.Empty;
                }
                Thread.Sleep(50);
            }
        }

        #endregion

        #region test
        private void SetID_button_Click(object sender, EventArgs e)
        {
            rcvsend.SendData(Port, 0x00, RcvSend_Data.SETID, setid: 0x00);
        }

        #endregion

        #region Buttun Test
        private void Read_motor_button_Click(object sender, EventArgs e)
        {
            rcvsend.SendData(Port, 0x00, RcvSend_Data.READ_MOTOR_PARA, motor_axis: 0);
        }

        private void write_motor_button_Click(object sender, EventArgs e)
        {
            rcvsend.SendData(Port, 0x00, RcvSend_Data.WRITE_MOTOR_PAPA, motor_axis: 0, freq: 1000, acc: 10);
        }

        private void save_data_button_Click(object sender, EventArgs e)
        {
            rcvsend.SendData(Port, 0x00, RcvSend_Data.SAVE_DATA);
        }

        private void state_button_Click(object sender, EventArgs e)
        {
            rcvsend.SendData(Port, 0x00, RcvSend_Data.STATE);
        }

        private void restart_button_Click(object sender, EventArgs e)
        {
            rcvsend.SendData(Port, 0x00, RcvSend_Data.RESTART);
        }

        private void IO_status_button_Click(object sender, EventArgs e)
        {
            rcvsend.SendData(Port, 0x00, RcvSend_Data.IO_STATUS, io: 0);
        }

        private void motor_move_button_Click(object sender, EventArgs e)
        {
            rcvsend.SendData(Port, 0x00, RcvSend_Data.MOTOR_MOVE, motor_axis: 0, steps: 1600);
        }

        private void motor_emerg_button_Click(object sender, EventArgs e)
        {
            //rcvsend.SendData(Port, 0x00,)
        }

        private void set_volt_button_Click(object sender, EventArgs e)
        {
            rcvsend.SendData(Port, 0x00, RcvSend_Data.SET_VOLT, set_volt: 10);
        }

        private void read_volt_button_Click(object sender, EventArgs e)
        {
            rcvsend.SendData(Port, 0x00, RcvSend_Data.READ_VOLT);
        }
        #endregion

        #region Comnmand Test
        public void SendDataString(string stringData)
        {
            Byte[] byteData = Encoding.ASCII.GetBytes(stringData);
            //SendData(byteData, byteData.Length);
        }

        public void SendDataHex(string hexData)
        {
            if (isExists(hexData))
            {
                MessageBox.Show("Hex 格式內容錯誤\r\n只能輸入0~9 A~F");
            }
            else if (hexData.Length % 2 != 0)
            {
                MessageBox.Show("Hex 長度錯誤");
            }
            else
            {
                Byte[] byteData = GetBytes(hexData);
                //SendData(byteData, byteData.Length);
                //MessageBox.Show("ToHexString: " + ToHexString(byteData, byteData.Length));
            }
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
            //MessageBox.Show("Byte Length: " + bytes.Length.ToString());
            return bytes;
        }

        private byte HexToByte(string hex)
        {
            if (hex.Length > 2 || hex.Length <= 0)
            {
                throw new ArgumentException("hex must be 1 or 2 characters in length");
                //MessageBox.Show("hex must be 1 or 2 characters in length");
            }
            byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return newByte;
        }


        public bool isExists(string str)
        {
            return (!string.IsNullOrEmpty(str) && !Regex.IsMatch(str, "^[A-F0-9]+$")) ? true : false;
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshingUIThread.Abort();
        }

        private void dataGridView_DataCollect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    //if (e.RowIndex < TotalDataNum)
                    {
                        //SendData(e.RowIndex);
                        ConsoleShow(e.RowIndex.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ConsoleShow(string buffer)
        {
            debug_textBox.AppendText(buffer + "\r\n");
        }

        private void button_SendData_Click(object sender, EventArgs e)
        {
            if(textBox_CmdDataInput.Text != "")
                SendDataHex(textBox_CmdDataInput.Text);
            else
            {
                MessageBox.Show("內容不能為空白");
            }
        }
        private bool HexStringDataCheck(string StringBuffer)
        {
            bool result = false;
            if (isExists(StringBuffer))
            {
                MessageBox.Show("Hex 格式內容錯誤\r\n只能輸入0~9 A~F");
            }
            else if (StringBuffer.Length % 2 != 0)
            {
                MessageBox.Show("Hex 長度錯誤");
            }
            else
            {
                Byte[] byteData = GetBytes(StringBuffer);
                //MessageBox.Show("ToHexString: " + ToHexString(byteData, byteData.Length));
                UploadCRC = rcvsend.checkCRC(byteData, byteData.Length + 1);
                //MessageBox.Show("UploadCRC: " + UploadCRC.ToString("X2"));
                result = true;
            }
            return result;
        }
        private void button_Upload_Click(object sender, EventArgs e)
        {
            if (textBox_CmdDataInput.Text != "")
            {
                if (HexStringDataCheck(textBox_CmdDataInput.Text))
                {
                    dataGridView_DataCollect.Rows.Add();
                    //MessageBox.Show("Rows.Count: " + dataGridView_DataCollect.Rows.Count.ToString());
                    //MessageBox.Show("DataUploadRowNum: " + DataUploadRowNum.ToString());
                    dataGridView_DataCollect.Rows[DataUploadRowNum].Cells[3].Value = textBox_CmdDataInput.Text;
                    dataGridView_DataCollect.Rows[DataUploadRowNum].Cells[4].Value = UploadCRC.ToString("X2");
                    DataUploadRowNum++;
                    for (int check = 0; check < dataGridView_DataCollect.Rows.Count - 1; check++)
                    {
                        dataGridView_DataCollect.Rows[check].Cells[0].Value = (check + 1).ToString();
                        dataGridView_DataCollect.Rows[check].Cells[1].Value = true;
                        dataGridView_DataCollect.Rows[check].Cells[2].Value = "Send";
                    }
                }
            }
            else
            {
                MessageBox.Show("內容不能為空白");
            }
        }

        private void button_CheckCRC_Click(object sender, EventArgs e)
        {
            if (textBox_CmdDataInput.Text != "")
            {
                if (HexStringDataCheck(textBox_CmdDataInput.Text))
                {
                    textBox_CheckCRC.Text = UploadCRC.ToString("X2");
                }
            }
            else
            {
                MessageBox.Show("內容不能為空白");
            }
        }
        #endregion

        #region ConvertData

        #endregion

        private void button_DecToHex_Click(object sender, EventArgs e)
        {
            if(comboBox_BinaryType.SelectedIndex == 0) //type: number
            {
                textBox_HexOutput.Text = textBox_DecInput.Text;
            }
            else
            {
                int DecInput = Int32.Parse(textBox_DecInput.Text);
                if (DecInput < 0)
                {
                    string hexValue = DecInput.ToString("X2");
                    if (hexValue.Length - comboBox_BinaryType.SelectedIndex * 2 >= 0)
                        textBox_HexOutput.Text = hexValue.Substring(hexValue.Length - comboBox_BinaryType.SelectedIndex * 2);
                    else
                    {
                        textBox_HexOutput.Text = "";
                    }
                }
                else
                {
                    string hexValue = DecInput.ToString("X2");
                    string zero = "";
                    //MessageBox.Show((comboBox_BinaryType.SelectedIndex * 2).ToString() + ", " + hexValue.Length.ToString());
                    if (comboBox_BinaryType.SelectedIndex * 2 - hexValue.Length > 0)
                    {
                        for(int add = 0; add< comboBox_BinaryType.SelectedIndex * 2 - hexValue.Length; add++)
                        {
                            zero += "0";
                            
                        }
                        textBox_HexOutput.Text = zero + hexValue;
                    }
                    else if(comboBox_BinaryType.SelectedIndex * 2 - hexValue.Length == 0)
                    {
                        textBox_HexOutput.Text = hexValue;
                    }
                    else
                    {
                        textBox_HexOutput.Text = "";
                    }
                }
            }
        }

        private void SW_1_ON_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBT 1 ON");
        }

        private void SW_1_OFF_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBT 1 OFF");
        }

        private void SW_2_ON_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBT 2 ON");
        }

        private void SW_2_OFF_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBT 2 OFF");
        }

        private void SW_3_ON_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBT 3 ON");
        }

        private void SW_3_OFF_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBT 3 OFF");
        }

        private void SW_4_ON_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBT 4 ON");
        }

        private void SW_4_OFF_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBT 4 OFF");
        }

        private void SW_5_ON_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBT 5 ON");
        }

        private void SW_5_OFF_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBT 5 OFF");
        }

        private void SBE_ON_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBE ON");
        }

        private void SBE_OFF_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SBE OFF");
        }

        private void SB_Status_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "SB_Status");
        }

        private void Reset_CNT_Click(object sender, EventArgs e)
        {
            rcvsend.SerialSend_String(Port, "Reset_CNT");
        }
    }
}
