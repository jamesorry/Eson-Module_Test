
namespace Eson_Pega
{
    partial class ChooseMode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Slide_button = new System.Windows.Forms.Button();
            this.LightBox_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Slide_button
            // 
            this.Slide_button.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Slide_button.Location = new System.Drawing.Point(36, 35);
            this.Slide_button.Margin = new System.Windows.Forms.Padding(4);
            this.Slide_button.Name = "Slide_button";
            this.Slide_button.Size = new System.Drawing.Size(240, 175);
            this.Slide_button.TabIndex = 0;
            this.Slide_button.Text = "電動滑軌";
            this.Slide_button.UseVisualStyleBackColor = true;
            this.Slide_button.Click += new System.EventHandler(this.Slide_button_Click);
            // 
            // LightBox_button
            // 
            this.LightBox_button.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LightBox_button.Location = new System.Drawing.Point(302, 35);
            this.LightBox_button.Margin = new System.Windows.Forms.Padding(4);
            this.LightBox_button.Name = "LightBox_button";
            this.LightBox_button.Size = new System.Drawing.Size(240, 175);
            this.LightBox_button.TabIndex = 1;
            this.LightBox_button.Text = "燈箱";
            this.LightBox_button.UseVisualStyleBackColor = true;
            this.LightBox_button.Click += new System.EventHandler(this.LightBox_button_Click);
            // 
            // ChooseMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 240);
            this.Controls.Add(this.LightBox_button);
            this.Controls.Add(this.Slide_button);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChooseMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模式選擇";
            this.Load += new System.EventHandler(this.ChooseMode_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Slide_button;
        private System.Windows.Forms.Button LightBox_button;
    }
}