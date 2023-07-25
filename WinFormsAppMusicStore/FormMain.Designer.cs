namespace WinFormsAppMusicStore
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            panel6 = new Panel();
            panelChildForm = new Panel();
            panel2 = new Panel();
            panel5 = new Panel();
            richTextBoxStatusMessages = new RichTextBox();
            panel3 = new Panel();
            label1 = new Label();
            splitContainer = new SplitContainer();
            panel6.SuspendLayout();
            panel2.SuspendLayout();
            panel5.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // panel6
            // 
            panel6.Controls.Add(panelChildForm);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(1440, 673);
            panel6.TabIndex = 2;
            // 
            // panelChildForm
            // 
            panelChildForm.Dock = DockStyle.Fill;
            panelChildForm.Location = new Point(0, 0);
            panelChildForm.Name = "panelChildForm";
            panelChildForm.Size = new Size(1440, 673);
            panelChildForm.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1440, 70);
            panel2.TabIndex = 2;
            // 
            // panel5
            // 
            panel5.Controls.Add(richTextBoxStatusMessages);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(118, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(1322, 70);
            panel5.TabIndex = 4;
            // 
            // richTextBoxStatusMessages
            // 
            richTextBoxStatusMessages.BackColor = SystemColors.GradientActiveCaption;
            richTextBoxStatusMessages.Dock = DockStyle.Fill;
            richTextBoxStatusMessages.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            richTextBoxStatusMessages.Location = new Point(0, 0);
            richTextBoxStatusMessages.Name = "richTextBoxStatusMessages";
            richTextBoxStatusMessages.ReadOnly = true;
            richTextBoxStatusMessages.Size = new Size(1322, 70);
            richTextBoxStatusMessages.TabIndex = 0;
            richTextBoxStatusMessages.Text = "";
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(118, 70);
            panel3.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(5, 21);
            label1.Name = "label1";
            label1.Size = new Size(100, 28);
            label1.TabIndex = 0;
            label1.Text = "ESTATUS:";
            // 
            // splitContainer
            // 
            splitContainer.BorderStyle = BorderStyle.FixedSingle;
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.FixedPanel = FixedPanel.Panel2;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(panel6);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(panel2);
            splitContainer.Panel2MinSize = 45;
            splitContainer.Size = new Size(1442, 753);
            splitContainer.SplitterDistance = 675;
            splitContainer.SplitterWidth = 6;
            splitContainer.TabIndex = 3;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(12F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1442, 753);
            Controls.Add(splitContainer);
            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Music Store";
            panel6.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel6;
        private Panel panelChildForm;
        private Panel panel2;
        private Panel panel5;
        private RichTextBox richTextBoxStatusMessages;
        private Panel panel3;
        private Label label1;
        private SplitContainer splitContainer;
    }
}