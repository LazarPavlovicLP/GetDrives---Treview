using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace GetDrives
{
    class ExplorerTreeView
    {
        public ExplorerTreeView()
        {

        }

        public bool CreateTree(TreeView treeView)
        {
            bool returnValue = false;

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {

                TreeNode child = new TreeNode();
                child.Text = drive.Name;
                //child.Name = drive.AvailableFreeSpace.ToString();
                child.ImageIndex = 0;
                child.SelectedImageIndex = 0;
                child.Nodes.Add("");
                treeView.Nodes.Add(child);
                returnValue = true;
            }

            return returnValue;

        }

        public TreeNode ListDirectory(TreeNode parentNode)
        {
            DirectoryInfo rootDir;

            Char[] slash = { '\\' };
            string[] nameList = parentNode.FullPath.Split(slash);

            rootDir = new DirectoryInfo(parentNode.FullPath + "\\");

            parentNode.Nodes[0].Remove();

            //Fill directories
            foreach (DirectoryInfo dir in rootDir.GetDirectories())
            {

                TreeNode node = new TreeNode();
                node.Text = dir.Name;
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                node.Nodes.Add("");
                parentNode.Nodes.Add(node);
            }

            //Fill files
            foreach (FileInfo file in rootDir.GetFiles())
            {
                TreeNode node = new TreeNode();
                node.Text = file.Name;
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                parentNode.Nodes.Add(node);
            }

            return parentNode;
        }
    }
}
