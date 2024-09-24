namespace PROFiLiX.GUI.Forms
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            btnExit = new Button();
            lblProfileDetail = new Label();
            pictureBox1 = new PictureBox();
            chkClearTempAtStart = new CheckBox();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(255, 128, 128);
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseOverBackColor = Color.Red;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.ForeColor = Color.FromArgb(64, 64, 64);
            btnExit.Location = new Point(471, 8);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(40, 24);
            btnExit.TabIndex = 27;
            btnExit.Text = "X";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // lblProfileDetail
            // 
            lblProfileDetail.AutoSize = true;
            lblProfileDetail.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblProfileDetail.ForeColor = Color.FromArgb(64, 64, 64);
            lblProfileDetail.Location = new Point(41, 9);
            lblProfileDetail.Name = "lblProfileDetail";
            lblProfileDetail.Size = new Size(79, 25);
            lblProfileDetail.TabIndex = 24;
            lblProfileDetail.Text = "Settings";
            lblProfileDetail.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(7, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 29);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 23;
            pictureBox1.TabStop = false;
            // 
            // chkClearTempAtStart
            // 
            chkClearTempAtStart.AutoSize = true;
            chkClearTempAtStart.FlatStyle = FlatStyle.Flat;
            chkClearTempAtStart.Location = new Point(7, 55);
            chkClearTempAtStart.Name = "chkClearTempAtStart";
            chkClearTempAtStart.Size = new Size(164, 19);
            chkClearTempAtStart.TabIndex = 28;
            chkClearTempAtStart.Text = "Clear Temp Files At Startup";
            chkClearTempAtStart.UseVisualStyleBackColor = true;
            chkClearTempAtStart.CheckStateChanged += Clicked;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.ForeColor = Color.FromArgb(64, 64, 64);
            lblStatus.Location = new Point(4, 90);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(507, 19);
            lblStatus.TabIndex = 34;
            lblStatus.Text = "Nothing Changed";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(519, 115);
            Controls.Add(lblStatus);
            Controls.Add(chkClearTempAtStart);
            Controls.Add(btnExit);
            Controls.Add(lblProfileDetail);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormSettings";
            Text = "FormSettings";
            Load += FormSettings_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnExit;
        private Label lblProfileDetail;
        private PictureBox pictureBox1;
        private CheckBox chkClearTempAtStart;
        private Label lblStatus;
    }
}