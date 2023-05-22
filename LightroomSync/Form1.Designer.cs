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
            button1 = new Button();
            button2 = new Button();
            buttonSelectLocalFolder = new Button();
            buttonSelectNetworkFolder = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 0;
            label1.Text = "Local Folder";
            label1.Click += label1_Click;
            // 
            // localFolderTextBox
            // 
            localFolderTextBox.Location = new Point(12, 27);
            localFolderTextBox.Name = "localFolderTextBox";
            localFolderTextBox.Size = new Size(757, 23);
            localFolderTextBox.TabIndex = 1;
            localFolderTextBox.TextChanged += localFolderTextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 69);
            label2.Name = "label2";
            label2.Size = new Size(88, 15);
            label2.TabIndex = 2;
            label2.Text = "Network Folder";
            // 
            // networkFolderTextBox
            // 
            networkFolderTextBox.Location = new Point(12, 87);
            networkFolderTextBox.Name = "networkFolderTextBox";
            networkFolderTextBox.Size = new Size(757, 23);
            networkFolderTextBox.TabIndex = 3;
            networkFolderTextBox.Text = "P:\\Lightroom";
            networkFolderTextBox.TextChanged += networkFolderTextBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 134);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 4;
            label3.Text = "Events";
            // 
            // eventsTextBox
            // 
            eventsTextBox.Location = new Point(12, 152);
            eventsTextBox.Multiline = true;
            eventsTextBox.Name = "eventsTextBox";
            eventsTextBox.ScrollBars = ScrollBars.Vertical;
            eventsTextBox.Size = new Size(776, 126);
            eventsTextBox.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(418, 370);
            button1.Name = "button1";
            button1.Size = new Size(149, 68);
            button1.TabIndex = 6;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(573, 370);
            button2.Name = "button2";
            button2.Size = new Size(149, 68);
            button2.TabIndex = 7;
            button2.Text = "Sync";
            button2.UseVisualStyleBackColor = true;
            // 
            // buttonSelectLocalFolder
            // 
            buttonSelectLocalFolder.Location = new Point(775, 27);
            buttonSelectLocalFolder.Name = "buttonSelectLocalFolder";
            buttonSelectLocalFolder.Size = new Size(21, 23);
            buttonSelectLocalFolder.TabIndex = 8;
            buttonSelectLocalFolder.UseVisualStyleBackColor = true;
            buttonSelectLocalFolder.Click += buttonSelectLocalFolder_Click;
            // 
            // buttonSelectNetworkFolder
            // 
            buttonSelectNetworkFolder.Location = new Point(775, 87);
            buttonSelectNetworkFolder.Name = "buttonSelectNetworkFolder";
            buttonSelectNetworkFolder.Size = new Size(21, 23);
            buttonSelectNetworkFolder.TabIndex = 9;
            buttonSelectNetworkFolder.UseVisualStyleBackColor = true;
            buttonSelectNetworkFolder.Click += buttonSelectNetworkFolder_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonSelectNetworkFolder);
            Controls.Add(buttonSelectLocalFolder);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(eventsTextBox);
            Controls.Add(label3);
            Controls.Add(networkFolderTextBox);
            Controls.Add(label2);
            Controls.Add(localFolderTextBox);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
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
        private Button button1;
        private Button button2;
        private Button buttonSelectLocalFolder;
        private Button buttonSelectNetworkFolder;
        private System.Windows.Forms.Timer timer1;
    }
}