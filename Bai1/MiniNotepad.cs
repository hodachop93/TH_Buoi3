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
using System.Drawing.Text;


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

        private void MiniNotepad_Load(object sender, EventArgs e)
        {
            int[] size = { 8, 10, 12, 14, 16, 18, 20, 24, 28, 32, 36, 48, 72 };
            for (int i = 0; i < size.Length; i++)
            {
                combSize.Items.Add(size[i]);
            }
            combSize.SelectedIndex = 2;
            InstalledFontCollection listFont = new InstalledFontCollection();
            //Xac dinh vi tri cua font Times New Roman
            int j = 0, c = 0;
            foreach (FontFamily font in listFont.Families)
            {
                combFont.Items.Add(font.Name);
                if (font.Name.Contains("Times New Roman"))
                    c = j;
                j++;
            }
            combFont.SelectedIndex = c;
        }
        //Xu ly trong rich text box khi font thay doi
        private void combFont_SelectedIndexChanged(object sender, EventArgs e)
        {

            FontFamily fontfa = new FontFamily(combFont.SelectedText);
            System.Drawing.Font font = new System.Drawing.Font(fontfa, float.Parse(combSize.SelectedText));
            if (!richTxtBox.Text.Equals(""))
                richTxtBox.SelectionFont = font;
        }

   
    }
}
