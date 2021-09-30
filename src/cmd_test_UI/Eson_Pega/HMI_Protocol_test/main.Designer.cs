
namespace Eson_Pega
{
    partial class main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Refresh_button = new System.Windows.Forms.Button();
            this.COM_comboBox = new System.Windows.Forms.ComboBox();
            this.connect_button = new System.Windows.Forms.Button();
            this.debug_textBox = new System.Windows.Forms.TextBox();
            this.SetID_button = new System.Windows.Forms.Button();
            this.Read_motor_button = new System.Windows.Forms.Button();
            this.set_volt_button = new System.Windows.Forms.Button();
            this.motor_emerg_button = new System.Windows.Forms.Button();
            this.motor_move_button = new System.Windows.Forms.Button();
            this.IO_status_button = new System.Windows.Forms.Button();
            this.restart_button = new System.Windows.Forms.Button();
            this.state_button = new System.Windows.Forms.Button();
            this.save_data_button = new System.Windows.Forms.Button();
            this.write_motor_button = new System.Windows.Forms.Button();
            this.read_volt_button = new System.Windows.Forms.Button();
            this.dataGridView_DataCollect = new System.Windows.Forms.DataGridView();
            this.Column1_Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2_CheckBtn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column3_SendBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column4_Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5_CRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_CmdDataInput = new System.Windows.Forms.TextBox();
            this.button_SendData = new System.Windows.Forms.Button();
            this.button_Upload = new System.Windows.Forms.Button();
            this.button_CheckCRC = new System.Windows.Forms.Button();
            this.textBox_CheckCRC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox_CommandTest = new System.Windows.Forms.GroupBox();
            this.textBox_DecInput = new System.Windows.Forms.TextBox();
            this.textBox_HexOutput = new System.Windows.Forms.TextBox();
            this.button_DecToHex = new System.Windows.Forms.Button();
            this.comboBox_BinaryType = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SW_1_ON = new System.Windows.Forms.Button();
            this.SW_1_OFF = new System.Windows.Forms.Button();
            this.SW_2_OFF = new System.Windows.Forms.Button();
            this.SW_2_ON = new System.Windows.Forms.Button();
            this.SW_4_OFF = new System.Windows.Forms.Button();
            this.SW_4_ON = new System.Windows.Forms.Button();
            this.SW_3_OFF = new System.Windows.Forms.Button();
            this.SW_3_ON = new System.Windows.Forms.Button();
            this.SBE_OFF = new System.Windows.Forms.Button();
            this.SBE_ON = new System.Windows.Forms.Button();
            this.SW_5_OFF = new System.Windows.Forms.Button();
            this.SW_5_ON = new System.Windows.Forms.Button();
            this.SB_Status = new System.Windows.Forms.Button();
            this.Reset_CNT = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DataCollect)).BeginInit();
            this.groupBox_CommandTest.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Refresh_button);
            this.groupBox1.Controls.Add(this.COM_comboBox);
            this.groupBox1.Controls.Add(this.connect_button);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(19, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(329, 61);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USB連線";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(7, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 19);
            this.label8.TabIndex = 20;
            this.label8.Text = "ComPort";
            // 
            // Refresh_button
            // 
            this.Refresh_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Refresh_button.BackgroundImage")));
            this.Refresh_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Refresh_button.Location = new System.Drawing.Point(189, 21);
            this.Refresh_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Refresh_button.Name = "Refresh_button";
            this.Refresh_button.Size = new System.Drawing.Size(29, 28);
            this.Refresh_button.TabIndex = 5;
            this.Refresh_button.UseVisualStyleBackColor = true;
            this.Refresh_button.Click += new System.EventHandler(this.Refresh_button_Click);
            // 
            // COM_comboBox
            // 
            this.COM_comboBox.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.COM_comboBox.FormattingEnabled = true;
            this.COM_comboBox.IntegralHeight = false;
            this.COM_comboBox.Location = new System.Drawing.Point(84, 21);
            this.COM_comboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.COM_comboBox.Name = "COM_comboBox";
            this.COM_comboBox.Size = new System.Drawing.Size(97, 28);
            this.COM_comboBox.TabIndex = 0;
            // 
            // connect_button
            // 
            this.connect_button.Location = new System.Drawing.Point(224, 21);
            this.connect_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(77, 30);
            this.connect_button.TabIndex = 4;
            this.connect_button.Text = "連線";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // debug_textBox
            // 
            this.debug_textBox.Location = new System.Drawing.Point(1033, 29);
            this.debug_textBox.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.debug_textBox.Multiline = true;
            this.debug_textBox.Name = "debug_textBox";
            this.debug_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.debug_textBox.Size = new System.Drawing.Size(363, 600);
            this.debug_textBox.TabIndex = 22;
            // 
            // SetID_button
            // 
            this.SetID_button.Location = new System.Drawing.Point(713, 29);
            this.SetID_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SetID_button.Name = "SetID_button";
            this.SetID_button.Size = new System.Drawing.Size(80, 28);
            this.SetID_button.TabIndex = 21;
            this.SetID_button.Text = "SetID";
            this.SetID_button.UseVisualStyleBackColor = true;
            this.SetID_button.Visible = false;
            this.SetID_button.Click += new System.EventHandler(this.SetID_button_Click);
            // 
            // Read_motor_button
            // 
            this.Read_motor_button.Location = new System.Drawing.Point(713, 64);
            this.Read_motor_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Read_motor_button.Name = "Read_motor_button";
            this.Read_motor_button.Size = new System.Drawing.Size(80, 28);
            this.Read_motor_button.TabIndex = 23;
            this.Read_motor_button.Text = "Read Motor";
            this.Read_motor_button.UseVisualStyleBackColor = true;
            this.Read_motor_button.Visible = false;
            this.Read_motor_button.Click += new System.EventHandler(this.Read_motor_button_Click);
            // 
            // set_volt_button
            // 
            this.set_volt_button.Location = new System.Drawing.Point(713, 324);
            this.set_volt_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_volt_button.Name = "set_volt_button";
            this.set_volt_button.Size = new System.Drawing.Size(80, 28);
            this.set_volt_button.TabIndex = 24;
            this.set_volt_button.Text = "Set Voltage";
            this.set_volt_button.UseVisualStyleBackColor = true;
            this.set_volt_button.Visible = false;
            this.set_volt_button.Click += new System.EventHandler(this.set_volt_button_Click);
            // 
            // motor_emerg_button
            // 
            this.motor_emerg_button.Location = new System.Drawing.Point(713, 291);
            this.motor_emerg_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.motor_emerg_button.Name = "motor_emerg_button";
            this.motor_emerg_button.Size = new System.Drawing.Size(80, 28);
            this.motor_emerg_button.TabIndex = 25;
            this.motor_emerg_button.Text = "Motor Emerg";
            this.motor_emerg_button.UseVisualStyleBackColor = true;
            this.motor_emerg_button.Visible = false;
            this.motor_emerg_button.Click += new System.EventHandler(this.motor_emerg_button_Click);
            // 
            // motor_move_button
            // 
            this.motor_move_button.Location = new System.Drawing.Point(713, 259);
            this.motor_move_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.motor_move_button.Name = "motor_move_button";
            this.motor_move_button.Size = new System.Drawing.Size(80, 28);
            this.motor_move_button.TabIndex = 26;
            this.motor_move_button.Text = "Motor move";
            this.motor_move_button.UseVisualStyleBackColor = true;
            this.motor_move_button.Visible = false;
            this.motor_move_button.Click += new System.EventHandler(this.motor_move_button_Click);
            // 
            // IO_status_button
            // 
            this.IO_status_button.Location = new System.Drawing.Point(713, 226);
            this.IO_status_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IO_status_button.Name = "IO_status_button";
            this.IO_status_button.Size = new System.Drawing.Size(80, 28);
            this.IO_status_button.TabIndex = 27;
            this.IO_status_button.Text = "IO Status";
            this.IO_status_button.UseVisualStyleBackColor = true;
            this.IO_status_button.Visible = false;
            this.IO_status_button.Click += new System.EventHandler(this.IO_status_button_Click);
            // 
            // restart_button
            // 
            this.restart_button.Location = new System.Drawing.Point(713, 194);
            this.restart_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.restart_button.Name = "restart_button";
            this.restart_button.Size = new System.Drawing.Size(80, 28);
            this.restart_button.TabIndex = 28;
            this.restart_button.Text = "Restart";
            this.restart_button.UseVisualStyleBackColor = true;
            this.restart_button.Visible = false;
            this.restart_button.Click += new System.EventHandler(this.restart_button_Click);
            // 
            // state_button
            // 
            this.state_button.Location = new System.Drawing.Point(713, 161);
            this.state_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.state_button.Name = "state_button";
            this.state_button.Size = new System.Drawing.Size(80, 28);
            this.state_button.TabIndex = 29;
            this.state_button.Text = "State";
            this.state_button.UseVisualStyleBackColor = true;
            this.state_button.Visible = false;
            this.state_button.Click += new System.EventHandler(this.state_button_Click);
            // 
            // save_data_button
            // 
            this.save_data_button.Location = new System.Drawing.Point(713, 129);
            this.save_data_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.save_data_button.Name = "save_data_button";
            this.save_data_button.Size = new System.Drawing.Size(80, 28);
            this.save_data_button.TabIndex = 30;
            this.save_data_button.Text = "Save Data";
            this.save_data_button.UseVisualStyleBackColor = true;
            this.save_data_button.Visible = false;
            this.save_data_button.Click += new System.EventHandler(this.save_data_button_Click);
            // 
            // write_motor_button
            // 
            this.write_motor_button.Location = new System.Drawing.Point(713, 96);
            this.write_motor_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.write_motor_button.Name = "write_motor_button";
            this.write_motor_button.Size = new System.Drawing.Size(80, 28);
            this.write_motor_button.TabIndex = 31;
            this.write_motor_button.Text = "Write Motor";
            this.write_motor_button.UseVisualStyleBackColor = true;
            this.write_motor_button.Visible = false;
            this.write_motor_button.Click += new System.EventHandler(this.write_motor_button_Click);
            // 
            // read_volt_button
            // 
            this.read_volt_button.Location = new System.Drawing.Point(713, 356);
            this.read_volt_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.read_volt_button.Name = "read_volt_button";
            this.read_volt_button.Size = new System.Drawing.Size(80, 28);
            this.read_volt_button.TabIndex = 32;
            this.read_volt_button.Text = "Read Voltage";
            this.read_volt_button.UseVisualStyleBackColor = true;
            this.read_volt_button.Visible = false;
            this.read_volt_button.Click += new System.EventHandler(this.read_volt_button_Click);
            // 
            // dataGridView_DataCollect
            // 
            this.dataGridView_DataCollect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_DataCollect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1_Num,
            this.Column2_CheckBtn,
            this.Column3_SendBtn,
            this.Column4_Data,
            this.Column5_CRC});
            this.dataGridView_DataCollect.Location = new System.Drawing.Point(13, 26);
            this.dataGridView_DataCollect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView_DataCollect.Name = "dataGridView_DataCollect";
            this.dataGridView_DataCollect.RowHeadersWidth = 51;
            this.dataGridView_DataCollect.RowTemplate.Height = 24;
            this.dataGridView_DataCollect.Size = new System.Drawing.Size(655, 188);
            this.dataGridView_DataCollect.TabIndex = 33;
            this.dataGridView_DataCollect.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_DataCollect_CellContentClick);
            // 
            // Column1_Num
            // 
            this.Column1_Num.HeaderText = "編號";
            this.Column1_Num.MinimumWidth = 6;
            this.Column1_Num.Name = "Column1_Num";
            this.Column1_Num.Width = 70;
            // 
            // Column2_CheckBtn
            // 
            this.Column2_CheckBtn.HeaderText = "確認";
            this.Column2_CheckBtn.MinimumWidth = 6;
            this.Column2_CheckBtn.Name = "Column2_CheckBtn";
            this.Column2_CheckBtn.Width = 50;
            // 
            // Column3_SendBtn
            // 
            this.Column3_SendBtn.HeaderText = "發送";
            this.Column3_SendBtn.MinimumWidth = 6;
            this.Column3_SendBtn.Name = "Column3_SendBtn";
            this.Column3_SendBtn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3_SendBtn.Width = 60;
            // 
            // Column4_Data
            // 
            this.Column4_Data.HeaderText = "資料";
            this.Column4_Data.MinimumWidth = 6;
            this.Column4_Data.Name = "Column4_Data";
            this.Column4_Data.Width = 200;
            // 
            // Column5_CRC
            // 
            this.Column5_CRC.HeaderText = "CRC";
            this.Column5_CRC.MinimumWidth = 6;
            this.Column5_CRC.Name = "Column5_CRC";
            this.Column5_CRC.Width = 40;
            // 
            // textBox_CmdDataInput
            // 
            this.textBox_CmdDataInput.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_CmdDataInput.Location = new System.Drawing.Point(13, 249);
            this.textBox_CmdDataInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_CmdDataInput.Name = "textBox_CmdDataInput";
            this.textBox_CmdDataInput.Size = new System.Drawing.Size(300, 36);
            this.textBox_CmdDataInput.TabIndex = 34;
            // 
            // button_SendData
            // 
            this.button_SendData.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_SendData.Location = new System.Drawing.Point(343, 249);
            this.button_SendData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_SendData.Name = "button_SendData";
            this.button_SendData.Size = new System.Drawing.Size(100, 38);
            this.button_SendData.TabIndex = 35;
            this.button_SendData.Text = "Send";
            this.button_SendData.UseVisualStyleBackColor = true;
            this.button_SendData.Click += new System.EventHandler(this.button_SendData_Click);
            // 
            // button_Upload
            // 
            this.button_Upload.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Upload.Location = new System.Drawing.Point(473, 249);
            this.button_Upload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Upload.Name = "button_Upload";
            this.button_Upload.Size = new System.Drawing.Size(100, 38);
            this.button_Upload.TabIndex = 36;
            this.button_Upload.Text = "Upload";
            this.button_Upload.UseVisualStyleBackColor = true;
            this.button_Upload.Click += new System.EventHandler(this.button_Upload_Click);
            // 
            // button_CheckCRC
            // 
            this.button_CheckCRC.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_CheckCRC.Location = new System.Drawing.Point(177, 321);
            this.button_CheckCRC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_CheckCRC.Name = "button_CheckCRC";
            this.button_CheckCRC.Size = new System.Drawing.Size(137, 38);
            this.button_CheckCRC.TabIndex = 37;
            this.button_CheckCRC.Text = "CheckCRC";
            this.button_CheckCRC.UseVisualStyleBackColor = true;
            this.button_CheckCRC.Click += new System.EventHandler(this.button_CheckCRC_Click);
            // 
            // textBox_CheckCRC
            // 
            this.textBox_CheckCRC.Enabled = false;
            this.textBox_CheckCRC.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_CheckCRC.Location = new System.Drawing.Point(13, 321);
            this.textBox_CheckCRC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_CheckCRC.Name = "textBox_CheckCRC";
            this.textBox_CheckCRC.Size = new System.Drawing.Size(111, 36);
            this.textBox_CheckCRC.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 230);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 39;
            this.label1.Text = "HEX 輸入";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 302);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 40;
            this.label2.Text = "CRC";
            // 
            // groupBox_CommandTest
            // 
            this.groupBox_CommandTest.Controls.Add(this.dataGridView_DataCollect);
            this.groupBox_CommandTest.Controls.Add(this.label2);
            this.groupBox_CommandTest.Controls.Add(this.textBox_CmdDataInput);
            this.groupBox_CommandTest.Controls.Add(this.label1);
            this.groupBox_CommandTest.Controls.Add(this.button_SendData);
            this.groupBox_CommandTest.Controls.Add(this.textBox_CheckCRC);
            this.groupBox_CommandTest.Controls.Add(this.button_Upload);
            this.groupBox_CommandTest.Controls.Add(this.button_CheckCRC);
            this.groupBox_CommandTest.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_CommandTest.Location = new System.Drawing.Point(19, 259);
            this.groupBox_CommandTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox_CommandTest.Name = "groupBox_CommandTest";
            this.groupBox_CommandTest.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox_CommandTest.Size = new System.Drawing.Size(680, 370);
            this.groupBox_CommandTest.TabIndex = 41;
            this.groupBox_CommandTest.TabStop = false;
            this.groupBox_CommandTest.Text = "測試通訊";
            // 
            // textBox_DecInput
            // 
            this.textBox_DecInput.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_DecInput.Location = new System.Drawing.Point(32, 82);
            this.textBox_DecInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_DecInput.Name = "textBox_DecInput";
            this.textBox_DecInput.Size = new System.Drawing.Size(156, 36);
            this.textBox_DecInput.TabIndex = 42;
            // 
            // textBox_HexOutput
            // 
            this.textBox_HexOutput.Enabled = false;
            this.textBox_HexOutput.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_HexOutput.Location = new System.Drawing.Point(297, 82);
            this.textBox_HexOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_HexOutput.Name = "textBox_HexOutput";
            this.textBox_HexOutput.Size = new System.Drawing.Size(156, 36);
            this.textBox_HexOutput.TabIndex = 43;
            // 
            // button_DecToHex
            // 
            this.button_DecToHex.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_DecToHex.Location = new System.Drawing.Point(213, 82);
            this.button_DecToHex.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_DecToHex.Name = "button_DecToHex";
            this.button_DecToHex.Size = new System.Drawing.Size(61, 38);
            this.button_DecToHex.TabIndex = 44;
            this.button_DecToHex.Text = "-->";
            this.button_DecToHex.UseVisualStyleBackColor = true;
            this.button_DecToHex.Click += new System.EventHandler(this.button_DecToHex_Click);
            // 
            // comboBox_BinaryType
            // 
            this.comboBox_BinaryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BinaryType.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_BinaryType.FormattingEnabled = true;
            this.comboBox_BinaryType.Items.AddRange(new object[] {
            "number",
            "8-bits",
            "16-bits",
            "24-bits",
            "32-bits"});
            this.comboBox_BinaryType.Location = new System.Drawing.Point(188, 28);
            this.comboBox_BinaryType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_BinaryType.Name = "comboBox_BinaryType";
            this.comboBox_BinaryType.Size = new System.Drawing.Size(112, 28);
            this.comboBox_BinaryType.TabIndex = 45;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboBox_BinaryType);
            this.groupBox2.Controls.Add(this.textBox_DecInput);
            this.groupBox2.Controls.Add(this.button_DecToHex);
            this.groupBox2.Controls.Add(this.textBox_HexOutput);
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(19, 96);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(499, 135);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "位元轉換";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(92, 35);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 49;
            this.label5.Text = "Binary Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(293, 64);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 48;
            this.label4.Text = "Hex";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 47;
            this.label3.Text = "Dec";
            // 
            // SW_1_ON
            // 
            this.SW_1_ON.Location = new System.Drawing.Point(816, 30);
            this.SW_1_ON.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SW_1_ON.Name = "SW_1_ON";
            this.SW_1_ON.Size = new System.Drawing.Size(87, 45);
            this.SW_1_ON.TabIndex = 47;
            this.SW_1_ON.Text = "SW_1_ON";
            this.SW_1_ON.UseVisualStyleBackColor = true;
            this.SW_1_ON.Click += new System.EventHandler(this.SW_1_ON_Click);
            // 
            // SW_1_OFF
            // 
            this.SW_1_OFF.Location = new System.Drawing.Point(928, 30);
            this.SW_1_OFF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SW_1_OFF.Name = "SW_1_OFF";
            this.SW_1_OFF.Size = new System.Drawing.Size(101, 45);
            this.SW_1_OFF.TabIndex = 48;
            this.SW_1_OFF.Text = "SW_1_OFF";
            this.SW_1_OFF.UseVisualStyleBackColor = true;
            this.SW_1_OFF.Click += new System.EventHandler(this.SW_1_OFF_Click);
            // 
            // SW_2_OFF
            // 
            this.SW_2_OFF.Location = new System.Drawing.Point(928, 103);
            this.SW_2_OFF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SW_2_OFF.Name = "SW_2_OFF";
            this.SW_2_OFF.Size = new System.Drawing.Size(101, 45);
            this.SW_2_OFF.TabIndex = 50;
            this.SW_2_OFF.Text = "SW_2_OFF";
            this.SW_2_OFF.UseVisualStyleBackColor = true;
            this.SW_2_OFF.Click += new System.EventHandler(this.SW_2_OFF_Click);
            // 
            // SW_2_ON
            // 
            this.SW_2_ON.Location = new System.Drawing.Point(816, 103);
            this.SW_2_ON.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SW_2_ON.Name = "SW_2_ON";
            this.SW_2_ON.Size = new System.Drawing.Size(87, 45);
            this.SW_2_ON.TabIndex = 49;
            this.SW_2_ON.Text = "SW_2_ON";
            this.SW_2_ON.UseVisualStyleBackColor = true;
            this.SW_2_ON.Click += new System.EventHandler(this.SW_2_ON_Click);
            // 
            // SW_4_OFF
            // 
            this.SW_4_OFF.Location = new System.Drawing.Point(928, 246);
            this.SW_4_OFF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SW_4_OFF.Name = "SW_4_OFF";
            this.SW_4_OFF.Size = new System.Drawing.Size(101, 45);
            this.SW_4_OFF.TabIndex = 54;
            this.SW_4_OFF.Text = "SW_4_OFF";
            this.SW_4_OFF.UseVisualStyleBackColor = true;
            this.SW_4_OFF.Click += new System.EventHandler(this.SW_4_OFF_Click);
            // 
            // SW_4_ON
            // 
            this.SW_4_ON.Location = new System.Drawing.Point(816, 246);
            this.SW_4_ON.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SW_4_ON.Name = "SW_4_ON";
            this.SW_4_ON.Size = new System.Drawing.Size(87, 45);
            this.SW_4_ON.TabIndex = 53;
            this.SW_4_ON.Text = "SW_4_ON";
            this.SW_4_ON.UseVisualStyleBackColor = true;
            this.SW_4_ON.Click += new System.EventHandler(this.SW_4_ON_Click);
            // 
            // SW_3_OFF
            // 
            this.SW_3_OFF.Location = new System.Drawing.Point(928, 173);
            this.SW_3_OFF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SW_3_OFF.Name = "SW_3_OFF";
            this.SW_3_OFF.Size = new System.Drawing.Size(101, 45);
            this.SW_3_OFF.TabIndex = 52;
            this.SW_3_OFF.Text = "SW_3_OFF";
            this.SW_3_OFF.UseVisualStyleBackColor = true;
            this.SW_3_OFF.Click += new System.EventHandler(this.SW_3_OFF_Click);
            // 
            // SW_3_ON
            // 
            this.SW_3_ON.Location = new System.Drawing.Point(816, 173);
            this.SW_3_ON.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SW_3_ON.Name = "SW_3_ON";
            this.SW_3_ON.Size = new System.Drawing.Size(87, 45);
            this.SW_3_ON.TabIndex = 51;
            this.SW_3_ON.Text = "SW_3_ON";
            this.SW_3_ON.UseVisualStyleBackColor = true;
            this.SW_3_ON.Click += new System.EventHandler(this.SW_3_ON_Click);
            // 
            // SBE_OFF
            // 
            this.SBE_OFF.Location = new System.Drawing.Point(928, 397);
            this.SBE_OFF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SBE_OFF.Name = "SBE_OFF";
            this.SBE_OFF.Size = new System.Drawing.Size(101, 45);
            this.SBE_OFF.TabIndex = 58;
            this.SBE_OFF.Text = "SBE_OFF";
            this.SBE_OFF.UseVisualStyleBackColor = true;
            this.SBE_OFF.Click += new System.EventHandler(this.SBE_OFF_Click);
            // 
            // SBE_ON
            // 
            this.SBE_ON.Location = new System.Drawing.Point(816, 397);
            this.SBE_ON.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SBE_ON.Name = "SBE_ON";
            this.SBE_ON.Size = new System.Drawing.Size(87, 45);
            this.SBE_ON.TabIndex = 57;
            this.SBE_ON.Text = "SBE_ON";
            this.SBE_ON.UseVisualStyleBackColor = true;
            this.SBE_ON.Click += new System.EventHandler(this.SBE_ON_Click);
            // 
            // SW_5_OFF
            // 
            this.SW_5_OFF.Location = new System.Drawing.Point(928, 324);
            this.SW_5_OFF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SW_5_OFF.Name = "SW_5_OFF";
            this.SW_5_OFF.Size = new System.Drawing.Size(101, 45);
            this.SW_5_OFF.TabIndex = 56;
            this.SW_5_OFF.Text = "SW_5_OFF";
            this.SW_5_OFF.UseVisualStyleBackColor = true;
            this.SW_5_OFF.Click += new System.EventHandler(this.SW_5_OFF_Click);
            // 
            // SW_5_ON
            // 
            this.SW_5_ON.Location = new System.Drawing.Point(816, 324);
            this.SW_5_ON.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SW_5_ON.Name = "SW_5_ON";
            this.SW_5_ON.Size = new System.Drawing.Size(87, 45);
            this.SW_5_ON.TabIndex = 55;
            this.SW_5_ON.Text = "SW_5_ON";
            this.SW_5_ON.UseVisualStyleBackColor = true;
            this.SW_5_ON.Click += new System.EventHandler(this.SW_5_ON_Click);
            // 
            // SB_Status
            // 
            this.SB_Status.Location = new System.Drawing.Point(816, 475);
            this.SB_Status.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SB_Status.Name = "SB_Status";
            this.SB_Status.Size = new System.Drawing.Size(87, 45);
            this.SB_Status.TabIndex = 59;
            this.SB_Status.Text = "SB_Status";
            this.SB_Status.UseVisualStyleBackColor = true;
            this.SB_Status.Click += new System.EventHandler(this.SB_Status_Click);
            // 
            // Reset_CNT
            // 
            this.Reset_CNT.Location = new System.Drawing.Point(928, 475);
            this.Reset_CNT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Reset_CNT.Name = "Reset_CNT";
            this.Reset_CNT.Size = new System.Drawing.Size(101, 45);
            this.Reset_CNT.TabIndex = 60;
            this.Reset_CNT.Text = "Reset_CNT";
            this.Reset_CNT.UseVisualStyleBackColor = true;
            this.Reset_CNT.Click += new System.EventHandler(this.Reset_CNT_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1406, 638);
            this.Controls.Add(this.Reset_CNT);
            this.Controls.Add(this.SB_Status);
            this.Controls.Add(this.SBE_OFF);
            this.Controls.Add(this.SBE_ON);
            this.Controls.Add(this.SW_5_OFF);
            this.Controls.Add(this.SW_5_ON);
            this.Controls.Add(this.SW_4_OFF);
            this.Controls.Add(this.SW_4_ON);
            this.Controls.Add(this.SW_3_OFF);
            this.Controls.Add(this.SW_3_ON);
            this.Controls.Add(this.SW_2_OFF);
            this.Controls.Add(this.SW_2_ON);
            this.Controls.Add(this.SW_1_OFF);
            this.Controls.Add(this.SW_1_ON);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox_CommandTest);
            this.Controls.Add(this.read_volt_button);
            this.Controls.Add(this.write_motor_button);
            this.Controls.Add(this.save_data_button);
            this.Controls.Add(this.state_button);
            this.Controls.Add(this.restart_button);
            this.Controls.Add(this.IO_status_button);
            this.Controls.Add(this.motor_move_button);
            this.Controls.Add(this.motor_emerg_button);
            this.Controls.Add(this.set_volt_button);
            this.Controls.Add(this.Read_motor_button);
            this.Controls.Add(this.SetID_button);
            this.Controls.Add(this.debug_textBox);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.Load += new System.EventHandler(this.main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DataCollect)).EndInit();
            this.groupBox_CommandTest.ResumeLayout(false);
            this.groupBox_CommandTest.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button Refresh_button;
        private System.Windows.Forms.ComboBox COM_comboBox;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.TextBox debug_textBox;
        private System.Windows.Forms.Button SetID_button;
        private System.Windows.Forms.Button Read_motor_button;
        private System.Windows.Forms.Button set_volt_button;
        private System.Windows.Forms.Button motor_emerg_button;
        private System.Windows.Forms.Button motor_move_button;
        private System.Windows.Forms.Button IO_status_button;
        private System.Windows.Forms.Button restart_button;
        private System.Windows.Forms.Button state_button;
        private System.Windows.Forms.Button save_data_button;
        private System.Windows.Forms.Button write_motor_button;
        private System.Windows.Forms.Button read_volt_button;
        private System.Windows.Forms.DataGridView dataGridView_DataCollect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1_Num;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2_CheckBtn;
        private System.Windows.Forms.DataGridViewButtonColumn Column3_SendBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4_Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5_CRC;
        private System.Windows.Forms.TextBox textBox_CmdDataInput;
        private System.Windows.Forms.Button button_SendData;
        private System.Windows.Forms.Button button_Upload;
        private System.Windows.Forms.Button button_CheckCRC;
        private System.Windows.Forms.TextBox textBox_CheckCRC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox_CommandTest;
        private System.Windows.Forms.TextBox textBox_DecInput;
        private System.Windows.Forms.TextBox textBox_HexOutput;
        private System.Windows.Forms.Button button_DecToHex;
        private System.Windows.Forms.ComboBox comboBox_BinaryType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SW_1_ON;
        private System.Windows.Forms.Button SW_1_OFF;
        private System.Windows.Forms.Button SW_2_OFF;
        private System.Windows.Forms.Button SW_2_ON;
        private System.Windows.Forms.Button SW_4_OFF;
        private System.Windows.Forms.Button SW_4_ON;
        private System.Windows.Forms.Button SW_3_OFF;
        private System.Windows.Forms.Button SW_3_ON;
        private System.Windows.Forms.Button SBE_OFF;
        private System.Windows.Forms.Button SBE_ON;
        private System.Windows.Forms.Button SW_5_OFF;
        private System.Windows.Forms.Button SW_5_ON;
        private System.Windows.Forms.Button SB_Status;
        private System.Windows.Forms.Button Reset_CNT;
    }
}

