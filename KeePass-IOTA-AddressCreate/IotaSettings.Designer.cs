namespace IOTAAddressCreation
{
    partial class IotaSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IotaSettings));
            this.buttonCreate = new System.Windows.Forms.Button();
            this.securityLevel = new System.Windows.Forms.ComboBox();
            this.numberOfAddresses = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxEntryName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxGroups = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfAddresses)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreate
            // 
            this.buttonCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonCreate.Location = new System.Drawing.Point(279, 187);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.TabIndex = 0;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.Create_Click);
            // 
            // securityLevel
            // 
            this.securityLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.securityLevel.FormattingEnabled = true;
            this.securityLevel.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High"});
            this.securityLevel.Location = new System.Drawing.Point(191, 97);
            this.securityLevel.Name = "securityLevel";
            this.securityLevel.Size = new System.Drawing.Size(120, 21);
            this.securityLevel.TabIndex = 2;
            // 
            // numberOfAddresses
            // 
            this.numberOfAddresses.Location = new System.Drawing.Point(191, 134);
            this.numberOfAddresses.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numberOfAddresses.Name = "numberOfAddresses";
            this.numberOfAddresses.Size = new System.Drawing.Size(120, 20);
            this.numberOfAddresses.TabIndex = 3;
            this.numberOfAddresses.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Security Level:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Number of addresses to create:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Entry Name:";
            // 
            // textBoxEntryName
            // 
            this.textBoxEntryName.Location = new System.Drawing.Point(191, 32);
            this.textBoxEntryName.Name = "textBoxEntryName";
            this.textBoxEntryName.Size = new System.Drawing.Size(163, 20);
            this.textBoxEntryName.TabIndex = 7;
            this.textBoxEntryName.Text = "Iota Seed";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Folder:";
            // 
            // comboBoxGroups
            // 
            this.comboBoxGroups.FormattingEnabled = true;
            this.comboBoxGroups.Location = new System.Drawing.Point(191, 62);
            this.comboBoxGroups.Name = "comboBoxGroups";
            this.comboBoxGroups.Size = new System.Drawing.Size(163, 21);
            this.comboBoxGroups.TabIndex = 10;
            this.comboBoxGroups.Text = "Iota Seeds";
            // 
            // IotaSettings
            // 
            this.AcceptButton = this.buttonCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 222);
            this.Controls.Add(this.comboBoxGroups);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxEntryName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numberOfAddresses);
            this.Controls.Add(this.securityLevel);
            this.Controls.Add(this.buttonCreate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IotaSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Iota Offline Seed Creation";
            ((System.ComponentModel.ISupportInitialize)(this.numberOfAddresses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.ComboBox securityLevel;
        private System.Windows.Forms.NumericUpDown numberOfAddresses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxEntryName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxGroups;
    }
}