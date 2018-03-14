using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePassLib;
using KeePassLib.Cryptography;

namespace IOTAAddressCreation
{
    public sealed class IOTAAddressCreationExt : Plugin
    {
        private IPluginHost m_host = null;

        public override bool Initialize(IPluginHost host)
        {
            m_host = host;

            // Get a reference to the 'Tools' menu item container
            ToolStripItemCollection tsMenu = m_host.MainWindow.ToolsMenu.DropDownItems;
            
            // Add a separator at the bottom
            var separator = new ToolStripSeparator();
            tsMenu.Add(separator);
            
            var menuItem = new ToolStripMenuItem();
            menuItem.Text = "Iota offline address creation";
            menuItem.Click += this.IotaWork;
            menuItem.Image = Properties.Resources.iota_favicon.ToBitmap();
            tsMenu.Add(menuItem);

            return true;
        }

        private void IotaWork(object sender, EventArgs e)
        {
            if (this.m_host.Database.IsOpen)
            {
                using (var dialog = new IotaSettings())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var response = CreateAddress(dialog.IotaSetting);
                        MessageBox.Show(response);
                    }
                }
            }
            else
            {
                MessageBox.Show("Database not open!");
            }
        }

        private string CreateAddress(IotaSetting settings)
        {
            var group = this.m_host.Database.RootGroup.FindCreateGroup(settings.FolderName, true);

            var entry = new KeePassLib.PwEntry(true, true);                        
            entry.Strings.Set(PwDefs.TitleField, new KeePassLib.Security.ProtectedString(false, settings.EntryTitle));
            KeePassLib.Security.ProtectedString protectedStringSeed = CreateSeed();
            entry.Strings.Set(PwDefs.PasswordField, protectedStringSeed);
            group.AddEntry(entry, true);

            if (protectedStringSeed.IsEmpty) return "Seed value is empty!";
            var seed = protectedStringSeed.ReadString();

            for (int i = 0; i < settings.NoOfAddress; i++)
            {
                var address = this.CreateAddress(protectedStringSeed.ReadString(), i, settings.SecurityLevel);
                entry.Strings.Set("Address " + i.ToString().PadLeft(3,'0'), new KeePassLib.Security.ProtectedString(false, address));
            }

            this.m_host.Database.Modified = true;
            return "Addresses created";
        }

        private KeePassLib.Security.ProtectedString CreateSeed()
        {
            KeePassLib.Security.ProtectedString outPutString;
            KeePassLib.Cryptography.PasswordGenerator.PwGenerator.Generate(
                out outPutString,
                this.GetIotaProfile(),
                CryptoRandom.Instance.GetRandomBytes(256),
                new KeePassLib.Cryptography.PasswordGenerator.CustomPwGeneratorPool()
                );

            return outPutString;
        }

        private KeePassLib.Cryptography.PasswordGenerator.PwProfile GetIotaProfile()
        {
            var charSet = new KeePassLib.Cryptography.PasswordGenerator.PwCharSet("ABCDEFGHIJKLMNOPQRST9");
            var profile = new KeePassLib.Cryptography.PasswordGenerator.PwProfile() { CharSet = charSet };
            profile.Length = 81;
            return profile;
        }

        private string CreateAddress(string seed, int index, int securityLevel)
        {
            return BeeFrog.Iota.Api.Utils.IotaUtils.GenerateAddress(seed, index, securityLevel).Address;
        }
    }
}
