namespace LightroomSync
{
    partial class Alert
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
            label_info = new Label();
            button_KillLightroom = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // label_info
            // 
            label_info.Location = new Point(12, 9);
            label_info.Name = "label_info";
            label_info.Size = new Size(776, 252);
            label_info.TabIndex = 0;
            label_info.Text = "This ";
            // 
            // button_KillLightroom
            // 
            button_KillLightroom.DialogResult = DialogResult.Yes;
            button_KillLightroom.Location = new Point(88, 359);
            button_KillLightroom.Name = "button_KillLightroom";
            button_KillLightroom.Size = new Size(208, 51);
            button_KillLightroom.TabIndex = 1;
            button_KillLightroom.Text = "Kill Lightroom";
            button_KillLightroom.UseVisualStyleBackColor = true;
            button_KillLightroom.Click += button_KillLightroom_Click;
            // 
            // button1
            // 
            button1.DialogResult = DialogResult.No;
            button1.Location = new Point(510, 359);
            button1.Name = "button1";
            button1.Size = new Size(208, 51);
            button1.TabIndex = 2;
            button1.Text = "Override Catalogs";
            button1.UseVisualStyleBackColor = true;
            // 
            // Alert
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(button1);
            Controls.Add(button_KillLightroom);
            Controls.Add(label_info);
            Name = "Alert";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "STOP USING LIGHTROOM";
            TopMost = true;
            FormClosing += Alert_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Label label_info;
        private Button button_KillLightroom;
        private Button button1;
    }
}