﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Bai2
{
    public partial class Bai2 : Form
    {
        XmlDocument xmlDoc;
        string fileName;
        public Bai2()
        {
            InitializeComponent();
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XML file (*.xml)|*.xml";
            open.RestoreDirectory = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                treeView.Visible = true;
                txtBox.Visible = true;
                xmlDoc = new XmlDocument();
                FileStream fs = File.OpenRead(open.FileName);
                fileName = open.FileName;
                xmlDoc.Load(fs);
                //Xoa cac node trong tree view
                treeView.Nodes.Clear();
                //Add node root cua file xml vao tree view
                treeView.Nodes.Add(new TreeNode(xmlDoc.DocumentElement.Name));
                //Lay node root cua file xml
                XmlNode xNode = xmlDoc.FirstChild;
                TreeNode tNode = treeView.Nodes[0];
                this.AddNode(xNode, tNode);
                
                
            }  

        }

        //Ham add node vao tree view
        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (int i = 0; i < nodeList.Count; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    string name = xNode.Name;
                    string text = xNode.InnerText;
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    this.AddNode(xNode, tNode);
                }
            }
            else
            {
                inTreeNode.Text = inXmlNode.InnerText;
                string s = inXmlNode.InnerText;
                string name = inXmlNode.Name;
            }
           
        }

        private void btnEditNode_Click(object sender, EventArgs e)
        {
            if (treeView.Visible == true)
            {
                txtBox.Text = treeView.SelectedNode.Text;

            }
        }

        //Luu node da duoc thay doi vao file xml
        private void btnSave_Click(object sender, EventArgs e)
        {
            TreeNode tSelectedNode = treeView.SelectedNode;
            string s = tSelectedNode.FullPath;
            XmlElement root = xmlDoc.DocumentElement;
            XmlNode xNode = root.SelectSingleNode(s);

        }



        
    }
}