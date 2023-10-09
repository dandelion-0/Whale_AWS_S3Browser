using System;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Whale_AWS_S3Browser
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();
        }
        private void MainUI_Load(object sender, EventArgs e)
        {
            //getbuckets_of_awsaccount();

            // To list all regionenddpoint Names in combobox while MainUI Form initalize
            List_RegionEndPoint_Names();
        }

        static string _S3AccessKey = string.Empty;
        static string _S3SecretKey = string.Empty;
        IAmazonS3 _client;
        static string bucketName = string.Empty;
        static string foldername = string.Empty;
        static string _downloadpath = string.Empty;


        // To list all RegionEndpoint name in combobox 
        private void List_RegionEndPoint_Names()
        {
            try
            {
                IEnumerable<RegionEndpoint> regions = RegionEndpoint.EnumerableAllRegions;

                foreach (var region in regions)
                {
                    cmbBox_RegionEndPoints.Items.Add(region.SystemName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // To Create Credentials with AWS Account
        private bool Create_AWSCredentials()
        {
            try
            {
                //Create Credential and store in _client
                // _client = new AmazonS3Client(_S3AccessKey, _S3SecretKey, Amazon.RegionEndpoint.APSouth1);
                _client = new AmazonS3Client(_S3AccessKey, _S3SecretKey, Amazon.RegionEndpoint.GetBySystemName(cmbBox_RegionEndPoints.Text));
                return true; // if successfully credential is created 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        IEnumerable<string> GetFiles(string path)
        {
            //To get all directories and Files Recursively
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                        CreateDir(subDir);
                        //listbucket();

                        foreach (string subsubDir in Directory.GetDirectories(subDir))
                        {
                            queue.Enqueue(subsubDir);
                            CreateDir(subsubDir);
                            //listbucket();
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        yield return files[i];
                    }
                }
            }
        }
        private async void UploadFile(string localFilePath, string subDirectoryInBucket, string fileNameInS3)
        {
            //To Upload File in S3Bucket
            try
            {
                TransferUtility utility = new TransferUtility(_client);
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                if (string.IsNullOrEmpty(subDirectoryInBucket))  //null subdirectory just bucket name --> upload file in bucket
                {
                    PutObjectRequest putRequest1 = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = fileNameInS3,
                        FilePath = localFilePath,
                        //ContentType = "text/plain"
                    };
                    PutObjectResponse response1 = await _client.PutObjectAsync(putRequest1);
                    listbucketrootfolders();
                    return;
                }
                else
                {
                    PutObjectRequest putRequest = new PutObjectRequest  //upload file in folder inside bucket
                    {
                        BucketName = bucketName,
                        Key = subDirectoryInBucket + fileNameInS3,
                        FilePath = localFilePath,
                        //ContentType = "application/x-directory"
                    };
                    PutObjectResponse response = await _client.PutObjectAsync(putRequest);
                }
                listfoldercontent(foldername);
            }
            catch (AmazonS3Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //To Download Directory and Files inside 
        private async void Downloadfolder(string foldername, string downloadpath)
        {
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    Prefix = foldername
                };
                var response = await _client.ListObjectsV2Async(request);
                foreach (var f in response.S3Objects)
                {
                    S3Download(f.Key, downloadpath);
                }
            }
            catch (AmazonS3Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void S3Download(string _ObjectKey, string downloadPath)
        {
            //To Download with Progressbar working got some bug 
            try
            {
                GetObjectRequest request = new GetObjectRequest();
                request.BucketName = bucketName;
                request.Key = _ObjectKey;
                //TransferUtility fileTransferUtility = new TransferUtility(client); 
                using (var response = await _client.GetObjectAsync(request))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        response.WriteObjectProgressEvent += Response_WriteObjectProgressEvent;
                        //if (!downloadPath.Contains(@"\"))
                        //{
                        //   downloadPath = downloadPath + @"\";
                        //}
                        //string path = (downloadPath + _ObjectKey);
                        //path = path.Replace("/", @"\");
                        string path = (downloadPath + @"\" + _ObjectKey);
                        await response.WriteResponseStreamToFileAsync(path, true, default(CancellationToken));
                        //fileTransferUtility.DownloadAsync(downloadPath + @"\" + _ObjectKey, bucketName, _ObjectKey);
                    }
                }
            }
            catch (AmazonS3Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Response_WriteObjectProgressEvent(object sender, WriteObjectProgressArgs args)
        {
            //Debug.WriteLine($"Tansfered: {args.TransferredBytes}/{args.TotalBytes} - Progress: {args.PercentDone}%");
            UpdateProgressBar(args.PercentDone);
        }
        void UpdateProgressBar(int value)
        {
            progressBar1.BeginInvoke(new Action(() =>
            {
                progressBar1.Value = (int)value;
            }));
        }
        public static class FileSizeFormatter
        {
            // Load all suffixes in an array  
            static readonly string[] suffixes =
            { "Bytes", "KB", "MB", "GB", "TB", "PB" };
            public static string FormatSize(Int64 bytes)
            {
                int counter = 0;
                decimal number = (decimal)bytes;
                while (Math.Round(number / 1024) >= 1)
                {
                    number = number / 1024;
                    counter++;
                }
                return string.Format("{0:n1}{1}", number, suffixes[counter]);
            }
        }

        // To List all Bucket Names
        private async void getbuckets_of_awsaccount()
        {
            try
            {
                var response1 = await _client.ListBucketsAsync();
                //listView1.Invoke((MethodInvoker)delegate ()
                //{
                listView1.Items.Clear();
                //});

                foreach (var b in response1.Buckets)
                {
                    var bn = b.BucketName;
                    var ca = Convert.ToString(b.CreationDate);
                    listView1.Invoke((MethodInvoker)delegate ()
                    {
                        ListViewItem item = new ListViewItem(bn);
                        item.SubItems.Add(ca);
                        listView1.Items.Add(item);
                        listView1.EnsureVisible(listView1.Items.Count - 1);
                    });
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        // listing root folders of S3Bucket in listview2
        private async void listbucketrootfolders()
        {
            //await Task.Run(() =>
            //{
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    MaxKeys = 1000
                };
                var response = await _client.ListObjectsV2Async(request);
                listView2.Invoke((MethodInvoker)delegate ()
                {
                    listView2.Items.Clear();
                });
                foreach (var obj in response.S3Objects)
                {
                    var path = obj.Key;
                    if (path.Contains("/"))
                    {
                        path = path.Substring(0, path.IndexOf("/") + 1);
                        if (listView2.FindItemWithText(path) != null)
                        {
                            continue;
                        }
                    }
                    string Date_Time = Convert.ToString(obj.LastModified);

                    listView2.Invoke((MethodInvoker)delegate ()
                    {
                        ListViewItem item = new ListViewItem(path);
                        item.SubItems.Add(Date_Time);
                        listView2.Items.Add(item);
                        listView2.EnsureVisible(listView2.Items.Count - 1);
                    });
                }
            }
            catch (AmazonS3Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // To list files and folder from specific folder of Bucket
        private async void listfoldercontent(string foldername)
        {
            //To List directories and files in listView3, 
            //it will show object key name

            //IAmazonS3 client = new AmazonS3Client(_S3AccessKey, _S3SecretKey, Amazon.RegionEndpoint.APSouth1);
            var request = new ListObjectsV2Request
            {
                BucketName = bucketName,
                Prefix = foldername
            };
            var response = await _client.ListObjectsV2Async(request);
            // if (listView3.InvokeRequired)
            //{
            listView3.Invoke((MethodInvoker)delegate ()
            {
                listView3.Items.Clear();
            });
            // } 
            foreach (var f in response.S3Objects)
            {
                double sizeCheck = Convert.ToDouble(f.Size);
                string size = FileSizeFormatter.FormatSize(Convert.ToInt64(sizeCheck));
                string Date_Time = Convert.ToString(f.LastModified);
                //if (listView3.InvokeRequired)
                //{
                listView3.Invoke((MethodInvoker)delegate ()
                {
                    ListViewItem item = new ListViewItem(f.Key);
                    item.SubItems.Add(size);
                    item.SubItems.Add(Date_Time);
                    listView3.Items.Add(item);
                    listView3.EnsureVisible(listView3.Items.Count - 1);
                });
                //}      
            }
        }

        //To Create Directory in S3Bucket 
        private async void CreateDir(string folderName)
        {
            try
            {
                string foldername = folderName.Substring(Path.GetPathRoot(folderName).Length);
                // Add "/" at last of string .. because it is required to make object in aws key form
                foldername = foldername.Replace(@"\", "/");
                string fn = foldername.EndsWith(@"/") ? foldername : foldername + @"/";

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = fn                   // <-- in S3 key represents a path  
                };
                PutObjectResponse response = await _client.PutObjectAsync(request);
                listbucketrootfolders();
            }
            catch (AmazonS3Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //To List all first Directories from S3Bucket in listView2, when clicked on Bucket name from listView1
        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            if (listView1.SelectedIndices.Count <= 0) return;
            int selectedindex = listView1.SelectedIndices[0];
            if (selectedindex >= 0)
            {
                bucketName = listView1.Items[selectedindex].Text;
                listbucketrootfolders();
            }
        }

        //To List all Directories and Folder from listView3 in object key fromat, when clicked on listView2 
        //listView2 contains first directorys of S3Bucket
        private void listView2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView2.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView2.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                foldername = listView2.Items[intselectedindex].Text;
                listfoldercontent(foldername);
            }
        }

        private void txtBox_AccessKeyID_TextChanged(object sender, EventArgs e)
        {
            _S3AccessKey = txtBox_AccessKeyID.Text;
        }

        private void txtBox_SecretAccessKey_TextChanged(object sender, EventArgs e)
        {
            _S3SecretKey = txtBox_SecretAccessKey.Text;
        }


        // TO Get Credentials & List All Buckets Name 
        private void btn_listbucket_Click(object sender, EventArgs e)
        {
            try
            {
                //checking credentials textbox is filled or empty
                if (string.IsNullOrEmpty(_S3AccessKey) || string.IsNullOrEmpty(_S3SecretKey))
                {
                    MessageBox.Show("Please Enter AWS Access_Key and SecretKey!");
                    return;
                }

                var result = Create_AWSCredentials();
                // if credentials not created then return
                if (result == false)
                    return;

                getbuckets_of_awsaccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // TO Create Directory in S3
        private void btn_CreateFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Folder Name");
                    return;
                }
                string foldername = textBox1.Text;
                CreateDir(foldername);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //To Delete Objects from S3Bucket
        private async void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                //Check if from listView2 and listView3 items is not Selected show popup message
                if ((listView2.SelectedItems.Count <= 0) && (listView3.SelectedItems.Count <= 0))
                {
                    MessageBox.Show("Please Select Object to Delete");
                    return;
                }

                string objectname;
                if (listView3.SelectedItems.Count > 0) //delete single object
                {
                    objectname = listView3.SelectedItems[0].Text;
                    Delete(objectname);
                    listfoldercontent(foldername);
                }
                else
                {
                    objectname = listView2.SelectedItems[0].Text;
                    //run if objectname is not folder
                    if (!objectname.Contains("/"))
                    {
                        Delete(objectname);
                        listView3.Items.Clear();
                    }
                    //run if objectname is folder inside bucket
                    else
                    {
                        //delete all objects inside folder
                        var request = new ListObjectsV2Request
                        {
                            BucketName = bucketName,
                            Prefix = objectname
                        };
                        var response = await _client.ListObjectsV2Async(request);
                        foreach (var file in response.S3Objects)
                        {
                            Delete(file.Key);
                            listView3.Items.Clear();
                        }
                    }
                }
                listbucketrootfolders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // To Delete Object
        private async void Delete(string objectname)
        {
            try
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    Key = objectname,
                    BucketName = bucketName,
                };
                await _client.DeleteObjectAsync(deleteRequest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //To Upload Folder in S3Buckect
        private async void btn_UploadFolder_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog1.ShowDialog();
                if (folderBrowserDialog1.SelectedPath == string.Empty)
                {
                    MessageBox.Show("Please Select folder to Upload");
                    return;
                }
                string _Upfopath = folderBrowserDialog1.SelectedPath;
                string keyname = string.Empty;
                foreach (string file in GetFiles(_Upfopath))
                {
                    //IAmazonS3 _client = new AmazonS3Client(_S3AccessKey, _S3SecretKey, Amazon.RegionEndpoint.APSouth1);
                    keyname = file.Substring(Path.GetPathRoot(file).Length);
                    keyname = keyname.Replace(@"\", "/");
                    string rootkeyname = keyname.Substring(0, keyname.IndexOf("/") + 1);
                    foldername = rootkeyname;
                    PutObjectRequest putRequest1 = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = keyname,
                        FilePath = file,
                    };
                    PutObjectResponse response1 = await _client.PutObjectAsync(putRequest1);
                    listbucketrootfolders();
                    listfoldercontent(foldername);
                }
                //listfoldercontent(foldername);
                //listbucketrootfolders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //To Upload File in S3Bucket and in Directory inside Bucket
        private void btn_UploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                string subdir = string.Empty;
                if ((listView2.SelectedItems.Count > 0) || (listView3.SelectedItems.Count > 0))
                {
                    if (listView2.SelectedItems.Count > 0)
                        subdir = listView2.SelectedItems[0].Text;
                    else
                        subdir = listView3.SelectedItems[0].Text;
                }
                string filepath;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                DialogResult result = openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName == string.Empty)
                {
                    MessageBox.Show("Please Select Object to Upload");
                    return;
                }
                if (result == DialogResult.OK)
                {
                    filepath = openFileDialog1.FileName;
                    UploadFile(filepath, subdir, Path.GetFileName(filepath));
                }
                listfoldercontent(foldername);
                listbucketrootfolders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Downloads Objects from S3Bucket. both File and Folder
        private void btn_Download_Click(object sender, EventArgs e)
        {
            try
            {
                //await Task.Run(() =>
                //{
                if ((listView3.SelectedItems.Count <= 0) && (listView2.SelectedItems.Count <= 0))
                {
                    MessageBox.Show("Select folder to Download");
                    return;
                }
                string objectname;
                if (listView3.SelectedItems.Count > 0)
                    objectname = listView3.SelectedItems[0].Text;
                else
                    objectname = listView2.SelectedItems[0].Text;

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    _downloadpath = folderBrowserDialog1.SelectedPath;

                if (_downloadpath == string.Empty)
                {
                    MessageBox.Show("Please Select folder to Download Object");
                    return;
                }
                Downloadfolder(objectname, _downloadpath);
                //});
            }
            catch (Exception ex)
            {
            }
        }

        // TO Download Whole Bucket
        private async void btn_Download_Whole_Bucket_Click(object sender, EventArgs e)
        {
            try
            {
                string _Dwnpath = string.Empty;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    _Dwnpath = folderBrowserDialog1.SelectedPath + "\\" + bucketName;
                }
                if (_Dwnpath == string.Empty)
                {
                    MessageBox.Show("Please Select folder to download whole Bucket");
                    return;
                }
                if (MessageBox.Show("Yes  Or  No", "Confirm Downloading at " + _Dwnpath, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                    return;

                await AsyncDownload(_Dwnpath);
                MessageBox.Show("Process Started");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task AsyncDownload(string downloadPath)
        {
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    MaxKeys = 1000
                };
                var response = await _client.ListObjectsV2Async(request);
                var utility = new TransferUtility(_client);
                foreach (var obj in response.S3Objects)
                {
                    string objKey = obj.Key;
                    double sizeCheck = Convert.ToDouble(obj.Size);
                    int fileNameLength = objKey.Length;
                    S3Download(obj.Key, downloadPath);
                }
            }
            catch (AmazonS3Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
