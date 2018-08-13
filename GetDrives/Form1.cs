using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetDrives
{
    public partial class Form1 : Form
    {
        ExplorerTreeView fileExplorer = new ExplorerTreeView();
        public Form1()
        {
            InitializeComponent();
            fileExplorer.CreateTree(this.treeView1);
        }

        private void treeView_FileExplorer(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "")
            {
                TreeNode node = fileExplorer.ListDirectory(e.Node);
            }
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = e.Node.FullPath;

            try
            {

                if (new DirectoryInfo(path).FullName == new DirectoryInfo(path).Root.FullName)
                {
                    var drives = DriveInfo.GetDrives();

                    var selectedDrive = drives.FirstOrDefault(drive => drive.IsReady && drive.Name == path);

                    lblInfo.Text = GetDriveDetails(selectedDrive);
                }

                else if (Directory.Exists(path))
                {
                    var stringBuilder = new StringBuilder();

                    var dirInfo = new DirectoryInfo(path);
                    var dirFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

                    lblInfo.Text = GetDirectoryDetails(dirInfo, dirFiles.Count());

                }
                else if (File.Exists(path))
                {
                    var fileInfo = new FileInfo(path);
                    lblInfo.Text = GetFileDetails(fileInfo);
                }

                else
                {
                    lblInfo.Text = "Something went wrong! Try Again";
                }
            }

            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private string GetDriveDetails(DriveInfo driveInfo)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"Drive letter: {driveInfo.Name} {Environment.NewLine}");
            stringBuilder.Append($"Drive label :  {driveInfo.VolumeLabel} {Environment.NewLine}");
            stringBuilder.Append($"Total Space (GB):  {string.Format("{0:0.##}", (double)(driveInfo.TotalSize / Math.Pow(1024, 3)))} {Environment.NewLine}");
            stringBuilder.Append($"Free Space  (GB):  {string.Format("{0:0.##}", (double)(driveInfo.TotalFreeSpace / Math.Pow(1024, 3)))} {Environment.NewLine}");

            return stringBuilder.ToString();
        }

        private string GetDirectoryDetails(DirectoryInfo directoryInfo, int filesCount)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"Folder name: {directoryInfo.Name} {Environment.NewLine}");
            stringBuilder.Append($"Number of files: {filesCount} {Environment.NewLine}");
            stringBuilder.Append($"Folder attributes: {directoryInfo.Attributes} {Environment.NewLine}");

            return stringBuilder.ToString();
        }

        private string GetFileDetails(FileInfo fileInfo)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"File name: {fileInfo.Name} {Environment.NewLine}");
            stringBuilder.Append($"File size: {fileInfo.Length} bytes {Environment.NewLine}");
            stringBuilder.Append($"File attributes: {fileInfo.Attributes} {Environment.NewLine}");

            return stringBuilder.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}