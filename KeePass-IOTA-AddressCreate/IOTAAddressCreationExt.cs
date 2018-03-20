using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
        private ToolStripItemCollection tsMenu;
        private ToolStripMenuItem menuItem;
        private byte[] iconUuId = Guid.Parse("C29F5DC9-60C5-4956-AC73-B2281BBC52A7").ToByteArray();

        public override bool Initialize(IPluginHost host)
        {
            m_host = host;

            // Get a reference to the 'Tools' menu item container
            tsMenu = m_host.MainWindow.ToolsMenu.DropDownItems;
            
            // Add a separator at the bottom
            var separator = new ToolStripSeparator();
            tsMenu.Add(separator);
            
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Iota offline address creation";
            menuItem.Click += this.IotaWork;
            menuItem.Image = Properties.Resources.iota_favicon.ToBitmap();
            tsMenu.Add(menuItem);

            return true;
        }

        public override void Terminate()
        {
            base.Terminate();
            this.tsMenu.Remove(menuItem);
        }

        private event EventHandler ProgressChanged;

        private void IotaWork(object sender, EventArgs e)
        {
            if (this.m_host.Database.IsOpen)
            {
                using (var dialog = new IotaSettings())
                {
                    dialog.Groups = this.GetFolders();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (CreationProgress progressform = new CreationProgress())
                        {                            
                            progressform.SetUpProgressBar(dialog.IotaSetting.NoOfAddress);
                            this.ProgressChanged += progressform.OnProgressIncremented;
                            progressform.Show();
                            Application.DoEvents();

                            var response = CreateAddress(dialog.IotaSetting);

                            this.ProgressChanged -= progressform.OnProgressIncremented;
                            progressform.Close();

                            MessageBox.Show(response);
                        }                        
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
            
            var addresses = new List<string>();
            
            for (int i = 0; i < settings.NoOfAddress; i++)
            {
                var address = this.CreateAddress(protectedStringSeed.ReadString(), i, settings.SecurityLevel);
                addresses.Add(address);

                if (this.ProgressChanged != null)
                {
                    this.ProgressChanged(this, new EventArgs());                    
                }
            }

            if(settings.Storagelocation == Storagelocation.Notes)
            {
                var notesString = String.Join("\n", addresses.Select((address, i) => $"Address {i.ToString().PadLeft(3, '0')} {address}"));
                var protectedString = new KeePassLib.Security.ProtectedString(false, notesString);
                entry.Strings.Set(PwDefs.NotesField, protectedString);
            }
            else
            {
                for(int i = 0; i < addresses.Count; i++)
                {
                    var addressName = "Address " + i.ToString().PadLeft(3, '0');
                    var protectedString = new KeePassLib.Security.ProtectedString(false, addresses[i]);
                    entry.Strings.Set(addressName, protectedString);
                }
            }

            try
            {
                CheckAndAddIotaIcon();
                entry.CustomIconUuid = new PwUuid(this.iconUuId);
            }
            catch { }

            this.m_host.Database.Modified = true;
            // Force the groups to refresh
            m_host.MainWindow.UpdateUI(false, null, true, m_host.Database.RootGroup, true, null, true);

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

        private void CheckAndAddIotaIcon()
        {

            if (!this.m_host.Database.CustomIcons.Any(i => i.Uuid.UuidBytes == this.iconUuId))
            {
                var icon = new PwCustomIcon(new PwUuid(this.iconUuId), GetIotaIconAsPng());
                this.m_host.Database.CustomIcons.Add(icon);
            }
        }

        private byte[] GetIotaIconAsPng()
        {
            var iconBitmap = Properties.Resources.iota_favicon.ToBitmap();
            byte[] iconPng = null;
            using (MemoryStream stream = new MemoryStream())
            {
                iconBitmap.Save(stream, ImageFormat.Png);
                iconPng = stream.ToArray();
            }

            return iconPng;
        }

        private string CreateAddress(string seed, int index, int securityLevel)
        {
            return BeeFrog.Iota.Api.Utils.IotaUtils.GenerateAddress(seed, index, securityLevel).Address;
        }

        private string[] GetFolders()
        {
            var groups = new List<string>();
            var group = this.m_host.Database.RootGroup;
            groups.Add(group.Name);

            groups.AddRange(group.GetGroups(true).Select(g => g.Name));

            return groups.ToArray();
        }
    }
}
