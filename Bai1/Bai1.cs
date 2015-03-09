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
            int imageIndex=0;
            int selectedIndex=0;
            treeView.Nodes.Clear();
            TreeNode tnode;
            tnode = new TreeNode("My Computer", imageIndex, selectedIndex);
            treeView.Nodes.Add(tnode);
            TreeNodeCollection nodeCollection = tnode.Nodes;
            //Lay danh sach cac o dia
            ManagementObjectCollection driverCollection = getDrivers();
            foreach (ManagementObject obj in driverCollection)
            {
                switch (int.Parse(obj["DriverType"].ToString()))
                {

                }
            }
        }

       
        
    }
}
