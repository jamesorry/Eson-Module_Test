using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eson_Pega
{
    public partial class ChooseMode : Form
    {
        public ChooseMode()
        {
            InitializeComponent();
        }

        private void ChooseMode_Load(object sender, EventArgs e)
        {

        }

        private void LightBox_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void Slide_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
    }
}
