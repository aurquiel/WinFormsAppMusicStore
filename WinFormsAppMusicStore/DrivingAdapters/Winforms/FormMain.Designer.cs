namespace WinFormsAppMusicStoreAdmin
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
            panel1 = new Panel();
            panel7 = new Panel();
            panel6.SuspendLayout();
            panel2.SuspendLayout();
            panel5.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            panel1.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // panel6
            // 
            panel6.Controls.Add(panelChildForm);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(1440, 681);
            panel6.TabIndex = 2;
            // 
            // panelChildForm
            // 
            panelChildForm.Dock = DockStyle.Fill;
            panelChildForm.Location = new Point(0, 0);
            panelChildForm.Name = "panelChildForm";
            panelChildForm.Size = new Size(1440, 681);
            panelChildForm.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1440, 62);
            panel2.TabIndex = 2;
            // 
            // panel5
            // 
            panel5.Controls.Add(richTextBoxStatusMessages);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(118, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(1322, 62);
            panel5.TabIndex = 4;
            // 
            // richTextBoxStatusMessages
            // 
            richTextBoxStatusMessages.BackColor = SystemColors.GradientActiveCaption;
            richTextBoxStatusMessages.Dock = DockStyle.Fill;
            richTextBoxStatusMessages.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            richTextBoxStatusMessages.Location = new Point(0, 0);
            richTextBoxStatusMessages.Name = "richTextBoxStatusMessages";
            richTextBoxStatusMessages.ReadOnly = true;
            richTextBoxStatusMessages.Size = new Size(1322, 62);
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
            panel3.Size = new Size(118, 62);
            panel3.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(11, 20);
            label1.Name = "label1";
            label1.Size = new Size(54, 13);
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
            splitContainer.SplitterDistance = 683;
            splitContainer.SplitterWidth = 6;
            splitContainer.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel7);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1442, 753);
            panel1.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.Controls.Add(splitContainer);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(0, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(1442, 753);
            panel7.TabIndex = 1;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1442, 753);
            Controls.Add(panel1);
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
            panel1.ResumeLayout(false);
            panel7.ResumeLayout(false);
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
        private Panel panel1;
        private Panel panel7;
    }
}