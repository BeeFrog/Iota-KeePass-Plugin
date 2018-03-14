using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//TODO: Make Folder a DropDown combo.
namespace IOTAAddressCreation
{
    public partial class IotaSettings : Form
    {        
        public IotaSettings()
        {
            InitializeComponent();
            this.securityLevel.SelectedIndex = 1;
        }

        public int MyProperty { get; set; }
        public IotaSetting IotaSetting { get; private set; }

        private void Create_Click(object sender, EventArgs e)
        {
            this.IotaSetting = new IotaSetting()
            {
                EntryTitle = this.textBoxEntryName.Text,
                NoOfAddress = (int)this.numberOfAddresses.Value,
                SecurityLevel = securityLevel.SelectedIndex + 1,
                FolderName = this.textBoxFolderName.Text
            };

            //TODO: do this properly!
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
