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
    public partial class IotaSettings : Form
    {        
        public IotaSettings()
        {
            InitializeComponent();
            this.securityLevel.SelectedIndex = 1;
            this.comboBoxStorage.SelectedIndex = 0;
        }

        public IotaSetting IotaSetting { get; private set; }
        public string[] Groups { get; internal set; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (this.Groups?.Any() == true)
            {
                this.comboBoxGroups.Items.AddRange(this.Groups);
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            this.IotaSetting = new IotaSetting()
            {
                EntryTitle = this.textBoxEntryName.Text,
                NoOfAddress = (int)this.numberOfAddresses.Value,
                SecurityLevel = securityLevel.SelectedIndex + 1,
                FolderName = this.comboBoxGroups.Text,
                Storagelocation = (Storagelocation)this.comboBoxStorage.SelectedIndex,
            };
        }
    }
}
