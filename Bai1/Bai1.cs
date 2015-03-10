using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Management;



namespace Bai1
{
    public partial class Bai1 : Form
    {
        public Bai1()
        {
            InitializeComponent();
        }

        //Ham lay cac o dia
        private ManagementObjectCollection getDrivers()
        {
            ManagementObjectSearcher query = new ManagementObjectSearcher("Select * From Win32_LogicalDisk");
            ManagementObjectCollection queryCollection = query.Get();
            return queryCollection;
        }

        //Hien thi thong tin My Computer ra treeView
        private void showDriversList()
        {
            int imageIndex = 0;
            int selectedImageIndex = 0;
            const int LocalDisk = 3;
            const int CD = 5;
            treeView.Nodes.Clear();
            TreeNode tnode;
            tnode = new TreeNode("My Computer",0,0);
            treeView.Nodes.Add(tnode);
            TreeNodeCollection nodeCollection = tnode.Nodes;
            //Lay danh sach cac o dia
            ManagementObjectCollection driverCollection = getDrivers();
            
            foreach (ManagementObject obj in driverCollection)
            {
                switch (int.Parse(obj["DriveType"].ToString()))
                {

                    
                    case LocalDisk: //O cung 
                        imageIndex = 1;
                        selectedImageIndex = 1;
                        break;
                    case CD:  //O CD			
                        imageIndex = 2;
                        selectedImageIndex = 2;
                        break;
                    default:  //Folder
                        imageIndex = 3;
                        selectedImageIndex = 3;
                        break;
            
                }
                // Tạo một Driver Node mới
                tnode = new TreeNode(obj["Name"].ToString() + "\\", imageIndex, selectedImageIndex);

                // Chèn vào Treeview
                nodeCollection.Add(tnode);
            }
            treeView.ExpandAll();
        }

        private void Bai1_Load(object sender, EventArgs e)
        {
            this.Text = "My Computer";
            showDriversList();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tnode = e.Node;
            tnode.Nodes.Clear();
            if (tnode.SelectedImageIndex == 0)
            {
                //Node My computer duoc chon
                showDriversList();
                this.Text = "My Computer";
            }
            else
            {
                this.Text = tnode.Text;
                showDirectory(tnode, tnode.Nodes);
            }
        }

        private void showDirectory(TreeNode tnode, TreeNodeCollection nodeCollection)
        {
            if (Directory.Exists(getFullPath(tnode.FullPath))==false)
            {
                MessageBox.Show("Duong dan nay khong ton tai", "Error", MessageBoxButtons.OK);
            }
            else
            {
               //O dia duoc chon
                string diskPath = tnode.Text;
                DriveInfo di = new DriveInfo(diskPath);
                DirectoryInfo rootdir = di.RootDirectory;
                showFolderFile(rootdir);
            }
        }

        //Lay duong dan cua mot folder
        private string getFullPath(string path)
        {
            return path.Replace("My Computer\\", "");
        }

        private void showFolderFile(DirectoryInfo rootdir)
        {
            string[] lvData= new string[3];
            listView.Items.Clear();
            //Duyet qua cac folder
            foreach (DirectoryInfo dir in rootdir.GetDirectories())
            {
                DirectoryInfo folder = new DirectoryInfo(dir.FullName);
                lvData[0] = folder.Name;
                lvData[1] = folder.LastWriteTime.ToString();
                lvData[2] = "Folder";
                ListViewItem lvItem= new ListViewItem(lvData);
                listView.Items.Add(lvItem);
            }
            //Duyet qua cac file
            string[] files = Directory.GetFiles(rootdir.FullName);
            foreach (string file in files)
            {
                FileInfo objFile = new FileInfo(file);
                lvData[0] = objFile.Name;
                lvData[1] = objFile.LastAccessTime.ToString();
                lvData[2] = "File";
                ListViewItem lvItem = new ListViewItem(lvData);
                listView.Items.Add(lvItem);
            }
        }

       
        
    }
}
