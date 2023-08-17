namespace Whale_AWS_S3Browser
{
    partial class MainUI
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
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            listView2 = new ListView();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            listView3 = new ListView();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            label1 = new Label();
            label2 = new Label();
            txtBox_AccessKeyID = new TextBox();
            txtBox_SecretAccessKey = new TextBox();
            btn_listbucket = new Button();
            textBox1 = new TextBox();
            btn_CreateFolder = new Button();
            btn_Delete = new Button();
            btn_UploadFolder = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            btn_UploadFile = new Button();
            btn_Download = new Button();
            progressBar1 = new ProgressBar();
            label3 = new Label();
            btn_Download_Whole_Bucket = new Button();
            cmbBox_RegionEndPoints = new ComboBox();
            lbl_RegionEndPoint = new Label();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listView1.Location = new Point(12, 79);
            listView1.Name = "listView1";
            listView1.Size = new Size(414, 405);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged_1;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "BucketName";
            columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "CreationDate";
            columnHeader2.Width = 160;
            // 
            // listView2
            // 
            listView2.Columns.AddRange(new ColumnHeader[] { columnHeader3, columnHeader4 });
            listView2.Location = new Point(432, 79);
            listView2.Name = "listView2";
            listView2.Size = new Size(404, 405);
            listView2.TabIndex = 1;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            listView2.SelectedIndexChanged += listView2_SelectedIndexChanged_1;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Folders";
            columnHeader3.Width = 250;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Modified Date&Time";
            columnHeader4.Width = 150;
            // 
            // listView3
            // 
            listView3.Columns.AddRange(new ColumnHeader[] { columnHeader5, columnHeader6, columnHeader7 });
            listView3.Location = new Point(842, 79);
            listView3.Name = "listView3";
            listView3.Size = new Size(506, 405);
            listView3.TabIndex = 2;
            listView3.UseCompatibleStateImageBehavior = false;
            listView3.View = View.Details;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "FileName";
            columnHeader5.Width = 250;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Size";
            columnHeader6.Width = 100;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Modified Date&Time";
            columnHeader7.Width = 150;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 30);
            label1.Name = "label1";
            label1.Size = new Size(117, 15);
            label1.TabIndex = 3;
            label1.Text = "AWS Access Key ID :-";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(457, 29);
            label2.Name = "label2";
            label2.Size = new Size(99, 15);
            label2.TabIndex = 4;
            label2.Text = "AWS Secret Key :-";
            // 
            // txtBox_AccessKeyID
            // 
            txtBox_AccessKeyID.Location = new Point(128, 22);
            txtBox_AccessKeyID.Name = "txtBox_AccessKeyID";
            txtBox_AccessKeyID.Size = new Size(312, 23);
            txtBox_AccessKeyID.TabIndex = 5;
            txtBox_AccessKeyID.TextChanged += txtBox_AccessKeyID_TextChanged;
            // 
            // txtBox_SecretAccessKey
            // 
            txtBox_SecretAccessKey.Location = new Point(561, 21);
            txtBox_SecretAccessKey.Name = "txtBox_SecretAccessKey";
            txtBox_SecretAccessKey.Size = new Size(291, 23);
            txtBox_SecretAccessKey.TabIndex = 6;
            txtBox_SecretAccessKey.TextChanged += txtBox_SecretAccessKey_TextChanged;
            // 
            // btn_listbucket
            // 
            btn_listbucket.Location = new Point(12, 482);
            btn_listbucket.Name = "btn_listbucket";
            btn_listbucket.Size = new Size(83, 23);
            btn_listbucket.TabIndex = 7;
            btn_listbucket.Text = "Get Buckets";
            btn_listbucket.UseVisualStyleBackColor = true;
            btn_listbucket.Click += btn_listbucket_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(432, 484);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(146, 23);
            textBox1.TabIndex = 8;
            // 
            // btn_CreateFolder
            // 
            btn_CreateFolder.Location = new Point(431, 506);
            btn_CreateFolder.Name = "btn_CreateFolder";
            btn_CreateFolder.Size = new Size(100, 23);
            btn_CreateFolder.TabIndex = 9;
            btn_CreateFolder.Text = "CreateFolder";
            btn_CreateFolder.UseVisualStyleBackColor = true;
            btn_CreateFolder.Click += btn_CreateFolder_Click;
            // 
            // btn_Delete
            // 
            btn_Delete.Location = new Point(584, 483);
            btn_Delete.Name = "btn_Delete";
            btn_Delete.Size = new Size(99, 23);
            btn_Delete.TabIndex = 10;
            btn_Delete.Text = "Delete";
            btn_Delete.UseVisualStyleBackColor = true;
            btn_Delete.Click += btn_Delete_Click;
            // 
            // btn_UploadFolder
            // 
            btn_UploadFolder.Location = new Point(689, 483);
            btn_UploadFolder.Name = "btn_UploadFolder";
            btn_UploadFolder.Size = new Size(95, 23);
            btn_UploadFolder.TabIndex = 11;
            btn_UploadFolder.Text = "UploadFolder";
            btn_UploadFolder.UseVisualStyleBackColor = true;
            btn_UploadFolder.Click += btn_UploadFolder_Click;
            // 
            // btn_UploadFile
            // 
            btn_UploadFile.Location = new Point(689, 506);
            btn_UploadFile.Name = "btn_UploadFile";
            btn_UploadFile.Size = new Size(95, 23);
            btn_UploadFile.TabIndex = 12;
            btn_UploadFile.Text = "UploadFile";
            btn_UploadFile.UseVisualStyleBackColor = true;
            btn_UploadFile.Click += btn_UploadFile_Click;
            // 
            // btn_Download
            // 
            btn_Download.Location = new Point(842, 484);
            btn_Download.Name = "btn_Download";
            btn_Download.Size = new Size(104, 23);
            btn_Download.TabIndex = 13;
            btn_Download.Text = "Download";
            btn_Download.UseVisualStyleBackColor = true;
            btn_Download.Click += btn_Download_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(584, 547);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(252, 23);
            progressBar1.TabIndex = 14;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(432, 555);
            label3.Name = "label3";
            label3.Size = new Size(137, 15);
            label3.TabIndex = 15;
            label3.Text = "Downloading Progress :-";
            // 
            // btn_Download_Whole_Bucket
            // 
            btn_Download_Whole_Bucket.Location = new Point(128, 482);
            btn_Download_Whole_Bucket.Name = "btn_Download_Whole_Bucket";
            btn_Download_Whole_Bucket.Size = new Size(112, 23);
            btn_Download_Whole_Bucket.TabIndex = 16;
            btn_Download_Whole_Bucket.Text = "Download Bucket";
            btn_Download_Whole_Bucket.UseVisualStyleBackColor = true;
            btn_Download_Whole_Bucket.Click += btn_Download_Whole_Bucket_Click;
            // 
            // cmbBox_RegionEndPoints
            // 
            cmbBox_RegionEndPoints.FormattingEnabled = true;
            cmbBox_RegionEndPoints.Location = new Point(1022, 22);
            cmbBox_RegionEndPoints.Name = "cmbBox_RegionEndPoints";
            cmbBox_RegionEndPoints.Size = new Size(147, 23);
            cmbBox_RegionEndPoints.TabIndex = 17;
            // 
            // lbl_RegionEndPoint
            // 
            lbl_RegionEndPoint.AutoSize = true;
            lbl_RegionEndPoint.Location = new Point(907, 30);
            lbl_RegionEndPoint.Name = "lbl_RegionEndPoint";
            lbl_RegionEndPoint.Size = new Size(109, 15);
            lbl_RegionEndPoint.TabIndex = 18;
            lbl_RegionEndPoint.Text = "Region End Point :-";
            // 
            // MainUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1352, 572);
            Controls.Add(lbl_RegionEndPoint);
            Controls.Add(cmbBox_RegionEndPoints);
            Controls.Add(btn_Download_Whole_Bucket);
            Controls.Add(label3);
            Controls.Add(progressBar1);
            Controls.Add(btn_Download);
            Controls.Add(btn_UploadFile);
            Controls.Add(btn_UploadFolder);
            Controls.Add(btn_Delete);
            Controls.Add(btn_CreateFolder);
            Controls.Add(textBox1);
            Controls.Add(btn_listbucket);
            Controls.Add(txtBox_SecretAccessKey);
            Controls.Add(txtBox_AccessKeyID);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listView3);
            Controls.Add(listView2);
            Controls.Add(listView1);
            Name = "MainUI";
            Text = "Whale_AWS_S3Browser";
            WindowState = FormWindowState.Maximized;
            Load += MainUI_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ListView listView2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ListView listView3;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private Label label1;
        private Label label2;
        private TextBox txtBox_AccessKeyID;
        private TextBox txtBox_SecretAccessKey;
        private Button btn_listbucket;
        private TextBox textBox1;
        private Button btn_CreateFolder;
        private Button btn_Delete;
        private Button btn_UploadFolder;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btn_UploadFile;
        private Button btn_Download;
        private ProgressBar progressBar1;
        private Label label3;
        private Button btn_Download_Whole_Bucket;
        private ComboBox cmbBox_RegionEndPoints;
        private Label lbl_RegionEndPoint;
    }
}