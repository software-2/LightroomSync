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
            minimizeToTrayToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            submitABugToolStripMenuItem = new ToolStripMenuItem();
            gitHubPageToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 39);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 0;
            label1.Text = "Local Folder";
            label1.Click += label1_Click;
            // 
            // localFolderTextBox
            // 
            localFolderTextBox.Location = new Point(12, 57);
            localFolderTextBox.Name = "localFolderTextBox";
            localFolderTextBox.Size = new Size(757, 23);
            localFolderTextBox.TabIndex = 1;
            localFolderTextBox.TextChanged += localFolderTextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 99);
            label2.Name = "label2";
            label2.Size = new Size(88, 15);
            label2.TabIndex = 2;
            label2.Text = "Network Folder";
            // 
            // networkFolderTextBox
            // 
            networkFolderTextBox.Location = new Point(12, 117);
            networkFolderTextBox.Name = "networkFolderTextBox";
            networkFolderTextBox.Size = new Size(757, 23);
            networkFolderTextBox.TabIndex = 3;
            networkFolderTextBox.Text = "P:\\Lightroom";
            networkFolderTextBox.TextChanged += networkFolderTextBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 164);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 4;
            label3.Text = "Events";
            // 
            // eventsTextBox
            // 
            eventsTextBox.Location = new Point(12, 182);
            eventsTextBox.Multiline = true;
            eventsTextBox.Name = "eventsTextBox";
            eventsTextBox.ScrollBars = ScrollBars.Vertical;
            eventsTextBox.Size = new Size(776, 126);
            eventsTextBox.TabIndex = 5;
            // 
            // buttonSelectLocalFolder
            // 
            buttonSelectLocalFolder.Location = new Point(775, 57);
            buttonSelectLocalFolder.Name = "buttonSelectLocalFolder";
            buttonSelectLocalFolder.Size = new Size(21, 23);
            buttonSelectLocalFolder.TabIndex = 8;
            buttonSelectLocalFolder.UseVisualStyleBackColor = true;
            buttonSelectLocalFolder.Click += buttonSelectLocalFolder_Click;
            // 
            // buttonSelectNetworkFolder
            // 
            buttonSelectNetworkFolder.Location = new Point(775, 117);
            buttonSelectNetworkFolder.Name = "buttonSelectNetworkFolder";
            buttonSelectNetworkFolder.Size = new Size(21, 23);
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
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { launchAtStartupToolStripMenuItem, minimizeToTrayToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // launchAtStartupToolStripMenuItem
            // 
            launchAtStartupToolStripMenuItem.Name = "launchAtStartupToolStripMenuItem";
            launchAtStartupToolStripMenuItem.Size = new Size(180, 22);
            launchAtStartupToolStripMenuItem.Text = "Launch At Startup";
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            minimizeToTrayToolStripMenuItem.Size = new Size(180, 22);
            minimizeToTrayToolStripMenuItem.Text = "Minimize To Tray";
            minimizeToTrayToolStripMenuItem.Click += minimizeToTrayToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 22);
            exitToolStripMenuItem.Text = "E&xit";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { submitABugToolStripMenuItem, gitHubPageToolStripMenuItem, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // submitABugToolStripMenuItem
            // 
            submitABugToolStripMenuItem.Name = "submitABugToolStripMenuItem";
            submitABugToolStripMenuItem.Size = new Size(147, 22);
            submitABugToolStripMenuItem.Text = "Submit A Bug";
            submitABugToolStripMenuItem.Click += submitABugToolStripMenuItem_Click;
            // 
            // gitHubPageToolStripMenuItem
            // 
            gitHubPageToolStripMenuItem.Name = "gitHubPageToolStripMenuItem";
            gitHubPageToolStripMenuItem.Size = new Size(147, 22);
            gitHubPageToolStripMenuItem.Text = "GitHub Page";
            gitHubPageToolStripMenuItem.Click += gitHubPageToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(147, 22);
            aboutToolStripMenuItem.Text = "About";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 326);
            Controls.Add(buttonSelectNetworkFolder);
            Controls.Add(buttonSelectLocalFolder);
            Controls.Add(eventsTextBox);
            Controls.Add(label3);
            Controls.Add(networkFolderTextBox);
            Controls.Add(label2);
            Controls.Add(localFolderTextBox);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
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
    }
}