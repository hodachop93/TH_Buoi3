using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bai1
{
    public partial class MiniNotepad : Form
    {
        private string filePath = null;
        public MiniNotepad()
        {
            InitializeComponent();
        }

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            newFile();
        }

        private void toolStripBtnNew_Click(object sender, EventArgs e)
        {
            newFile();
        }

        //
        private void newFile()
        {
            this.Text = "New Text Document";
            richTxtBox.Text = "";
            richTxtBox.Visible = true;
            filePath = null;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void toolStripBtnOpen_Click(object sender, EventArgs e)
        {
            openFile();
        }

        //Mo file san co
        private void openFile()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text File (*.txt)|*.txt|Rich Text File (*.rtf)|*.rtf";
            //Mo lai directory o lan truoc
            open.RestoreDirectory = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                richTxtBox.Visible = true;
                string fileName = Path.GetFileNameWithoutExtension(open.FileName);
                this.Text = fileName + " - Mini Notepad";
                //Luu dia chi file
                filePath = open.FileName;
                //Load file giu nguyen dinh dang van ban
                richTxtBox.LoadFile(open.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        //Save file
        private void saveFile()
        {
            //Tao hop thoai Save File
            SaveFileDialog save = new SaveFileDialog();
            //Mo lai directory da luu o lan truoc
            save.RestoreDirectory = true;
            save.Filter = "Text File (*.txt)|*.txt|Rich Text File (*.rtf)|*.rtf";
            if (filePath!=null)
            {
                richTxtBox.SaveFile(filePath, RichTextBoxStreamType.PlainText);
            }
            else
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    richTxtBox.SaveFile(save.FileName, RichTextBoxStreamType.PlainText);
                    filePath = save.FileName;
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    this.Text = fileName + " - Mini Notepad";
                }
            }
        }

   
    }
}
