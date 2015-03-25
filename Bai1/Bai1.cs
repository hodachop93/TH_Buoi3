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
        private string currentPath;
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
                showDirectory(tnode);
            }
            treeView.SelectedNode = null;
        }

        private void showDirectory(TreeNode tnode)
        {
            if (Directory.Exists(getFullPath(tnode.FullPath))==false)
            {
                MessageBox.Show("Duong dan nay khong ton tai", "Error", MessageBoxButtons.OK);
            }
            else
            {
               //O dia hoac folder duoc chon
               
                string path = tnode.FullPath;
                path = getFullPath(path);
                DirectoryInfo rootdir = new DirectoryInfo(path);
                try
                {
                    foreach (DirectoryInfo direc in rootdir.GetDirectories())
                    {
                        tnode.Nodes.Add(new TreeNode(direc.Name, 3, 3));
                    }
                    tnode.Expand();
                }
                catch
                {
                    MessageBox.Show("Bạn không có quyền truy cập vào tệp này!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
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
            string[] lvData= new string[4];
            listView.Items.Clear();
            //Duyet qua cac folder
            try
            {
                foreach (DirectoryInfo dir in rootdir.GetDirectories())
                {
                    DirectoryInfo folder = new DirectoryInfo(dir.FullName);
                    lvData[0] = folder.Name;
                    lvData[1] = folder.LastWriteTime.ToString();
                    lvData[2] = "Folder";
                    lvData[3] = folder.FullName;
                    ListViewItem lvItem = new ListViewItem(lvData);
                    listView.Items.Add(lvItem);
                }
                string[] files = Directory.GetFiles(rootdir.FullName);
                //Duyet qua cac file
                foreach (string file in files)
                {
                    FileInfo objFile = new FileInfo(file);
                    lvData[0] = objFile.Name;
                    lvData[1] = objFile.LastAccessTime.ToString();
                    lvData[2] = "File";
                    lvData[3] = objFile.FullName;
                    ListViewItem lvItem = new ListViewItem(lvData);
                    listView.Items.Add(lvItem);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Truy cập này không được phép", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
           
            
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem lvItem = listView.FocusedItem;
            string path = lvItem.SubItems[3].Text;
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    //Neu item ta chon la file
                    Process.Start(path);
                }
                else
                {
                    //Neu item ta chon la folder
                    DirectoryInfo dir = new DirectoryInfo(path + "\\");
                    if (dir.Exists)
                    {
                        showFolderFile(dir);
                    }
                    else
                    {
                        MessageBox.Show("Folder này không tồn tại", "Error", MessageBoxButtons.OK);    
                    }
                }
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Không có chương trình nào để mở loại file này", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ListViewItem lvItem = listView.FocusedItem;
            string filename = lvItem.SubItems[0].Text;
            string path = lvItem.SubItems[3].Text;
            MessageBox.Show("File name: " + filename + "\nPath: " + path, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnNewFolder_Click(object sender, EventArgs e)
        {
            //Lay duong dan cua cua thu muc hien hanh
            ListViewItem lvItem = listView.Items[0];
            string name = lvItem.SubItems[0].Text;
            string path = lvItem.SubItems[3].Text;
            currentPath= path.Replace(name, "");
            lvItem = new ListViewItem();
            lvItem.Text = "New Folder";
            listView.LabelEdit = true;
            listView.Items.Add(lvItem);
            lvItem.BeginEdit();
        }

        private void listView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string pathNewFolder = currentPath + "\\" + e.Label;
            DirectoryInfo rootDir = new DirectoryInfo(currentPath);
            if (e.Label == null)
            {
                pathNewFolder = currentPath + "\\New Folder";
            }
            if (Directory.Exists(pathNewFolder))
            {
                MessageBox.Show("Folder này đã tồn tại", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                ListViewItem lvItem = listView.FocusedItem;
                listView.Items.Remove(lvItem);
            }
            else
            {
                Directory.CreateDirectory(pathNewFolder);
                showFolderFile(rootDir);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui long chon File hoac Folder de copy!");
            }
            else
            {
                FolderBrowserDialog browser = new FolderBrowserDialog();
                browser.ShowDialog();

                for (int i = 0; i < listView.SelectedItems.Count; i++)
                {
                    //Lay duong dan cua file da chon
                    string pathCopy = listView.SelectedItems[i].SubItems[3].Text;
                    //Neu la file
                    if (File.Exists(pathCopy))
                    {
                        FileInfo file = new FileInfo(pathCopy);
                        file.CopyTo(browser.SelectedPath+"\\"+file.Name, true);
                    }
                    else if (Directory.Exists(pathCopy))
                    {
                        DirectoryCopy(pathCopy, browser.SelectedPath + "\\" + listView.SelectedItems[i].SubItems[0].Text, true);
                    }
                    
                }
              

            }
        }

        //Ham copy folder
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ListViewItem lvItem = listView.FocusedItem;
            //Lay duong dan thu muc hien hanh
            string name = lvItem.SubItems[0].Text;
            string path = lvItem.SubItems[3].Text;
            currentPath = path.Replace(name, "");
            listView.Items.Remove(lvItem);
            string pathDelete = lvItem.SubItems[3].Text;

            if (File.Exists(pathDelete))
            {
                //Neu la file
                FileInfo file = new FileInfo(pathDelete);
                DialogResult dialog = MessageBox.Show("Bạn có muốn xóa file này không?", "Xac nhan xoa file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    file.Delete();
                }
                else 
                    return;

            }
            else if (Directory.Exists(path))
            {
                //Neu la folder
                DirectoryInfo dir = new DirectoryInfo(path);
                DialogResult dialog = MessageBox.Show("Bạn có muốn xóa file này không?", "Xac nhan xoa file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    dir.Delete(true);
                }
                else
                    return;
            }
            DirectoryInfo rootDir = new DirectoryInfo(currentPath);
            showFolderFile(rootDir);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Lay duong dan thu muc hien hanh
            ListViewItem lvItem = listView.Items[0];
            string name = lvItem.SubItems[0].Text;
            string path = lvItem.SubItems[3].Text;
            currentPath = path.Replace(name, "");
            listView.Items.Clear();
            string[] lvData = new string[4];
            DirectoryInfo rootdir = new DirectoryInfo(currentPath);
            foreach (DirectoryInfo dir in rootdir.GetDirectories())
            {
                DirectoryInfo folder = new DirectoryInfo(dir.FullName);
                if (folder.Name.Contains(txtSearch.Text))
                {
                    lvData[0] = folder.Name;
                    lvData[1] = folder.LastWriteTime.ToString();
                    lvData[2] = "Folder";
                    lvData[3] = folder.FullName;
                    ListViewItem lvItem1 = new ListViewItem(lvData);
                    listView.Items.Add(lvItem1);
                }
               
            }
            string[] files = Directory.GetFiles(rootdir.FullName);
            //Duyet qua cac file
            foreach (string file in files)
            {
                FileInfo objFile = new FileInfo(file);
                if (objFile.Name.Contains(txtSearch.Text))
                {
                    lvData[0] = objFile.Name;
                    lvData[1] = objFile.LastAccessTime.ToString();
                    lvData[2] = "File";
                    lvData[3] = objFile.FullName;
                    ListViewItem lvItem1 = new ListViewItem(lvData);
                    listView.Items.Add(lvItem1);
                }
                
            }
        }

        
       
        
    }
}
