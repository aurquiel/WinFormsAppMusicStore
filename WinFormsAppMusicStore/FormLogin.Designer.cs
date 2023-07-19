namespace WinFormsAppMusicStore
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            textBoxStatus = new TextBox();
            buttonClose = new Button();
            buttonLogin = new Button();
            textBoxUserPassword = new TextBox();
            textBoxUserAlias = new TextBox();
            label2 = new Label();
            label1 = new Label();
            pictureBoxLogo = new PictureBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // textBoxStatus
            // 
            textBoxStatus.BorderStyle = BorderStyle.None;
            textBoxStatus.Location = new Point(166, 420);
            textBoxStatus.Margin = new Padding(3, 4, 3, 4);
            textBoxStatus.Name = "textBoxStatus";
            textBoxStatus.Size = new Size(293, 20);
            textBoxStatus.TabIndex = 15;
            textBoxStatus.TabStop = false;
            // 
            // buttonClose
            // 
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonClose.ForeColor = Color.Red;
            buttonClose.Image = Properties.Resources.close;
            buttonClose.ImageAlign = ContentAlignment.MiddleRight;
            buttonClose.Location = new Point(342, 473);
            buttonClose.Margin = new Padding(3, 4, 3, 4);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(118, 55);
            buttonClose.TabIndex = 14;
            buttonClose.Text = "Salir";
            buttonClose.TextAlign = ContentAlignment.MiddleLeft;
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // buttonLogin
            // 
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonLogin.ForeColor = Color.Green;
            buttonLogin.Image = Properties.Resources.login;
            buttonLogin.ImageAlign = ContentAlignment.MiddleRight;
            buttonLogin.Location = new Point(217, 473);
            buttonLogin.Margin = new Padding(3, 4, 3, 4);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(118, 55);
            buttonLogin.TabIndex = 12;
            buttonLogin.Text = "Entrar";
            buttonLogin.TextAlign = ContentAlignment.MiddleLeft;
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // textBoxUserPassword
            // 
            textBoxUserPassword.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxUserPassword.Location = new Point(165, 368);
            textBoxUserPassword.Margin = new Padding(3, 4, 3, 4);
            textBoxUserPassword.Name = "textBoxUserPassword";
            textBoxUserPassword.Size = new Size(293, 30);
            textBoxUserPassword.TabIndex = 10;
            textBoxUserPassword.Text = "54001990";
            textBoxUserPassword.UseSystemPasswordChar = true;
            // 
            // textBoxUserAlias
            // 
            textBoxUserAlias.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxUserAlias.Location = new Point(166, 309);
            textBoxUserAlias.Margin = new Padding(3, 4, 3, 4);
            textBoxUserAlias.Name = "textBoxUserAlias";
            textBoxUserAlias.Size = new Size(293, 30);
            textBoxUserAlias.TabIndex = 8;
            textBoxUserAlias.Text = "ag01";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(30, 372);
            label2.Name = "label2";
            label2.Size = new Size(131, 25);
            label2.TabIndex = 13;
            label2.Text = "Contraseña:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(30, 313);
            label1.Name = "label1";
            label1.Size = new Size(93, 25);
            label1.TabIndex = 11;
            label1.Text = "Usuario:";
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Dock = DockStyle.Top;
            pictureBoxLogo.ErrorImage = null;
            pictureBoxLogo.Image = Properties.Resources.logoMundoTotalGrande;
            pictureBoxLogo.Location = new Point(0, 0);
            pictureBoxLogo.Margin = new Padding(3, 4, 3, 4);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(496, 211);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxLogo.TabIndex = 9;
            pictureBoxLogo.TabStop = false;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.SteelBlue;
            label3.Location = new Point(0, 211);
            label3.Margin = new Padding(3, 27, 3, 0);
            label3.Name = "label3";
            label3.Size = new Size(496, 72);
            label3.TabIndex = 16;
            label3.Text = "MUSICA TIENDAS";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(496, 581);
            Controls.Add(label3);
            Controls.Add(textBoxStatus);
            Controls.Add(buttonClose);
            Controls.Add(buttonLogin);
            Controls.Add(textBoxUserPassword);
            Controls.Add(textBoxUserAlias);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBoxLogo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login Music Store";
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxStatus;
        private Button buttonClose;
        private Button buttonLogin;
        private TextBox textBoxUserPassword;
        private TextBox textBoxUserAlias;
        private Label label2;
        private Label label1;
        private PictureBox pictureBoxLogo;
        private Label label3;
    }
}