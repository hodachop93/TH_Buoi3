using System;
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
                XmlDocument xmlDoc = new XmlDocument();
                FileStream fs = File.OpenRead(open.FileName);
                xmlDoc.Load(fs);
                //Xoa cac node trong tree view
                treeView.Nodes.Clear();
                //Add node root cua file xml vao tree view
                treeView.Nodes.Add(new TreeNode(xmlDoc.DocumentElement.Name));
                //Lay node root cua file xml
                XmlNode xNode = xmlDoc.FirstChild;
                TreeNode tNode = treeView.Nodes[0];
                this.AddNode(xNode, tNode);

                //XmlDataDocument xmldoc = new XmlDataDocument();
                //XmlNode xmlnode;
                //FileStream fs = new FileStream(open.FileName, FileMode.Open, FileAccess.Read);
                //xmldoc.Load(fs);
                //xmlnode = xmldoc.FirstChild;
                //treeView.Nodes.Clear();
                ////Add nut rootNode
                //treeView.Nodes.Add(new TreeNode(xmldoc.DocumentElement.Name));
                //TreeNode tNode;
                //tNode = treeView.Nodes[0];
                //this.AddNode(xmlnode, tNode);
            }

        }

        //Ham add node vao tree view
        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i = 0;
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                inTreeNode.Text = inXmlNode.InnerText;

            }
        }

        private void btnEditNode_Click(object sender, EventArgs e)
        {
            if (treeView.Visible == true)
            {
                txtBox.Text = treeView.SelectedNode.Text;
            }
        }

        
    }
}
