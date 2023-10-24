namespace WinFormsAppMusicStoreAdmin
{
    partial class FormOperationAndWait
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOperationAndWait));
            panel4 = new Panel();
            panel5 = new Panel();
            labelMessage = new Label();
            labelOperation = new Label();
            label2 = new Label();
            label1 = new Label();
            bindingSource1 = new BindingSource(components);
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // panel4
            // 
            panel4.BackColor = Color.SlateGray;
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(4, 4, 4, 4);
            panel4.Size = new Size(644, 151);
            panel4.TabIndex = 6;
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.Controls.Add(labelMessage);
            panel5.Controls.Add(labelOperation);
            panel5.Controls.Add(label2);
            panel5.Controls.Add(label1);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(4, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(636, 143);
            panel5.TabIndex = 3;
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            labelMessage.ForeColor = Color.DarkGreen;
            labelMessage.Location = new Point(136, 58);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(100, 13);
            labelMessage.TabIndex = 21;
            labelMessage.Text = "por favor espere...";
            // 
            // labelOperation
            // 
            labelOperation.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            labelOperation.Location = new Point(188, 56);
            labelOperation.Name = "labelOperation";
            labelOperation.Size = new Size(442, 22);
            labelOperation.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(136, 17);
            label2.Name = "label2";
            label2.Size = new Size(140, 15);
            label2.TabIndex = 16;
            label2.Text = "Ejecutando Operaciones";
            // 
            // label1
            // 
            label1.Image = Properties.Resources.wait;
            label1.Location = new Point(-4, 0);
            label1.Name = "label1";
            label1.Size = new Size(135, 143);
            label1.TabIndex = 0;
            // 
            // FormOperationAndWait
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(644, 151);
            Controls.Add(panel4);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "FormOperationAndWait";
            StartPosition = FormStartPosition.Manual;
            Text = "Realizando Operaciones ...";
            TopMost = true;
            FormClosing += FormOperationAndWait_FormClosing;
            Shown += FormWait_Shown;
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel4;
        private Panel panel5;
        private Label label1;
        private BindingSource bindingSource1;
        private Label label2;
        private Label labelOperation;
        private Label labelMessage;
    }
}