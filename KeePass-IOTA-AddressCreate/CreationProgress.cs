using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOTAAddressCreation
{
    public partial class CreationProgress : Form
    {
        public CreationProgress()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void SetUpProgressBar(int addressCount)
        {
            this.progressBar.Maximum = addressCount;
        }

        public void OnProgressIncremented(object sender, EventArgs e)
        {
            if (this.progressBar.Maximum > this.progressBar.Value)
            {
                this.progressBar.Value = this.progressBar.Value + 1;
                Application.DoEvents();
            }
        }
    }
}
