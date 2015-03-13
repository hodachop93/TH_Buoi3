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
        XmlDocument xmlDoc;
        XmlNodeList nodeList;
        string fileName;
        public Bai2()
        {
            InitializeComponent();
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XML file (*.xml)|*.xml";
            open.RestoreDirectory = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                treeView.Visible = true;
                
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
            treeView.ExpandAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML File (*.xml)|*.xml";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                XmlWriter xmlWrite = new XmlTextWriter(saveDialog.FileName, System.Text.Encoding.UTF8);
                xmlWrite.WriteStartDocument();
                //Tao root Node
                xmlWrite.WriteStartElement(treeView.Nodes[0].Text);
                foreach (TreeNode node in treeView.Nodes)
                {
                    saveNode(node.Nodes, xmlWrite);
                }
                //Dong root Node
                xmlWrite.WriteEndElement();
                xmlWrite.Close();
            }
        }

        private void saveNode(TreeNodeCollection collection, XmlWriter writer)
        {
            foreach (TreeNode node in collection)
            {
            
                if (node.Nodes.Count > 0)
                {
                    writer.WriteStartElement(node.Text);
                    saveNode(node.Nodes, writer);
                    writer.WriteEndElement();
                }
                else //No child nodes, so we just write the text
                {
                    writer.WriteString(node.Text);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa node này không?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                treeView.SelectedNode.Remove();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            treeView.SelectedNode.Nodes.Add(new TreeNode(txtAddNode.Text));
            txtAddNode.Text = "";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            nodeList = xmlDoc.GetElementsByTagName(txtFindNode.Text);
            foreach (XmlNode node in nodeList)
            {
                listBox.Items.Add(node.Name);
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBox.Text = nodeList[listBox.SelectedIndex].InnerText;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            treeView.SelectedNode.Text = txtEdit.Text;
        }

      

       



        
    }
}
