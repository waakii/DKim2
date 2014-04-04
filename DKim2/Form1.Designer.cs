namespace DKim2
{
    partial class MainWindow
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnResolution = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pictureBoxVideo = new System.Windows.Forms.PictureBox();
            this.LogoTimer = new System.Windows.Forms.Timer(this.components);
            this.btnFocus = new System.Windows.Forms.Button();
            this.globalEventProvider1 = new Gma.UserActivityMonitor.GlobalEventProvider();
            this.GCTimer = new System.Windows.Forms.Timer(this.components);
            this.btnSnapshot = new System.Windows.Forms.Button();
            this.ButtonTimer = new System.Windows.Forms.Timer(this.components);
            this.SnapTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnResolution
            // 
            this.btnResolution.ForeColor = System.Drawing.SystemColors.Control;
            this.btnResolution.Location = new System.Drawing.Point(0, 0);
            this.btnResolution.Name = "btnResolution";
            this.btnResolution.Size = new System.Drawing.Size(50, 50);
            this.btnResolution.TabIndex = 0;
            this.btnResolution.Text = "RES";
            this.btnResolution.UseVisualStyleBackColor = false;
            this.btnResolution.Visible = false;
            this.btnResolution.Click += new System.EventHandler(this.btnResolution_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRecord.Location = new System.Drawing.Point(0, 56);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(50, 50);
            this.btnRecord.TabIndex = 1;
            this.btnRecord.Text = "REC";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Visible = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnExit
            // 
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(0, 112);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 50);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pictureBoxVideo
            // 
            this.pictureBoxVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxVideo.Location = new System.Drawing.Point(284, 87);
            this.pictureBoxVideo.Name = "pictureBoxVideo";
            this.pictureBoxVideo.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxVideo.TabIndex = 3;
            this.pictureBoxVideo.TabStop = false;
            // 
            // LogoTimer
            // 
            this.LogoTimer.Interval = 2000;
            this.LogoTimer.Tick += new System.EventHandler(this.LogoTimer_Tick);
            // 
            // btnFocus
            // 
            this.btnFocus.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFocus.Location = new System.Drawing.Point(56, 0);
            this.btnFocus.Name = "btnFocus";
            this.btnFocus.Size = new System.Drawing.Size(50, 50);
            this.btnFocus.TabIndex = 4;
            this.btnFocus.Text = "AF";
            this.btnFocus.UseVisualStyleBackColor = false;
            this.btnFocus.Visible = false;
            this.btnFocus.Click += new System.EventHandler(this.btnFocus_Click);
            // 
            // GCTimer
            // 
            this.GCTimer.Interval = 1000;
            this.GCTimer.Tick += new System.EventHandler(this.GCTimer_Tick);
            // 
            // btnSnapshot
            // 
            this.btnSnapshot.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSnapshot.Location = new System.Drawing.Point(56, 56);
            this.btnSnapshot.Name = "btnSnapshot";
            this.btnSnapshot.Size = new System.Drawing.Size(50, 50);
            this.btnSnapshot.TabIndex = 5;
            this.btnSnapshot.Text = "IMG";
            this.btnSnapshot.UseVisualStyleBackColor = false;
            this.btnSnapshot.Visible = false;
            this.btnSnapshot.Click += new System.EventHandler(this.btnSnapshot_Click);
            // 
            // ButtonTimer
            // 
            this.ButtonTimer.Interval = 3000;
            this.ButtonTimer.Tick += new System.EventHandler(this.ButtonTimer_Tick);
            // 
            // SnapTimer
            // 
            this.SnapTimer.Interval = 200;
            this.SnapTimer.Tick += new System.EventHandler(this.SnapTimer_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(669, 262);
            this.Controls.Add(this.btnSnapshot);
            this.Controls.Add(this.btnFocus);
            this.Controls.Add(this.pictureBoxVideo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnResolution);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnResolution;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pictureBoxVideo;
        private System.Windows.Forms.Timer LogoTimer;
        private System.Windows.Forms.Button btnFocus;
        private Gma.UserActivityMonitor.GlobalEventProvider globalEventProvider1;
        private System.Windows.Forms.Timer GCTimer;
        private System.Windows.Forms.Button btnSnapshot;
        private System.Windows.Forms.Timer ButtonTimer;
        private System.Windows.Forms.Timer SnapTimer;
    }
}

