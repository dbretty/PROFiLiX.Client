namespace PROFiLiX.GUI.Forms
{
    partial class FormDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDetail));
			lblProfileDetail = new Label();
			pictureBox1 = new PictureBox();
			dgProfileDetails = new DataGridView();
			Key = new DataGridViewTextBoxColumn();
			Value = new DataGridViewTextBoxColumn();
			label7 = new Label();
			btnProfileDetailsSort = new Button();
			btnFolderRedirectionSort = new Button();
			label3 = new Label();
			dgFolderRedirection = new DataGridView();
			dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			btnExit = new Button();
			lblStatus = new Label();
			pbMain = new ProgressBar();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgProfileDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgFolderRedirection).BeginInit();
			SuspendLayout();
			// 
			// lblProfileDetail
			// 
			lblProfileDetail.AutoSize = true;
			lblProfileDetail.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblProfileDetail.ForeColor = Color.FromArgb(64, 64, 64);
			lblProfileDetail.Location = new Point(46, 20);
			lblProfileDetail.Name = "lblProfileDetail";
			lblProfileDetail.Size = new Size(121, 25);
			lblProfileDetail.TabIndex = 6;
			lblProfileDetail.Text = "Profile Detail";
			lblProfileDetail.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// pictureBox1
			// 
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(12, 19);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(32, 29);
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 5;
			pictureBox1.TabStop = false;
			// 
			// dgProfileDetails
			// 
			dgProfileDetails.AllowUserToAddRows = false;
			dgProfileDetails.AllowUserToDeleteRows = false;
			dgProfileDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgProfileDetails.ColumnHeadersVisible = false;
			dgProfileDetails.Columns.AddRange(new DataGridViewColumn[] { Key, Value });
			dgProfileDetails.EditMode = DataGridViewEditMode.EditProgrammatically;
			dgProfileDetails.Location = new Point(12, 80);
			dgProfileDetails.MultiSelect = false;
			dgProfileDetails.Name = "dgProfileDetails";
			dgProfileDetails.ReadOnly = true;
			dgProfileDetails.RowHeadersVisible = false;
			dgProfileDetails.ShowEditingIcon = false;
			dgProfileDetails.Size = new Size(699, 341);
			dgProfileDetails.TabIndex = 15;
			// 
			// Key
			// 
			Key.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			Key.HeaderText = "Key";
			Key.Name = "Key";
			Key.ReadOnly = true;
			Key.SortMode = DataGridViewColumnSortMode.Programmatic;
			Key.Width = 5;
			// 
			// Value
			// 
			Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			Value.HeaderText = "Value";
			Value.Name = "Value";
			Value.ReadOnly = true;
			Value.SortMode = DataGridViewColumnSortMode.Programmatic;
			Value.Width = 5;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label7.ForeColor = Color.FromArgb(64, 64, 64);
			label7.Location = new Point(9, 57);
			label7.Name = "label7";
			label7.Size = new Size(103, 19);
			label7.TabIndex = 16;
			label7.Text = "Profile Details";
			label7.TextAlign = ContentAlignment.MiddleRight;
			// 
			// btnProfileDetailsSort
			// 
			btnProfileDetailsSort.BackColor = Color.White;
			btnProfileDetailsSort.FlatAppearance.BorderColor = Color.SteelBlue;
			btnProfileDetailsSort.FlatAppearance.MouseOverBackColor = Color.Silver;
			btnProfileDetailsSort.FlatStyle = FlatStyle.Flat;
			btnProfileDetailsSort.ForeColor = Color.FromArgb(64, 64, 64);
			btnProfileDetailsSort.Location = new Point(654, 50);
			btnProfileDetailsSort.Name = "btnProfileDetailsSort";
			btnProfileDetailsSort.Size = new Size(57, 24);
			btnProfileDetailsSort.TabIndex = 18;
			btnProfileDetailsSort.Text = "Asc";
			btnProfileDetailsSort.UseVisualStyleBackColor = false;
			btnProfileDetailsSort.Click += btnProfileDetailsSort_Click;
			// 
			// btnFolderRedirectionSort
			// 
			btnFolderRedirectionSort.BackColor = Color.White;
			btnFolderRedirectionSort.FlatAppearance.BorderColor = Color.SteelBlue;
			btnFolderRedirectionSort.FlatAppearance.MouseOverBackColor = Color.Silver;
			btnFolderRedirectionSort.FlatStyle = FlatStyle.Flat;
			btnFolderRedirectionSort.ForeColor = Color.FromArgb(64, 64, 64);
			btnFolderRedirectionSort.Location = new Point(654, 427);
			btnFolderRedirectionSort.Name = "btnFolderRedirectionSort";
			btnFolderRedirectionSort.Size = new Size(57, 24);
			btnFolderRedirectionSort.TabIndex = 21;
			btnFolderRedirectionSort.Text = "Asc";
			btnFolderRedirectionSort.UseVisualStyleBackColor = false;
			btnFolderRedirectionSort.Click += btnFolderRedirectionSort_Click;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label3.ForeColor = Color.FromArgb(64, 64, 64);
			label3.Location = new Point(9, 434);
			label3.Name = "label3";
			label3.Size = new Size(182, 19);
			label3.TabIndex = 20;
			label3.Text = "Folder Redirection Details";
			label3.TextAlign = ContentAlignment.MiddleRight;
			// 
			// dgFolderRedirection
			// 
			dgFolderRedirection.AllowUserToAddRows = false;
			dgFolderRedirection.AllowUserToDeleteRows = false;
			dgFolderRedirection.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgFolderRedirection.ColumnHeadersVisible = false;
			dgFolderRedirection.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2 });
			dgFolderRedirection.EditMode = DataGridViewEditMode.EditProgrammatically;
			dgFolderRedirection.Location = new Point(12, 457);
			dgFolderRedirection.MultiSelect = false;
			dgFolderRedirection.Name = "dgFolderRedirection";
			dgFolderRedirection.ReadOnly = true;
			dgFolderRedirection.RowHeadersVisible = false;
			dgFolderRedirection.ShowEditingIcon = false;
			dgFolderRedirection.Size = new Size(699, 194);
			dgFolderRedirection.TabIndex = 19;
			// 
			// dataGridViewTextBoxColumn1
			// 
			dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewTextBoxColumn1.HeaderText = "Key";
			dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			dataGridViewTextBoxColumn1.ReadOnly = true;
			dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Programmatic;
			dataGridViewTextBoxColumn1.Width = 5;
			// 
			// dataGridViewTextBoxColumn2
			// 
			dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewTextBoxColumn2.HeaderText = "Value";
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.ReadOnly = true;
			dataGridViewTextBoxColumn2.Width = 5;
			// 
			// btnExit
			// 
			btnExit.BackColor = Color.FromArgb(255, 128, 128);
			btnExit.FlatAppearance.BorderSize = 0;
			btnExit.FlatAppearance.MouseOverBackColor = Color.Red;
			btnExit.FlatStyle = FlatStyle.Flat;
			btnExit.ForeColor = Color.FromArgb(64, 64, 64);
			btnExit.Location = new Point(676, 7);
			btnExit.Name = "btnExit";
			btnExit.Size = new Size(40, 24);
			btnExit.TabIndex = 22;
			btnExit.Text = "X";
			btnExit.UseVisualStyleBackColor = false;
			btnExit.Click += btnExit_Click;
			// 
			// lblStatus
			// 
			lblStatus.Font = new Font("Segoe UI", 10F);
			lblStatus.ForeColor = Color.FromArgb(64, 64, 64);
			lblStatus.Location = new Point(9, 661);
			lblStatus.Name = "lblStatus";
			lblStatus.Size = new Size(239, 19);
			lblStatus.TabIndex = 33;
			lblStatus.Text = "Ready";
			lblStatus.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// pbMain
			// 
			pbMain.ForeColor = Color.SteelBlue;
			pbMain.Location = new Point(257, 664);
			pbMain.Name = "pbMain";
			pbMain.Size = new Size(454, 16);
			pbMain.Style = ProgressBarStyle.Marquee;
			pbMain.TabIndex = 32;
			// 
			// FormDetail
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			ClientSize = new Size(723, 689);
			Controls.Add(lblStatus);
			Controls.Add(pbMain);
			Controls.Add(btnExit);
			Controls.Add(btnFolderRedirectionSort);
			Controls.Add(label3);
			Controls.Add(dgFolderRedirection);
			Controls.Add(btnProfileDetailsSort);
			Controls.Add(label7);
			Controls.Add(dgProfileDetails);
			Controls.Add(lblProfileDetail);
			Controls.Add(pictureBox1);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "FormDetail";
			Text = "Profile Details";
			Load += FormDetail_Load;
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)dgProfileDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)dgFolderRedirection).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblProfileDetail;
        private PictureBox pictureBox1;
        private DataGridView dgProfileDetails;
        private Label label7;
        private Button btnProfileDetailsSort;
        private Button btnFolderRedirectionSort;
        private Label label3;
        private DataGridView dgFolderRedirection;
        private Button btnExit;
        private DataGridViewTextBoxColumn Key;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Label lblStatus;
        private ProgressBar pbMain;
    }
}