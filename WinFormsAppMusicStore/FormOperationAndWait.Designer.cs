namespace WinFormsAppMusicStore
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
            panel4 = new Panel();
            panel5 = new Panel();
            labelMessage = new Label();
            labelOperation = new Label();
            labelTotalNumber = new Label();
            label4 = new Label();
            labelActualNumber = new Label();
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
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(5, 5, 5, 5);
            panel4.Size = new Size(760, 256);
            panel4.TabIndex = 6;
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.Controls.Add(labelMessage);
            panel5.Controls.Add(labelOperation);
            panel5.Controls.Add(labelTotalNumber);
            panel5.Controls.Add(label4);
            panel5.Controls.Add(labelActualNumber);
            panel5.Controls.Add(label2);
            panel5.Controls.Add(label1);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(5, 5);
            panel5.Margin = new Padding(3, 4, 3, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(750, 246);
            panel5.TabIndex = 3;
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            labelMessage.ForeColor = Color.DarkGreen;
            labelMessage.Location = new Point(238, 125);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(137, 20);
            labelMessage.TabIndex = 21;
            labelMessage.Text = "por favor espere...";
            // 
            // labelOperation
            // 
            labelOperation.Location = new Point(238, 95);
            labelOperation.Name = "labelOperation";
            labelOperation.Size = new Size(505, 29);
            labelOperation.TabIndex = 20;
            // 
            // labelTotalNumber
            // 
            labelTotalNumber.AutoSize = true;
            labelTotalNumber.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelTotalNumber.Location = new Point(471, 56);
            labelTotalNumber.Name = "labelTotalNumber";
            labelTotalNumber.Size = new Size(24, 28);
            labelTotalNumber.TabIndex = 19;
            labelTotalNumber.Text = "1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(409, 56);
            label4.Name = "label4";
            label4.Size = new Size(35, 28);
            label4.TabIndex = 18;
            label4.Text = "de";
            // 
            // labelActualNumber
            // 
            labelActualNumber.AutoSize = true;
            labelActualNumber.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelActualNumber.Location = new Point(365, 56);
            labelActualNumber.Name = "labelActualNumber";
            labelActualNumber.Size = new Size(24, 28);
            labelActualNumber.TabIndex = 17;
            labelActualNumber.Text = "1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(238, 56);
            label2.Name = "label2";
            label2.Size = new Size(109, 28);
            label2.TabIndex = 16;
            label2.Text = "Operacion";
            // 
            // label1
            // 
            label1.Image = Properties.Resources.loader;
            label1.Location = new Point(9, 0);
            label1.Name = "label1";
            label1.Size = new Size(223, 246);
            label1.TabIndex = 0;
            // 
            // FormOperationAndWait
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 256);
            Controls.Add(panel4);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormOperationAndWait";
            StartPosition = FormStartPosition.Manual;
            Text = "FormWait";
            TopMost = true;
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
        private Label labelTotalNumber;
        private Label label4;
        private Label labelActualNumber;
        private Label labelOperation;
        private Label labelMessage;
    }
}