namespace WinFormsAppMusicStoreAdmin
{
    partial class UserControlPlayer
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            panel2 = new Panel();
            labelOnline = new Label();
            buttonPullFromPc = new Button();
            labelTotalTime = new Label();
            label4 = new Label();
            labelCurrentTime = new Label();
            progressBarAudio = new ProgressBar();
            label2 = new Label();
            trackBarVolume = new TrackBar();
            buttonStop = new Button();
            buttonPause = new Button();
            buttonPlay = new Button();
            buttonPullFromServer = new Button();
            label1 = new Label();
            listBoxAudio = new ListBox();
            panel4 = new Panel();
            panel5 = new Panel();
            tableLayoutPanel.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).BeginInit();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(panel2, 0, 2);
            tableLayoutPanel.Controls.Add(label1, 0, 0);
            tableLayoutPanel.Controls.Add(listBoxAudio, 0, 1);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel.Size = new Size(814, 391);
            tableLayoutPanel.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(labelOnline);
            panel2.Controls.Add(buttonPullFromPc);
            panel2.Controls.Add(labelTotalTime);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(labelCurrentTime);
            panel2.Controls.Add(progressBarAudio);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(trackBarVolume);
            panel2.Controls.Add(buttonStop);
            panel2.Controls.Add(buttonPause);
            panel2.Controls.Add(buttonPlay);
            panel2.Controls.Add(buttonPullFromServer);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 350);
            panel2.Name = "panel2";
            panel2.Size = new Size(808, 38);
            panel2.TabIndex = 22;
            // 
            // labelOnline
            // 
            labelOnline.AutoSize = true;
            labelOnline.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelOnline.Location = new Point(78, 10);
            labelOnline.Name = "labelOnline";
            labelOnline.Size = new Size(42, 15);
            labelOnline.TabIndex = 34;
            labelOnline.Text = "Modo:";
            // 
            // buttonPullFromPc
            // 
            buttonPullFromPc.BackColor = Color.White;
            buttonPullFromPc.FlatStyle = FlatStyle.Flat;
            buttonPullFromPc.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonPullFromPc.ForeColor = Color.Black;
            buttonPullFromPc.Image = Properties.Resources.pc;
            buttonPullFromPc.Location = new Point(3, 3);
            buttonPullFromPc.Name = "buttonPullFromPc";
            buttonPullFromPc.Size = new Size(32, 27);
            buttonPullFromPc.TabIndex = 33;
            buttonPullFromPc.TextAlign = ContentAlignment.MiddleLeft;
            buttonPullFromPc.UseVisualStyleBackColor = false;
            buttonPullFromPc.Click += buttonPullFromPc_Click;
            // 
            // labelTotalTime
            // 
            labelTotalTime.AutoSize = true;
            labelTotalTime.Location = new Point(427, 19);
            labelTotalTime.Name = "labelTotalTime";
            labelTotalTime.Size = new Size(34, 15);
            labelTotalTime.TabIndex = 32;
            labelTotalTime.Text = "00:00";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(411, 19);
            label4.Name = "label4";
            label4.Size = new Size(10, 15);
            label4.TabIndex = 31;
            label4.Text = ":";
            // 
            // labelCurrentTime
            // 
            labelCurrentTime.AutoSize = true;
            labelCurrentTime.Location = new Point(368, 19);
            labelCurrentTime.Name = "labelCurrentTime";
            labelCurrentTime.Size = new Size(34, 15);
            labelCurrentTime.TabIndex = 30;
            labelCurrentTime.Text = "00:00";
            // 
            // progressBarAudio
            // 
            progressBarAudio.BackColor = Color.White;
            progressBarAudio.ForeColor = Color.Blue;
            progressBarAudio.Location = new Point(285, 8);
            progressBarAudio.Margin = new Padding(3, 2, 3, 2);
            progressBarAudio.Name = "progressBarAudio";
            progressBarAudio.Size = new Size(270, 8);
            progressBarAudio.TabIndex = 28;
            progressBarAudio.MouseDown += progressBarAudio_MouseDown;
            // 
            // label2
            // 
            label2.Image = Properties.Resources.volume;
            label2.Location = new Point(561, 3);
            label2.Name = "label2";
            label2.Size = new Size(33, 27);
            label2.TabIndex = 27;
            // 
            // trackBarVolume
            // 
            trackBarVolume.BackColor = Color.White;
            trackBarVolume.LargeChange = 1;
            trackBarVolume.Location = new Point(600, 8);
            trackBarVolume.Maximum = 100;
            trackBarVolume.Name = "trackBarVolume";
            trackBarVolume.Size = new Size(107, 45);
            trackBarVolume.TabIndex = 26;
            trackBarVolume.TickStyle = TickStyle.None;
            trackBarVolume.Scroll += trackBarVolume_Scroll;
            // 
            // buttonStop
            // 
            buttonStop.BackColor = Color.White;
            buttonStop.FlatStyle = FlatStyle.Flat;
            buttonStop.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonStop.ForeColor = Color.Black;
            buttonStop.Image = Properties.Resources.stop;
            buttonStop.Location = new Point(248, 3);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(32, 27);
            buttonStop.TabIndex = 24;
            buttonStop.TextAlign = ContentAlignment.MiddleLeft;
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Click += buttonStop_Click;
            // 
            // buttonPause
            // 
            buttonPause.BackColor = Color.White;
            buttonPause.FlatStyle = FlatStyle.Flat;
            buttonPause.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonPause.ForeColor = Color.Black;
            buttonPause.Image = Properties.Resources.pause;
            buttonPause.Location = new Point(209, 3);
            buttonPause.Name = "buttonPause";
            buttonPause.Size = new Size(32, 27);
            buttonPause.TabIndex = 23;
            buttonPause.TextAlign = ContentAlignment.MiddleLeft;
            buttonPause.UseVisualStyleBackColor = false;
            buttonPause.Click += buttonPause_Click;
            // 
            // buttonPlay
            // 
            buttonPlay.BackColor = Color.White;
            buttonPlay.FlatStyle = FlatStyle.Flat;
            buttonPlay.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonPlay.ForeColor = Color.Black;
            buttonPlay.Image = Properties.Resources.play;
            buttonPlay.Location = new Point(172, 3);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(32, 27);
            buttonPlay.TabIndex = 22;
            buttonPlay.TextAlign = ContentAlignment.MiddleLeft;
            buttonPlay.UseVisualStyleBackColor = false;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // buttonPullFromServer
            // 
            buttonPullFromServer.BackColor = Color.White;
            buttonPullFromServer.FlatStyle = FlatStyle.Flat;
            buttonPullFromServer.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonPullFromServer.ForeColor = Color.RoyalBlue;
            buttonPullFromServer.Image = Properties.Resources.replicaDown;
            buttonPullFromServer.Location = new Point(40, 3);
            buttonPullFromServer.Name = "buttonPullFromServer";
            buttonPullFromServer.Size = new Size(32, 27);
            buttonPullFromServer.TabIndex = 21;
            buttonPullFromServer.TextAlign = ContentAlignment.MiddleLeft;
            buttonPullFromServer.UseVisualStyleBackColor = false;
            buttonPullFromServer.Click += buttonPullFromServer_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.White;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(808, 24);
            label1.TabIndex = 2;
            label1.Text = "REPRODUCTOR";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listBoxAudio
            // 
            listBoxAudio.Dock = DockStyle.Fill;
            listBoxAudio.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxAudio.FormattingEnabled = true;
            listBoxAudio.ItemHeight = 16;
            listBoxAudio.Location = new Point(3, 27);
            listBoxAudio.Name = "listBoxAudio";
            listBoxAudio.ScrollAlwaysVisible = true;
            listBoxAudio.Size = new Size(808, 317);
            listBoxAudio.TabIndex = 0;
            listBoxAudio.MouseDoubleClick += listBoxAudio_MouseDoubleClick;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(4);
            panel4.Size = new Size(822, 399);
            panel4.TabIndex = 6;
            // 
            // panel5
            // 
            panel5.BackColor = Color.WhiteSmoke;
            panel5.Controls.Add(tableLayoutPanel);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(4, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(814, 391);
            panel5.TabIndex = 3;
            // 
            // UserControlPlayer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel4);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UserControlPlayer";
            Size = new Size(822, 399);
            tableLayoutPanel.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).EndInit();
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private Label label1;
        private ListBox listBoxAudio;
        private Panel panel2;
        private Button buttonStop;
        private Button buttonPause;
        private Button buttonPlay;
        private Button buttonPullFromServer;
        private TrackBar trackBarVolume;
        private Label label2;
        private Panel panel4;
        private Panel panel5;
        private ProgressBar progressBarAudio;
        private Label labelTotalTime;
        private Label label4;
        private Label labelCurrentTime;
        private Button buttonPullFromPc;
        private Label labelOnline;
    }
}
