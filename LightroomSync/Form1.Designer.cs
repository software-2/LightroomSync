namespace LightroomSync
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            localFolderTextBox = new TextBox();
            label2 = new Label();
            networkFolderTextBox = new TextBox();
            label3 = new Label();
            eventsTextBox = new TextBox();
            buttonSelectLocalFolder = new Button();
            buttonSelectNetworkFolder = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            launchAtStartupToolStripMenuItem = new ToolStripMenuItem();
            autoCheckForUpdatesToolStripMenuItem = new ToolStripMenuItem();
            minimizeToTrayToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            submitABugToolStripMenuItem = new ToolStripMenuItem();
            gitHubPageToolStripMenuItem = new ToolStripMenuItem();
            checkForUpdatesToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 52);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 0;
            label1.Text = "Local Folder";
            label1.Click += label1_Click;
            // 
            // localFolderTextBox
            // 
            localFolderTextBox.Location = new Point(14, 76);
            localFolderTextBox.Margin = new Padding(3, 4, 3, 4);
            localFolderTextBox.Name = "localFolderTextBox";
            localFolderTextBox.Size = new Size(865, 27);
            localFolderTextBox.TabIndex = 1;
            localFolderTextBox.TextChanged += localFolderTextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 132);
            label2.Name = "label2";
            label2.Size = new Size(111, 20);
            label2.TabIndex = 2;
            label2.Text = "Network Folder";
            // 
            // networkFolderTextBox
            // 
            networkFolderTextBox.Location = new Point(14, 156);
            networkFolderTextBox.Margin = new Padding(3, 4, 3, 4);
            networkFolderTextBox.Name = "networkFolderTextBox";
            networkFolderTextBox.Size = new Size(865, 27);
            networkFolderTextBox.TabIndex = 3;
            networkFolderTextBox.Text = "P:\\Lightroom";
            networkFolderTextBox.TextChanged += networkFolderTextBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 219);
            label3.Name = "label3";
            label3.Size = new Size(51, 20);
            label3.TabIndex = 4;
            label3.Text = "Events";
            // 
            // eventsTextBox
            // 
            eventsTextBox.Location = new Point(14, 243);
            eventsTextBox.Margin = new Padding(3, 4, 3, 4);
            eventsTextBox.Multiline = true;
            eventsTextBox.Name = "eventsTextBox";
            eventsTextBox.ScrollBars = ScrollBars.Vertical;
            eventsTextBox.Size = new Size(886, 167);
            eventsTextBox.TabIndex = 5;
            // 
            // buttonSelectLocalFolder
            // 
            buttonSelectLocalFolder.Location = new Point(886, 76);
            buttonSelectLocalFolder.Margin = new Padding(3, 4, 3, 4);
            buttonSelectLocalFolder.Name = "buttonSelectLocalFolder";
            buttonSelectLocalFolder.Size = new Size(24, 31);
            buttonSelectLocalFolder.TabIndex = 8;
            buttonSelectLocalFolder.UseVisualStyleBackColor = true;
            buttonSelectLocalFolder.Click += buttonSelectLocalFolder_Click;
            // 
            // buttonSelectNetworkFolder
            // 
            buttonSelectNetworkFolder.Location = new Point(886, 156);
            buttonSelectNetworkFolder.Margin = new Padding(3, 4, 3, 4);
            buttonSelectNetworkFolder.Name = "buttonSelectNetworkFolder";
            buttonSelectNetworkFolder.Size = new Size(24, 31);
            buttonSelectNetworkFolder.TabIndex = 9;
            buttonSelectNetworkFolder.UseVisualStyleBackColor = true;
            buttonSelectNetworkFolder.Click += buttonSelectNetworkFolder_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 5000;
            timer1.Tick += timer1_Tick;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(914, 30);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { launchAtStartupToolStripMenuItem, autoCheckForUpdatesToolStripMenuItem, minimizeToTrayToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // launchAtStartupToolStripMenuItem
            // 
            launchAtStartupToolStripMenuItem.Name = "launchAtStartupToolStripMenuItem";
            launchAtStartupToolStripMenuItem.Size = new Size(251, 26);
            launchAtStartupToolStripMenuItem.Text = "Launch At Startup";
            launchAtStartupToolStripMenuItem.Click += launchAtStartupToolStripMenuItem_Click;
            // 
            // autoCheckForUpdatesToolStripMenuItem
            // 
            autoCheckForUpdatesToolStripMenuItem.Name = "autoCheckForUpdatesToolStripMenuItem";
            autoCheckForUpdatesToolStripMenuItem.Size = new Size(251, 26);
            autoCheckForUpdatesToolStripMenuItem.Text = "Auto Check For Updates";
            autoCheckForUpdatesToolStripMenuItem.Click += autoCheckForUpdatesToolStripMenuItem_Click;
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            minimizeToTrayToolStripMenuItem.Size = new Size(251, 26);
            minimizeToTrayToolStripMenuItem.Text = "Minimize To Tray";
            minimizeToTrayToolStripMenuItem.Click += minimizeToTrayToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(251, 26);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { submitABugToolStripMenuItem, gitHubPageToolStripMenuItem, checkForUpdatesToolStripMenuItem, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(55, 24);
            helpToolStripMenuItem.Text = "Help";
            // 
            // submitABugToolStripMenuItem
            // 
            submitABugToolStripMenuItem.Name = "submitABugToolStripMenuItem";
            submitABugToolStripMenuItem.Size = new Size(215, 26);
            submitABugToolStripMenuItem.Text = "Submit A Bug";
            submitABugToolStripMenuItem.Click += submitABugToolStripMenuItem_Click;
            // 
            // gitHubPageToolStripMenuItem
            // 
            gitHubPageToolStripMenuItem.Name = "gitHubPageToolStripMenuItem";
            gitHubPageToolStripMenuItem.Size = new Size(215, 26);
            gitHubPageToolStripMenuItem.Text = "GitHub Page";
            gitHubPageToolStripMenuItem.Click += gitHubPageToolStripMenuItem_Click;
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            checkForUpdatesToolStripMenuItem.Size = new Size(215, 26);
            checkForUpdatesToolStripMenuItem.Text = "Check For Updates";
            checkForUpdatesToolStripMenuItem.Click += checkForUpdatesToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(215, 26);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 435);
            Controls.Add(buttonSelectNetworkFolder);
            Controls.Add(buttonSelectLocalFolder);
            Controls.Add(eventsTextBox);
            Controls.Add(label3);
            Controls.Add(networkFolderTextBox);
            Controls.Add(label2);
            Controls.Add(localFolderTextBox);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Lightroom Sync";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox localFolderTextBox;
        private Label label2;
        private TextBox networkFolderTextBox;
        private Label label3;
        private TextBox eventsTextBox;
        private Button buttonSelectLocalFolder;
        private Button buttonSelectNetworkFolder;
        private System.Windows.Forms.Timer timer1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem launchAtStartupToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem minimizeToTrayToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem submitABugToolStripMenuItem;
        private ToolStripMenuItem gitHubPageToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private ToolStripMenuItem autoCheckForUpdatesToolStripMenuItem;
    }
}